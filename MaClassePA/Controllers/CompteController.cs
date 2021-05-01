#nullable enable

namespace MaClassePA.Controllers
{
    using MaClassePA.Extensions;
    using MaClassePA.Models.Compte;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CompteController : Controller
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ILogger<CompteController> logger;

        public CompteController(SignInManager<IdentityUser> _signInManager, UserManager<IdentityUser> _userManager, ILogger<CompteController> _logger)
        {
            signInManager = _signInManager;
            userManager = _userManager;
            logger = _logger;
        }

        public IActionResult Index()
        {
            logger.LogDebug($"GET /Compte/Index");

            return RedirectToAction(nameof(In));
        }

        public IActionResult In(string? url_retour)
        {
            logger.LogDebug($"GET /Compte/In?url_retour={url_retour}");

            return View(new ConnecterModel { UrlRetour = url_retour });
        }

        [HttpPost]
        public async Task<IActionResult> In(ConnecterModel entree)
        {
            logger.LogDebug($"POST /Compte/In");

            await signInManager.SignOutAsync();

            if (!ModelState.IsValid) return View(entree); //Invalid inputs

            var result = await signInManager.PasswordSignInAsync(entree.Nom, entree.MotDePasse, entree.SeSouvenir, false);

            if (result.Succeeded)
            {
                if (signInManager.Context.User.IsInRole("Admin"))
                {
                    logger.LogWarning("Admin {Admin} logged in.", entree.Nom);
                }
                else
                {
                    logger.LogInformation("User {User} logged in.", entree.Nom);
                }
                return LocalRedirect(Uri.UnescapeDataString(entree.UrlRetour ?? "/"));
            }
            
            ModelState.AddModelError(string.Empty, "Identifiants incorrects.");
            return View(entree);
        }

        public async Task<IActionResult> Out(string? url_retour)
        {
            logger.LogDebug($"GET /Compte/Out?url_retour={url_retour}");

            if (signInManager.Context.User.IsInRole("Admin"))
            {
                logger.LogWarning("Admin {Admin} logged out.", signInManager.Context.User.Identity?.Name);
            }
            else
            {
                logger.LogInformation("User {User} logged out.", signInManager.Context.User.Identity?.Name);
            }

            await signInManager.SignOutAsync();
            return LocalRedirect(Uri.UnescapeDataString(url_retour ?? "/"));
        }

        public IActionResult Creer(string? url_retour)
        {
            logger.LogDebug($"GET /Compte/Creer?url_retour={url_retour}");

            return View(new EnregistrerModel { UrlRetour = url_retour });
        }

        [HttpPost]
        public async Task<IActionResult> Creer(Models.Compte.EnregistrerModel entree)
        {
            logger.LogDebug($"POST /Compte/Creer");

            if (!ModelState.IsValid) return View(entree); // Invalid inputs

            var result = await userManager.CreateAsync(new IdentityUser(entree.Nom), entree.MotDePasse);

            if (result.Succeeded)
            {
                await signInManager.PasswordSignInAsync(entree.Nom, entree.MotDePasse, false, false);

                logger.LogWarning("User {User} created an account.", entree.Nom);

                return LocalRedirect(Uri.UnescapeDataString(entree.UrlRetour ?? "/"));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(entree);
        }

        public IActionResult Interdit(string? url_retour)
        {
            logger.LogDebug($"GET /Compte/Interdit?url_retour={url_retour}");

            return View(new InterditModel() { UrlRetour = url_retour ?? "%2F" });
        }
    }
}
