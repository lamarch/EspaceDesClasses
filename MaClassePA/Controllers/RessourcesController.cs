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

            if (id == null)
            {
                return NotFound();
            }

            var ressource = context.Ressources.FirstOrDefault(r => r.Id == id);

            if (ressource == null)
            {
                return NotFound();
            }

            return View(ressource);
        }

        public IActionResult Creer(int? mid)
        {
            if (mid == null) return NotFound();
            var matiere = context.Matieres.FirstOrDefault(m => m.Id == mid);
            if (matiere == null) return NotFound();

            var ressource = new RessourceModel { Matiere = matiere, MatiereId = matiere.Id };

            return View(ressource);
        }

        [HttpPost]
        public IActionResult Creer([Bind("Nom,Contenu")] RessourceModel ressource, int mid)
        {
            if (ModelState.IsValid)
            {
                var matiere = context.Matieres.FirstOrDefault(m => m.Id == mid);
                if (matiere == null) return NotFound();

                ressource.Matiere = matiere;
                ressource.Rendu = markdownParser.Render(ressource.Contenu);

                ressource.Matiere.Ressources.Add(ressource);

                context.Sauvegarder();

                logger.LogWarning("Rédacteur {redacteur} created ressource {ressource}.", signInManager.Context.User.Identity.Name, ressource.NomComplet);

                return RedirectToAction("Index", "Matieres", new { id = ressource.Matiere.Id } );
            }
            return View(ressource);
        }

        public IActionResult Editer(int? id)
        {
            if (id == null) return NotFound();
            var res = context.Ressources.FirstOrDefault(r => r.Id == id);
            if (res == null) return NotFound();

            return View(res);
        }

        [HttpPost]
        public IActionResult Editer(int id, [Bind("Id,Nom,Contenu,MatiereId,Created,Modified")] RessourceModel ressource)
        {
            if(id != ressource.Id)
            {
                return NotFound();
            }

            if (!context.Ressources.Any(r => r.Id == id))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                ressource.Rendu = markdownParser.Render(ressource.Contenu);
                context.EditerRessource(ressource);
                context.Sauvegarder();

                logger.LogWarning("Rédacteur {redacteur} updated ressource {ressource}.", signInManager.Context.User.Identity.Name, ressource.NomComplet);

                return RedirectToAction("Index", "Matieres", new { id = ressource.MatiereId });
            }

            return View(ressource);

        }

        public IActionResult Supprimer(int? id)
        {
            if (id is null) return NotFound();
            var ressource = context.Ressources.FirstOrDefault(r => r.Id == id);
            int matiere_id = ressource.Matiere.Id;

            if (ressource == null)
            {
                return NotFound();
            }

            ressource.EstSupprime = true;

            context.Sauvegarder();

            logger.LogWarning("Admin {admin} deleted ressource {ressource}.", signInManager.Context.User.Identity.Name, ressource.NomComplet);

            return RedirectToAction("Index", "Matieres", new { id = matiere_id });
        }
    }
}
