using Backend_Escaperoom_2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Application.Interfaces.Repositories
{
    public interface ITeamsRepositoryAsync : IGenericRepositoryAsync<Team>
    {
        Task<IEnumerable<Team>> GetFullTeam(int id);

        Task<IEnumerable<Team>> GetAllFullTeams();

        Task<IEnumerable<Team>> GetAllFullTeams(Expression<Func<Team, bool>> predicate);

        Task<IEnumerable<Team>> GetPagedReponseFullAsync(int pageNumber, int pageSize);

        Task<IEnumerable<Team>> GetPagedReponseFullAsync(int pageNumber, int pageSize, Expression<Func<Team, bool>> predicate);
    }
}
