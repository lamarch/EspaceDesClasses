namespace MaClassePA.Controllers
{
    using MaClassePA.Data;
    using MaClassePA.Models;
    using MaClassePA.Services;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    using System.Linq;
    using System.Threading.Tasks;

    [Authorize(Policy = Authorizations.FullMatieres)]
    public class MatieresController : Controller
    {
        private readonly IClassesContext context;
        private readonly ILogger<MatieresController> logger;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IAuthorizationService authorizationService;

        public MatieresController(IClassesContext _context, ILogger<MatieresController> _logger, SignInManager<IdentityUser> _signInManager, IAuthorizationService _authorizationService)
        {
            context = _context;
            logger = _logger;
            signInManager = _signInManager;
            authorizationService = _authorizationService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matiere = context.Matieres.FirstOrDefault(m => m.Id == id);

            if (matiere == null)
            {
                return NotFound();
            }

            ViewBag.CanUpdateRessources = (await authorizationService.AuthorizeAsync(User, Authorizations.FullMatieres)).Succeeded;
            ViewBag.CanDeleteRessources = (await authorizationService.AuthorizeAsync(User, Authorizations.FullMatieres)).Succeeded;
            ViewBag.CanCreateRessources = (await authorizationService.AuthorizeAsync(User, Authorizations.FullMatieres)).Succeeded;

            return View(matiere);
        }

        public IActionResult Creer(int? cid)
        {
            if (cid is null) return NotFound();
            var classe = context.Classes.FirstOrDefault(c => c.Id == cid);
            if (classe is null) return NotFound();

            var matiere = new MatiereModel
            {
                ClasseId = classe.Id
            };

            return View(matiere);
        }

        [HttpPost]
        public IActionResult Creer([Bind("Nom")] MatiereModel matiere, int cid)
        {
            if (ModelState.IsValid)
            {
                var classe = context.Classes.FirstOrDefault(c => c.Id == cid);
                if (classe is null) return NotFound();

                matiere.Classe = classe;
                matiere.Classe.Matieres.Add(matiere);

                context.Sauvegarder();

                logger.LogWarning("Admin {Admin} created matière {matiere}.", signInManager.Context.User.Identity.Name, matiere.NomComplet);

                return RedirectToAction("Index", "Classes", new { id = matiere.ClasseId });
            }
            return View(matiere);
        }

        public IActionResult Editer(int? id)
        {
            if (id == null) return NotFound();
            var matiere = context.Matieres.FirstOrDefault(m => m.Id == id);
            if (matiere == null) return NotFound();

            return View(matiere);
        }

        [HttpPost]
        public IActionResult Editer(int id, [Bind("Id,Nom,Contenu,ClasseId")] MatiereModel matiere)
        {
            if (id != matiere.Id)
            {
                return NotFound();
            }

            if (!context.Matieres.Any(m => m.Id == id))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                context.EditerMatiere(matiere);
                context.Sauvegarder();

                logger.LogWarning("Admin {Admin} updated matière {matiere}.", signInManager.Context.User.Identity.Name, matiere.NomComplet);


                return RedirectToAction("Index", "Classes", new { id = matiere.ClasseId });
            }

            return View(matiere);

        }

        public IActionResult Supprimer(int? id)
        {
            if (id is null) return NotFound();
            var matiere = context.Matieres.FirstOrDefault(m => m.Id == id);
            int classe_id = matiere.ClasseId;

            if (matiere is null) return NotFound();

            matiere.EstSupprime = true;

            context.Sauvegarder();

            logger.LogWarning("Superadmin {Admin} deleted matière {matiere}.", signInManager.Context.User.Identity.Name, matiere.NomComplet);

            return RedirectToAction("Index", "Classes", new { id = classe_id });
        }
    }
}
