using LifeCounter.Common.Container;
using LifeCounter.Common.Front;
using LifeCounter.Common.Utilities;
using LifeCounter.DataLayer.Container;
using LifeCounter.Monitor.Background;
using LifeCounter.Monitor.Controllers;
using LifeCounter.Monitor.Hubs;
using LifeCounter.Monitor.Models.LifeUpdates;
using LifeCounter.Monitor.Models.LifeUpdates.Subscription;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton(
    typeof(HubLifetimeManager<>),
    typeof(LifeUpdatesHubLifeTimeManager<>)
);
builder.Services.AddSignalR();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHostedService<LivesUpdatesProcessingWorker>();

builder.Services
    .UseLifeStore()
    .UseUtilities("monitor")
    .AddTransient<IViewRenderService, ViewRenderService>()
    .AddSingleton<ILifeUpdatesSubscriptionsManager, LifeUpdatesSubscriptionsManager>()
    .AddSingleton<ILifeUpdatesSubscriber, LifeUpdatesSubscribeRequestsChannel>()
    .AddSingleton<ILifeUpdatesSubscribeRequestsProvider, LifeUpdatesSubscribeRequestsChannel>()
    .UseAutoDependencies(typeof(DashboardController).Assembly)
    ;

if (builder.Environment.IsDevelopment())
{
    builder.Services
        .AddControllersWithViews()
        .AddRazorRuntimeCompilation();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseFront();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<LifeUpdatesHub>("/monitor/life-updates");

app.Run();