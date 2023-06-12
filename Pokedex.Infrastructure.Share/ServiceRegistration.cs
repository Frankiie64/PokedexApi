using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pokedex.Core.Application.Interfaces.Services;
using Pokedex.Infrastructure.Share.Services;

namespace Pokedex.Infrastructure.Share
{
    public static class ServiceRegistration
    {
        public static void AddShared(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IIdsService>(provider => new IdsService(configuration.GetValue<string>("UrlServicios:Ids")));

        }
    }
}

