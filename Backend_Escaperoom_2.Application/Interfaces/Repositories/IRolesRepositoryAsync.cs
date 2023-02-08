using Backend_Escaperoom_2.Domain.Entities;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Application.Interfaces.Repositories
{
    public interface IRolesRepositoryAsync : IGenericRepositoryAsync<Role>
    {
        Task<bool> NoExistRoleAsync(string id);
    }
}
