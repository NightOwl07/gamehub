using Microsoft.Extensions.DependencyInjection;
using TTT.Database.Contracts.Interfaces.Repositories;
using TTT.Database.Repositories;

namespace TTT.Database
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDatabaseServiceCollection(this IServiceCollection services)
        {
            services.AddSingleton<IAccountRepository, AccountRepository>();
            return services;
        }
    }
}