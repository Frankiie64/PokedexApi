using Microsoft.EntityFrameworkCore;
using Pokedex.Core.Application.Interfaces.Repositories;
using Pokedex.Infrastructure.Persistence.Context;
using System.Linq.Expressions;

namespace Pokedex.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>
    where T : class
    {
        private readonly ApplicationDbContext _db;

        public GenericRepository(ApplicationDbContext db)
        {
            _db= db;
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await _db.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(Guid Id)
        {          
            return await _db.Set<T>().FindAsync(Id);
        }

        public async Task<bool> Add(T entity)
        {
            await _db.Set<T>().AddAsync(entity);
            return await CommitChanges();
        }

        public async Task<bool> Delete(Guid Id)
        {
            var objectToDelete = await _db.Set<T>().FindAsync(Id);
            _db.Set<T>().Remove(objectToDelete);
            return await CommitChanges();
        }

        public async virtual Task<bool> Update(T entity)
        {
            if (_db.Entry(entity).State == EntityState.Detached)
            {
                _db.Attach(entity);
            }
            _db.Entry(entity).State = EntityState.Modified;
            return await CommitChanges();
        }

        public async Task<T> FindWhere(Expression<Func<T, bool>> predicate = null, Expression<Func<T, dynamic>> include = null)
        {
            IQueryable<T> query = _db.Set<T>();

           
            if (predicate != null)
            {
                query.Where(predicate);
            }

            if (include != null)
            {
                query.Include(include);
            }

            return await query.FirstOrDefaultAsync();           
        }

        public async Task<IEnumerable<T>> GetList(Expression<Func<T, bool>> predicate = null, Expression<Func<T, dynamic>> include = null)
        {
            try
            {
                if (predicate != null && include != null)
                {
                    return await _db.Set<T>().Include(include).Where(predicate).ToListAsync();
                }
                if (predicate != null)
                {
                    return await _db.Set<T>().Where(predicate).ToListAsync();
                }
                if (include != null)
                {
                    return await _db.Set<T>().Include(include).ToListAsync();
                }

                return await _db.Set<T>().ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
        }
        public async Task<bool> Exists(Expression<Func<T, bool>> predicate)
        {
            if (predicate != null)
            {
                return await _db.Set<T>().AnyAsync(predicate);
            }

            return false;

        }
        private async Task<bool> CommitChanges()
        {
            return await _db.SaveChangesAsync() >= 0;
        }

    }
}
