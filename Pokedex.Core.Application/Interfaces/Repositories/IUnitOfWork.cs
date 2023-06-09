using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Core.Application.Interfaces.Repositories
{
    public interface IUnitOfWork: IDisposable
    {
        void Dispose();
        IGenericRepository<T> Repository<T>() where T : class;
    }
}
