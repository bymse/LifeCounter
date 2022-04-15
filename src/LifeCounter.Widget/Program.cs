using LifeCounter.Common.Container;
using LifeCounter.Widget.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services
    .UseLifeStore()
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

app.MapControllers();

app.Run();