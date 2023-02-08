using Backend_Escaperoom_2.Application.Interfaces.Repositories;
using Backend_Escaperoom_2.Domain.Entities;
using Backend_Escaperoom_2.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Infrastructure.Persistence.Repositories
{
    public class UsuarioRepositoryAsync : IUsuarioRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;

        public UsuarioRepositoryAsync(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Usuario> GetUserByIdFullAsync(string id)
        {
            return await _dbContext.UsuariosDbSet.FirstOrDefaultAsync(u => u.Id.Equals(id));
        }

        public async Task<Usuario> GetUserByEmailFullAsync(string email)
        {
            return await _dbContext.UsuariosDbSet.FirstOrDefaultAsync(u => u.Email.ToLower().Equals(email));
        }

        public async Task<IEnumerable<Usuario>> GetAllUsersAsync()
        {
            var res = await _dbContext.UsuariosDbSet.ToListAsync();
            return res;
        }

        public async Task<IEnumerable<Usuario>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            return await _dbContext.UsuariosDbSet.Skip((pageNumber - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync();
        }

        public async Task<int> CountUsuariosAsync()
        {
            return await _dbContext.UsuariosDbSet.CountAsync();
        }

        public async Task<bool> IsExistAttributeAsync(Expression<Func<Usuario, bool>> predicate)
        {
            return await _dbContext.UsuariosDbSet.AllAsync(predicate);
        }
    }
}
