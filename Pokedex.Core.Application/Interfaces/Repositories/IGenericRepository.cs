using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Core.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T>
     where T : class
    { 
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Guid Id);
        Task<T> FindWhere(Expression<Func<T, bool>> predicate, Expression<Func<T, dynamic>> include);
        Task<IEnumerable<T>> GetList(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        Task<bool> Add(T entity);
        Task<bool> Delete(Guid Id);
        Task<bool> Update(T entity);
        Task<bool> Exists(Expression<Func<T, bool>> predicate);
    }
}
