using Backend_Escaperoom_2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Application.Interfaces.Repositories
{
    public interface IParticipantesRepositoryAsync : IGenericRepositoryAsync<Participante>
    {
        Task<Participante> GetParticipanteByIdFullAsync(int id);

        Task<Participante> GetParticipanteByIdentFullAsync(string identificacion, Expression<Func<Participante, bool>> predicate);

        Task<IEnumerable<Participante>> GetAllParticipantesFullAsync();

        Task<IEnumerable<Participante>> GetAllParticipantesFullAsync(Expression<Func<Participante, bool>> predicate);

        Task<IEnumerable<Participante>> GetPagedReponseFullAsync(int pageNumber, int pageSize);

        Task<IEnumerable<Participante>> GetPagedReponseFullAsync(int pageNumber, int pageSize, Expression<Func<Participante, bool>> predicate);

    }
}
