using LifeCounter.Widget.Models;
using LifeCounter.Widget.Models.Dto;
using LifeCounter.Widget.Models.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace LifeCounter.Widget.Controllers;

[ApiController]
[Route("/widget/api/v1")]
public class WidgetApiController : Controller
{
    private readonly StartRequestHandler startRequestHandler;
    private readonly AliveRequestHandler aliveRequestHandler;
    private readonly StopRequestHandler stopRequestHandler;

    public WidgetApiController(StartRequestHandler startRequestHandler, AliveRequestHandler aliveRequestHandler, StopRequestHandler stopRequestHandler)
    {
        this.startRequestHandler = startRequestHandler;
        this.aliveRequestHandler = aliveRequestHandler;
        this.stopRequestHandler = stopRequestHandler;
    }

    [HttpPost, Route("start")]
    public Task<StartResponse> Start(StartRequest request)
    {
        return startRequestHandler.HandleAsync(request);
    }

    [HttpPost, Route("alive")]
    public async Task<IActionResult> Alive(LifeRequest request)
    {
        await aliveRequestHandler.HandleAsync(request);
        return Ok();
    }

    [HttpPost, Route("stop")]
    public async Task<IActionResult> Stop(LifeRequest request)
    {
        await stopRequestHandler.HandleAsync(request);
        return Ok();
    }
}