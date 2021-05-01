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

    [Authorize(Authorizations.FullRessources)]
    public class RessourcesController : Controller
    {
        private readonly IClassesContext context;
        private readonly ILogger<MatieresController> logger;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly MarkdownParser markdownParser;

        public RessourcesController(IClassesContext _context, ILogger<MatieresController> _logger, SignInManager<IdentityUser> _signInManager, MarkdownParser _markdownParser)
        {
            context = _context;
            logger = _logger;
            signInManager = _signInManager;
            markdownParser = _markdownParser;
        }

        [AllowAnonymous]
        public IActionResult Index(int? id)
        {
            logger.LogDebug($"GET /Ressources/Index?id={id}");

            if (id == null) return NotFound(); // Id null
            var ressource = context.Ressources.FirstOrDefault(r => r.Id == id);
            if (ressource == null) return NotFound(); // Ressource doesn't exist

            return View(ressource);
        }

        public IActionResult Creer(int? mid)
        {
            logger.LogDebug($"GET /Ressources/Creer?mid={mid}");

            if (mid == null) return NotFound();
            var matiere = context.Matieres.FirstOrDefault(m => m.Id == mid);
            if (matiere == null) return NotFound();

            var ressource = new RessourceModel 
            { 
                MatiereId = matiere.Id 
            };

            return View(ressource);
        }

        [HttpPost]
        public IActionResult Creer([Bind("Nom,Contenu")] RessourceModel ressource, int mid)
        {
            logger.LogDebug($"POST /Ressources/Creer?mid={mid}");

            if (!ModelState.IsValid) return View(ressource); // Invalid inputs
            var matiere = context.Matieres.FirstOrDefault(m => m.Id == mid);
            if (matiere == null) return NotFound();

            ressource.Contenu ??= "";
            ressource.Matiere = matiere;
            ressource.Rendu = markdownParser.Render(ressource.Contenu);
            ressource.Matiere.Ressources.Add(ressource);
            context.Sauvegarder();

            logger.LogInformation("User {user} created ressource \"{ressource}\".", signInManager.Context.User.Identity.Name, ressource.NomComplet);

            return RedirectToAction("Index", "Matieres", new { id = ressource.MatiereId });
        }

        public IActionResult Editer(int? id)
        {
            logger.LogDebug($"GET /Ressources/Editer?id={id}");

            if (id == null) return NotFound(); // Id null
            var res = context.Ressources.FirstOrDefault(r => r.Id == id);
            if (res == null) return NotFound(); // Ressource doesn't exist

            return View(res);
        }

        [HttpPost]
        public IActionResult Editer([Bind("Id,Nom,Contenu,MatiereId,Created,Modified")] RessourceModel ressource, int id)
        {
            logger.LogDebug($"POST /Ressources/Editer?id={id}");

            if (id != ressource.Id) return NotFound(); // Bad ressource id
            if (!context.Ressources.Any(r => r.Id == id)) return NotFound(); // Ressource doesn't exist
            if (!ModelState.IsValid) return View(ressource); // Invalid inputs

            ressource.Contenu ??= "";
            ressource.Rendu = markdownParser.Render(ressource.Contenu);
            context.EditerRessource(ressource);
            context.Sauvegarder();

            logger.LogInformation("User {user} updated ressource \"{ressource}\".", signInManager.Context.User.Identity.Name, ressource.NomComplet);

            return RedirectToAction("Index", "Matieres", new { id = ressource.MatiereId });

        }

        public IActionResult Supprimer(int? id)
        {
            logger.LogDebug($"GET /Ressources/Supprimer?id={id}");

            if (id is null) return NotFound(); // Id null
            var ressource = context.Ressources.FirstOrDefault(r => r.Id == id);
            if (ressource == null) return NotFound(); // Ressource doesn't exist

            ressource.EstSupprime = true;
            context.Sauvegarder();

            logger.LogWarning("User {user} deleted ressource \"{ressource}\".", signInManager.Context.User.Identity.Name, ressource.NomComplet);

            return RedirectToAction("Index", "Matieres", new { id = ressource.MatiereId });
        }
    }
}
