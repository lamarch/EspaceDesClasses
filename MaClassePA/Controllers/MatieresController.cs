namespace MaClassePA.Controllers
{
    using MaClassePA.Data;
    using MaClassePA.Models;
    using MaClassePA.Services;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
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
            logger.LogDebug($"GET /Matieres/Index?id={id}");

            if (id == null) return NotFound(); // Id null
            var matiere = context.Matieres.FirstOrDefault(m => m.Id == id);
            if (matiere == null) return NotFound(); // Matiere doesn't exist

            // Authorizations
            ViewBag.CanUpdateRessources = (await authorizationService.AuthorizeAsync(User, Authorizations.FullMatieres)).Succeeded;
            ViewBag.CanDeleteRessources = (await authorizationService.AuthorizeAsync(User, Authorizations.FullMatieres)).Succeeded;
            ViewBag.CanCreateRessources = (await authorizationService.AuthorizeAsync(User, Authorizations.FullMatieres)).Succeeded;

            return View(matiere);
        }

        public IActionResult Creer(int? cid)
        {
            logger.LogDebug($"GET /Matieres/Creer?cid={cid}");

            if (cid is null) return NotFound(); // Classe id null
            var classe = context.Classes.FirstOrDefault(c => c.Id == cid);
            if (classe is null) return NotFound(); // Classe doesn't exist

            var matiere = new MatiereModel
            {
                ClasseId = classe.Id
            };

            return View(matiere);
        }

        [HttpPost]
        public IActionResult Creer([Bind("Nom")] MatiereModel matiere, int cid)
        {
            logger.LogDebug($"POST /Matieres/Creer?cid={cid}");

            if (!ModelState.IsValid) return View(matiere); // Invalid inputs
            var classe = context.Classes.FirstOrDefault(c => c.Id == cid);
            if (classe is null) return NotFound();

            matiere.Classe = classe;
            matiere.Classe.Matieres.Add(matiere);
            context.Sauvegarder();

            logger.LogInformation("User {user} created matière \"{matiere}\".", signInManager.Context.User.Identity.Name, matiere.NomComplet);

            return RedirectToAction("Index", "Classes", new { id = matiere.ClasseId });
        }

        public IActionResult Editer(int? id)
        {
            logger.LogDebug($"GET /Matieres/Editer?id={id}");

            if (id == null) return NotFound(); // Id null
            var matiere = context.Matieres.FirstOrDefault(m => m.Id == id);
            if (matiere == null) return NotFound(); // Matiere doesn't exist

            return View(matiere);
        }

        [HttpPost]
        public IActionResult Editer([Bind("Id,Nom,Contenu,ClasseId")] MatiereModel matiere, int id)
        {
            logger.LogDebug($"POST /Matieres/Editer?id={id}");

            if (id != matiere.Id) return NotFound(); // Bad Matiere Id
            if (!context.Matieres.Any(m => m.Id == id)) return NotFound(); // Matieres doesn't exist
            if (!ModelState.IsValid) return View(matiere); // Invalid inputs

            context.EditerMatiere(matiere);
            context.Sauvegarder();

            logger.LogInformation("User {user} updated matière \"{matiere}\".", signInManager.Context.User.Identity.Name, matiere.NomComplet);

            return RedirectToAction("Index", "Classes", new { id = matiere.ClasseId });

        }

        public IActionResult Supprimer(int? id)
        {
            logger.LogDebug($"GET /Matieres/Supprimer?id={id}");

            if (id is null) return NotFound(); // Id null
            var matiere = context.Matieres.FirstOrDefault(m => m.Id == id);
            if (matiere is null) return NotFound(); // Matiere doesn't exist

            matiere.EstSupprime = true;
            context.Sauvegarder();

            logger.LogWarning("User {user} deleted matière \"{matiere}\".", signInManager.Context.User.Identity.Name, matiere.NomComplet);

            return RedirectToAction("Index", "Classes", new { id = matiere.ClasseId });
        }
    }
}
