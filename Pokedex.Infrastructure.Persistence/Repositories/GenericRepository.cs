using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Pokedex.Core.Application.Interfaces.Repositories;
using Pokedex.Core.Domain.Commons;
using Pokedex.Infrastructure.Persistence.Context;
using System.Linq.Expressions;

namespace Pokedex.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>
    where T : AuditableBaseEntity
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

        public async Task<bool> DeleteAll()
        {
            var objectToDelete = await _db.Set<T>().ToListAsync();
            _db.Set<T>().RemoveRange(objectToDelete);
            return await CommitChanges();
        }

        public async virtual Task<bool> Update(T entity)
        {
            var entry = await _db.Set<T>().FindAsync(entity.Id);

            if (entry == null)
            {
                return false;
            }

            entity.Created = entry.Created;
            entity.CreateBy = entry.CreateBy;

            _db.Entry(entry).CurrentValues.SetValues(entity);

           
            return await CommitChanges();
        }

        public async Task<T> FindWhere(Expression<Func<T, bool>> predicate, Expression<Func<T, dynamic>> include)
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

        public async Task<IEnumerable<T>> GetList(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            try
            {
                IQueryable<T> query = _db.Set<T>();

                if (include != null)
                {
                    query = include(query);
                }

                if (predicate != null)
                {
                    query = query.Where(predicate);
                }

                return await query.ToListAsync();
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
