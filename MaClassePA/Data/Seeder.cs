namespace MaClassePA.Data
{
    using MaClassePA.Extensions;
    using MaClassePA.Models;
    using MaClassePA.Models.Compte;
    using MaClassePA.Models.Configuration;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public static class Seeder
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ClassesDbContext(serviceProvider.GetRequiredService<DbContextOptions<ClassesDbContext>>()))
            {
                await InitializeDbContext(context);
            }

            using (var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>())
            {
                await InitializeRoles(roleManager);
            }

            using (var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>())
            {

                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                await InitializeAdmins(userManager, configuration);
            }

            
        }

        private static async Task InitializeAdmins(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            var accounts = configuration.GetSection("Accounts").Get<AccountsModel>();

            foreach(var acc in accounts.Passwords)
            {
                var name = acc.Key;
                var password = acc.Value;
                var found = await userManager.FindByNameAsync(name);

                if (found == null)
                {
                    var result = await userManager.CreateAsync(new IdentityUser() { UserName = name, Email = name }, password);
                }
            }

            foreach (var acc in accounts.Permissions)
            {
                await userManager.EnsureHasRole(acc.Key, acc.Value);
            }
        }

        private static async Task InitializeRoles(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.EnsureCreated("Superadmin");
            await roleManager.EnsureCreated("Admin");
            await roleManager.EnsureCreated("Redacteur");
        }

        private static async Task InitializeDbContext(ClassesDbContext context)
        {
            if (context.Classes.Any()) return;

            var classe = new ClasseModel
            {
                Nom = "1G",
                Matieres = new()
            };

            var matiere = new MatiereModel
            {
                Nom = "Anglais",
                Ressources = new(),
                Classe = classe
            };

            classe.Matieres.Add(matiere);

            var res = new RessourceModel
            {
                Nom = "Cours numéro 12",
                Contenu = "du blabla",
                Matiere = matiere
            };

            matiere.Ressources.Add(res);

            context.AjouterClasse(classe);

            await context.SaveChangesAsync();
        }
    }
}
