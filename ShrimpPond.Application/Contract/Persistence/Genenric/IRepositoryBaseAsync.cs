using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Contract.Persistence.Genenric
{
    public interface IRepositoryQueryBase<T, Key>
    where T : class
    {
        IQueryable<T> FindAll(bool trackChanges = false);
        IQueryable<T> FindAll(bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties);

        Task<T?> GetByIdAsync(Key id);

    }

    public interface IRepositoryBaseAsync<T, K> : IRepositoryQueryBase<T, K>
        where T : class
    {
        void Update(T entity);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
