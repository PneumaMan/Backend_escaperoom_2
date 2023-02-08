using Backend_Escaperoom_2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Application.Interfaces.Repositories
{
    public interface IUsuarioRepositoryAsync
    {
        Task<Usuario> GetUserByIdFullAsync(string id);

        Task<Usuario> GetUserByEmailFullAsync(string email);

        Task<IEnumerable<Usuario>> GetAllUsersAsync();

        Task<IEnumerable<Usuario>> GetPagedReponseAsync(int pageNumber, int pageSize);

        Task<int> CountUsuariosAsync();

        Task<bool> IsExistAttributeAsync(Expression<Func<Usuario, bool>> predicate);
    }
}
