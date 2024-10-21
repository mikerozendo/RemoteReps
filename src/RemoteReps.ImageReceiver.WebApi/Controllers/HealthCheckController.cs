using Microsoft.AspNetCore.Mvc;

namespace RemoteReps.ImageReceiver.WebApi.Controllers;

[ApiController]
[Route("api/health-check")]
public sealed class HealthCheckController : ControllerBase
{
    [HttpGet]
    public IActionResult Check() => Ok();
}