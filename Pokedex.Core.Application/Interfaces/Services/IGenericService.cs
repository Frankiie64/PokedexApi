using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Core.Application.Interfaces.Services
{
    public interface IGenericService<sv,dto,model>
        where dto : class
        where model : class
        where sv : class
    {
        Task<IEnumerable<dto>> GetAll();
        Task<dto> GetById(Guid Id);
        Task<dto> FindWhere(Expression<Func<model, bool>> predicate, Expression<Func<model, dynamic>> include);
        Task<IEnumerable<dto>> GetList(Expression<Func<model, bool>> predicate = null, Expression<Func<model, dynamic>> include = null);
        Task<bool> Add(sv entity);
        Task<bool> Delete(Guid Id);
        Task<bool> Update(sv entity);
        Task<bool> Exists(Expression<Func<model, bool>> predicate);
    }
}
