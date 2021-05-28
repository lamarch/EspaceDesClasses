namespace MaClassePA.Controllers
{
    using MaClassePA.Data;
    using MaClassePA.Models;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using System.Diagnostics;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IClassesContext context;

        public HomeController(ILogger<HomeController> _logger, IClassesContext _context)
        {
            logger = _logger;
            context = _context;
        }

        public IActionResult Index()
        {
            logger.LogDebug("GET /Home/Index");

            return View(context.Classes.ToList());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            logger.LogDebug("GET /Home/Error");

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("error/{code:int}")]
        public IActionResult Error(int code)
        {
            logger.LogDebug($"GET /Home/Error/{code}");

            return View(code);
        }
    }
}
