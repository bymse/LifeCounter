using LifeCounter.Widget.Models.Dto;
using LifeCounter.Widget.Models.Handlers;
using Microsoft.AspNetCore.SignalR;

namespace LifeCounter.Widget.Hubs;

public class LifeCounterHub : Hub
{
    private readonly IServiceProvider serviceProvider;

    public LifeCounterHub(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public async Task<StartResponse> Start(StartRequest request)
    {
        using (GetScoped<StartRequestHandler>(out var service))
            return await service.HandleAsync(request);
    }

    public async Task Alive(LifeRequest request)
    {
        using (GetScoped<AliveRequestHandler>(out var service))
            await service.HandleAsync(request);
    }

    public async Task Stop(StopRequest request)
    {
        using (GetScoped<StopRequestHandler>(out var service))
            await service.HandleAsync(request);
    }

    private IDisposable GetScoped<T>(out T service) where T : notnull
    {
        var scope = serviceProvider.CreateScope();
        service = scope.ServiceProvider.GetRequiredService<T>();
        return scope;
    }
}