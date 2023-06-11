using Pokedex.Infrastructure.Persistence.Context;

namespace Pokedex.Infrastructure.Persistence.Repositories
{
    public sealed class Singleton
    {
        public static readonly Singleton Instance = new Singleton();

        public LinkedList<KeyValuePair<string, object>> repositories = new LinkedList<KeyValuePair<string, object>>();
        public Dictionary<string, IDisposable> dbs = new Dictionary<string, IDisposable>();

        private Singleton(){}

       
    }
}
