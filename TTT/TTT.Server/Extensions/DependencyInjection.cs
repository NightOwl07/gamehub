using Microsoft.Extensions.DependencyInjection;
using TTT.Server.Contracts.Interfaces.Services;
using TTT.Server.Services;

namespace TTT.Server.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServerServiceCollection(this IServiceCollection services)
        {
            services.AddSingleton<IAccountService, AccountService>();
            services.AddSingleton<ICharacterService, CharacterService>();
            services.AddSingleton<INotificationService, NotificationService>();
            services.AddSingleton<IInventoryService, InventoryService>();
            return services;
        }
    }
}