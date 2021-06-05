using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ModuloGameServer.Controllers
{
    public class BotController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public BotController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Endpoint()
        {
            _logger.LogInformation("Homecontroller is called");
            return View();
        }


    }
}
