using Microsoft.AspNetCore.Mvc;

namespace RemoteReps.RTSPListener.Web.Controllers;

public sealed class StreamingController : Controller
{
    private readonly ILogger<StreamingController> _logger;

    public StreamingController(ILogger<StreamingController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
}