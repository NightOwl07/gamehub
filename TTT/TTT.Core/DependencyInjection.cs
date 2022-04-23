using Microsoft.Extensions.DependencyInjection;
using TTT.Core.Contracts.Interfaces.Menu;
using TTT.Core.Menu;

namespace TTT.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCoreServiceCollection(this IServiceCollection services)
        {
            services.AddSingleton<IMenuBuilder, MenuBuilder>();
            return services;
        }
    }
}