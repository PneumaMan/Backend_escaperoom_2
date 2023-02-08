using Backend_Escaperoom_2.Application.Interfaces.Repositories;
using Backend_Escaperoom_2.Domain.Entities;
using Backend_Escaperoom_2.Infrastructure.Persistence.Contexts;
using Backend_Escaperoom_2.Infrastructure.Persistence.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Infrastructure.Persistence.Repositories
{
    public class RolesRepositoryAsync : GenericRepositoryAsync<Role>, IRolesRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;

        public RolesRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> NoExistRoleAsync(string id)
        {
            return await _dbContext.RolesDbSet.AllAsync(p => p.Id != id);
        }
    }
}
