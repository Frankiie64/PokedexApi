using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Pokedex.Core.Application.Interfaces.Repositories;
using Pokedex.Core.Domain.Commons;
using Pokedex.Infrastructure.Persistence.Context;
using Pokedex.Infrastructure.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public ApplicationDbContext Db { get; }

    public UnitOfWork(DbContextOptions<ApplicationDbContext> opt, IHttpContextAccessor http)
    {
        Db = new ApplicationDbContext(opt, http);
    }

    public IGenericRepository<T> Repository<T>() where T : AuditableBaseEntity
    {
        var type = typeof(T).Name;
        var repository = Singleton.Instance.repositories.FirstOrDefault(r => r.Key == type);

        if (repository.Value == null)
        {
            var repositoryType = typeof(GenericRepository<>);
            object repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), Db);
            repository = new KeyValuePair<string, object>(type, repositoryInstance);
            Singleton.Instance.repositories.AddLast(repository);
            Singleton.Instance.dbs.Add(type, Db);
        }
        else
        {
            Singleton.Instance.repositories.Remove(repository);
            Singleton.Instance.repositories.AddLast(repository);
        }

        return (GenericRepository<T>)repository.Value;
    }

    public void Dispose()
    {
        if (Singleton.Instance.repositories.Count > 2)
        {
            var oldestRepository = Singleton.Instance.repositories.First();
            var disposableRepository = Singleton.Instance.dbs.FirstOrDefault(x =>x.Key == oldestRepository.Key).Value;

            Singleton.Instance.repositories.Remove(oldestRepository);
            disposableRepository.Dispose();

        }

    }
}
