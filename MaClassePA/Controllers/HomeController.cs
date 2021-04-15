namespace MaClassePA.Controllers
{
    using MaClassePA.Data;
    using MaClassePA.Models;

    using Microsoft.AspNetCore.Authorization;
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
            return View(context.Classes.ToList());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("error/{code:int}")]
        public IActionResult Error(int code)
        {
            return View(code);
        }
    }
}
