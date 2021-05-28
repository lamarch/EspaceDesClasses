namespace MaClassePA.Extensions
{
    using Microsoft.AspNetCore.Identity;

    using System.Threading.Tasks;

    public static class Users
    {
        private static async Task<IdentityResult> EnsureExist(this UserManager<IdentityUser> manager, string name, string password)
        {
            var found = await manager.FindByNameAsync(name);
            if (found == null)
            {
                var user = new IdentityUser(name) { Email = name };
                return await manager.CreateAsync(user, password);
            }
            return IdentityResult.Success;
        }

        public static async Task EnsureHasRole(this UserManager<IdentityUser> manager, string name, string roleName)
        {
            var user = await manager.FindByNameAsync(name);
            if (user != null && !await manager.IsInRoleAsync(user, roleName))
            {
                await manager.AddToRoleAsync(user, roleName);
            }
        }
    }
}
