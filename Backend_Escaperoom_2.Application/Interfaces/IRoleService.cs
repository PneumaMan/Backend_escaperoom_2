using Backend_Escaperoom_2.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Application.Interfaces
{
    public interface IRoleService
    {
        Task<Role> GetRoleById(string id);

        Task<Role> GetRoleByName(string roleName);

        Task<IEnumerable<Role>> GetAllRoles();
    }
}
