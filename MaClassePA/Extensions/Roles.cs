namespace MaClassePA.Extensions
{
    using Microsoft.AspNetCore.Identity;

    using System.Threading.Tasks;

    public static class Roles
    {
        public static async Task EnsureCreated(this RoleManager<IdentityRole> manager, string roleName)
        {
            if (!await manager.RoleExistsAsync(roleName))
            {
                await manager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}
