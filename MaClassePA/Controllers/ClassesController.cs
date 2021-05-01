namespace MaClassePA.Controllers
{
    using MaClassePA.Data;
    using MaClassePA.Models;
    using MaClassePA.Services;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using System.Linq;
    using System.Threading.Tasks;

    [Authorize(Policy = Authorizations.FullClasses)]
    public class ClassesController : Controller
    {
        private readonly IClassesContext context;
        private readonly IAuthorizationService authorizationService;
        private readonly ILogger<ClassesController> logger;

        public ClassesController(IClassesContext _context, ILogger<ClassesController> _logger, IAuthorizationService _authorizationService, SignInManager<IdentityUser> _signInManager)
        {
            context = _context;
            authorizationService = _authorizationService;
            logger = _logger;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(int? id)
        {
            logger.LogDebug($"GET /Classes/Index?id={id}");

            if (id == null) return NotFound();

            var classe = context.Classes.FirstOrDefault(c => c.Id == id);

            if (classe == null) return NotFound();

            //Authorizations :

            //Update
            ViewBag.CanUpdateMatieres = (await authorizationService.AuthorizeAsync(User, Authorizations.FullMatieres)).Succeeded;

            //Delete
            ViewBag.CanDeleteMatieres = (await authorizationService.AuthorizeAsync(User, Authorizations.FullMatieres)).Succeeded;

            //Create
            ViewBag.CanCreateMatieres = (await authorizationService.AuthorizeAsync(User, Authorizations.FullMatieres)).Succeeded;

            return View(classe);
        }
    }
}
