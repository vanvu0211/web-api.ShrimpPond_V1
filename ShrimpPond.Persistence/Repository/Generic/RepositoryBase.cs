using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Persistence.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ShrimpPond.Persistence.Repository.Generic
{
    public class RepositoryBase<T, Key> : RepositoryQueryBase<T, Key>,
     IRepositoryBaseAsync<T, Key>
     where T : class
    {
        protected readonly ShrimpPondDbContext _context;

        public RepositoryBase(ShrimpPondDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(_context));

        }

        public void Add(T entity)
        {
            _context.Set<T>().AddAsync(entity);
        }
        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRangeAsync(entities);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
