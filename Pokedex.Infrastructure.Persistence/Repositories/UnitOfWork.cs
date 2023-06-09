using Microsoft.EntityFrameworkCore;
using Pokedex.Core.Application.Interfaces.Repositories;
using Pokedex.Infrastructure.Persistence.Context;

namespace Pokedex.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private Dictionary<string, object> repositories;
        private ApplicationDbContext _db { get; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
        }

        public IGenericRepository<T> Repository<T>() where T : class
        {
            if (repositories == null)
            {
                repositories = new Dictionary<string, object>();
            }

            var type = typeof(T).Name;

            if (!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                object repositoryInstance = null;

                repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _db);
                repositories.Add(type, repositoryInstance);

            }

            return (GenericRepository<T>)repositories[type];
        }
        public void Dispose()
        {
            _db.Dispose();
        }
    }
}


