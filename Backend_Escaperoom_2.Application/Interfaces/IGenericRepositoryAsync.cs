using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Application.Interfaces
{
    public interface IGenericRepositoryAsync<T> where T : class
    {
        Task<T> GetByIdAsync(int id);

        Task<T> GetByIdAsync(string id);

        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> GetPagedReponseAsync(int pageNumber, int pageSize);

        Task<IEnumerable<T>> GetPagedReponseAsync(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate);

        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task<int> CountAsync();

        Task<int> CountAsync(Expression<Func<T, bool>> predicate);

        Task<bool> ExistElementAsync(Expression<Func<T, bool>> predicate);

        Task<bool> ExistElementAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, bool>> wherePredicate);

        Task<bool> IsExistAttributeAsync(Expression<Func<T, bool>> predicate);

        Task<bool> IsExistAttributeAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, bool>> wherePredicate);
    }
}
