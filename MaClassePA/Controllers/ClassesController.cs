namespace MaClassePA.Controllers
{
    using MaClassePA.Data;
    using MaClassePA.Models;
    using MaClassePA.Services;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using System.Linq;
    using System.Threading.Tasks;

    [Authorize(Policy = Authorizations.FullClasses)]
    public class ClassesController : Controller
    {
        private readonly IClassesContext context;
        private readonly IAuthorizationService authorizationService;

        public ClassesController(IClassesContext _context, IAuthorizationService _authorizationService)
        {
            context = _context;
            authorizationService = _authorizationService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classe = context.Classes.FirstOrDefault(c => c.Id == id);

            if (classe == null)
            {
                return NotFound();
            }

            ViewBag.CanUpdateMatieres = (await authorizationService.AuthorizeAsync(User, Authorizations.FullMatieres)).Succeeded;
            ViewBag.CanDeleteMatieres = (await authorizationService.AuthorizeAsync(User, Authorizations.FullMatieres)).Succeeded;
            ViewBag.CanCreateMatieres = (await authorizationService.AuthorizeAsync(User, Authorizations.FullMatieres)).Succeeded;

            return View(classe);
        }

        [HttpPost]
        public IActionResult Creer([Bind("Nom")] ClasseModel classe)
        {
            if (ModelState.IsValid)
            {
                context.AjouterClasse(classe);
                context.Sauvegarder();
                return CreatedAtAction(nameof(Index), classe.Id);
            }
            return Ok();
        }

        public IActionResult Supprimer(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classe = context.Classes.FirstOrDefault(c => c.Id == id);

            if (classe == null)
            {
                return NotFound();
            }

            classe.EstSupprime = true;

            return Ok();
        }
    }
}
