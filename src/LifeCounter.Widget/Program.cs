using LifeCounter.Common.Container;
using LifeCounter.Widget.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

LifeStoreContainerConfig.RegisterLifeStore(builder.Services);
DependenciesAutoRegisterer.Register(builder.Services, typeof(WidgetApiController).Assembly);

var app = builder.Build();

app.UseCors(e => e
    .AllowAnyMethod()
    .SetIsOriginAllowed(_ => true)
    .AllowAnyHeader()
    .AllowCredentials()
);

app.MapControllers();

app.Run();