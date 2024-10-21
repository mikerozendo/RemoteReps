using Microsoft.AspNetCore.Mvc;
using RemoteReps.RTSPListener.Web.Repositories;

namespace RemoteReps.RTSPListener.Web.Controllers;

public sealed class StreamingController(
    SignalRHubRepository signalRHubRepository,
    ILogger<StreamingController> logger)
    : Controller
{
    [HttpPost]
    [Route("streaming-handler")]
    public async Task SendBufferToHub([FromBody] byte[] buffer)
        => await signalRHubRepository.SendBufferAsync(buffer);
    
    public IActionResult Index()
        => View();
}