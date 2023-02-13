using Backend_Escaperoom_2.Application.Interfaces.Repositories;
using Backend_Escaperoom_2.Domain.Entities;
using Backend_Escaperoom_2.Infrastructure.Persistence.Contexts;
using Backend_Escaperoom_2.Infrastructure.Persistence.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Infrastructure.Persistence.Repositories
{

    public class TeamsRepositoryAsync : GenericRepositoryAsync<Team>, ITeamsRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;

        public TeamsRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Team>> GetFullTeam(int id)
        {
            return await _dbContext.TeamsDbSet.OrderByDescending(x => x.Id)
                .Include(x => x.Participantes)
                .Where(x => x.Id == id).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Team>> GetAllFullTeams()
        {
            return await _dbContext.TeamsDbSet.OrderByDescending(x => x.Id)
                .Include(x => x.Participantes)
                .AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Team>> GetAllFullTeams(Expression<Func<Team, bool>> predicate)
        {
            return await _dbContext.TeamsDbSet.OrderByDescending(x => x.Id)
                .Include(x => x.Participantes)
                .Where(predicate).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Team>> GetPagedReponseFullAsync(int pageNumber, int pageSize)
        {
            return await _dbContext.TeamsDbSet.OrderByDescending(x => x.Id)
                .Include(x => x.Participantes)
                .Skip((pageNumber - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Team>> GetPagedReponseFullAsync(int pageNumber, int pageSize, Expression<Func<Team, bool>> predicate)
        {
            return await _dbContext.TeamsDbSet.Where(predicate).OrderByDescending(x => x.Id)
                .Include(x => x.Participantes)
                .Skip((pageNumber - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync();
        }
    }
}
