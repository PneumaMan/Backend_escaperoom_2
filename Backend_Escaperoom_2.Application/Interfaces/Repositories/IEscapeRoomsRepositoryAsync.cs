using Backend_Escaperoom_2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Application.Interfaces.Repositories
{
    public interface IEscapeRoomsRepositoryAsync : IGenericRepositoryAsync<EscapeRoom>
    {
        Task<EscapeRoom> GetEscapeRoomByIdFullAsync(int id);


        Task<EscapeRoom> GetEscapeRoomByIdAsync(int id);

        Task<IEnumerable<EscapeRoom>> GetAllFullEscapeRooms();

        Task<IEnumerable<EscapeRoom>> GetPagedReponseFullAsync(int pageNumber, int pageSize);

        Task<IEnumerable<EscapeRoom>> GetPagedReponseFullAsync(int pageNumber, int pageSize, Expression<Func<EscapeRoom, bool>> predicate);

    }
}
