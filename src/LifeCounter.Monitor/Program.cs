using LifeCounter.Common.Container;
using LifeCounter.Common.Front;
using LifeCounter.Monitor.Controllers;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services
    .UseLifeStore()
    .UseAutoDependencies(typeof(DashboardController).Assembly)
    .UseUtilities("monitor")
    ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

using var scope = app.Services.CreateScope();
var frontBundleProvider = scope.ServiceProvider.GetService<IFrontBundleProvider>()!;
app.UseStaticFiles(new StaticFileOptions()
{
    RequestPath = new PathString(frontBundleProvider.FrontBaseUrl),
    FileProvider = new PhysicalFileProvider(frontBundleProvider.GetBasePath())
});

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();