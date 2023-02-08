using Backend_Escaperoom_2.Application.Interfaces;
using Backend_Escaperoom_2.Domain.Entities;
using Backend_Escaperoom_2.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Infrastructure.Persistence.Repositories.GenericRepository
{
    public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepositoryAsync(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> GetByIdAsync(string id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            return await _dbContext.Set<T>().Skip((pageNumber - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetPagedReponseAsync(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate)
        {

            return await _dbContext.Set<T>().Where(predicate).Skip((pageNumber - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            //_dbContext.Entry(entity).State = EntityState.Deleted;
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _dbContext.Set<T>().CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).CountAsync();
        }

        /// <summary>
        /// Ejecuta el metodo Any para verificar que existan los Id en las tablas
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<bool> ExistElementAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().AnyAsync(predicate);
        }

        /// <summary>
        /// Ejecuta el metodo Any para verificar que existan los Id en las tablas
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<bool> ExistElementAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, bool>> wherePredicate)
        {
            return await _dbContext.Set<T>().Where(wherePredicate).AnyAsync(predicate);
        }

        /// <summary>
        /// Ejecuta el metodo All es usado para varificar que no existan atributos como nombres, descripcion....
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<bool> IsExistAttributeAsync(Expression<Func<T, bool>> predicateAll)
        {
            return await _dbContext.Set<T>().AllAsync(predicateAll);
        }

        /// <summary>
        /// Ejecuta el metodo All es usado para varificar que no existan atributos como nombres, descripcion....
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<bool> IsExistAttributeAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, bool>> wherePredicate)
        {
            return await _dbContext.Set<T>().Where(wherePredicate).AllAsync(predicate);
        }
    }
}
