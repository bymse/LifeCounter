using LifeCounter.Common.Container;
using LifeCounter.Common.Front;
using LifeCounter.DataLayer.Container;
using LifeCounter.Widget.Controllers;
using LifeCounter.Widget.Hubs;
using LifeCounter.Widget.Models.Validation;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSignalR()
    .AddJsonProtocol()
    .AddHubOptions<LifeCounterHub>(e => e.AddFilter<WidgetHubValidationFilter>())
    ;

builder.Services
    .AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services
    .UseLifeStore()
    .UseDb()
    .UseAutoDependencies(typeof(WidgetApiController).Assembly)
    .UseUtilities("widget")
    ;

var app = builder.Build();

app.UseCors(e => e
    .AllowAnyMethod()
    .SetIsOriginAllowed(_ => true)
    .AllowAnyHeader()
    .AllowCredentials()
);

app.UseFront();

app.MapControllers();
app.MapHub<LifeCounterHub>("/widget/hub/life-counter");
app.Run();