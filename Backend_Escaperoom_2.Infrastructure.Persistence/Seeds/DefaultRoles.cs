using Backend_Escaperoom_2.Application.Enums;
using Backend_Escaperoom_2.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Infrastructure.Persistence.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<Usuario> userManager, RoleManager<Role> roleManager)
        {
            //Seed Roles
            await CheckRoleAsync(roleManager, TiposUsuarios.Desarrollador.ToString());
            await CheckRoleAsync(roleManager, TiposUsuarios.Administrador.ToString());
            await CheckRoleAsync(roleManager, TiposUsuarios.Participante.ToString());
        }

        private static async Task CheckRoleAsync(RoleManager<Role> roleManager, string roleName)
        {
            bool roleExists = await roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await roleManager.CreateAsync(new Role
                {
                    Name = roleName
                });
            }
        }
    }
}
