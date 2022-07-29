using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using AltV.Net.EntitySync;
using AltV.Net.EntitySync.ServerEvent;
using AltV.Net.EntitySync.SpatialPartitions;
using AltV.Net.Interactions;
using CustomCommandsSystem.Integration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using NLog;
using NLog.Extensions.Logging;
using System.IO;
using TTT.Contracts.Interfaces.DependencyInjection;
using TTT.Core;
using TTT.Core.Entities.Factories;
using TTT.Database;
using TTT.EntitySync;
using TTT.Server.Extensions;

namespace TTT.Server
{
    public class Gamemode : AsyncResource
    {
        private InteractionsService interactionsService;

        private Logger logger = LogManager.GetCurrentClassLogger();

        public Gamemode()
            : base(new ActionTickSchedulerFactory())
        {
            this.Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();
            ConsoleFormatterOptions loggerOptions = new ConsoleFormatterOptions()
            {
               
            };
        }

        public IConfiguration Configuration { get; }

        public override void OnStart()
        {
            this.InitEntitySync();

            ServiceCollection services = new();

            services.AddSingleton(this.Configuration);
      
            services.AddLogging(config => config
                .ClearProviders()
                .SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Debug)
                .AddDebug()
                .AddConsole()
                .AddNLog(new NLogProviderOptions
                {
                    
                }));

            services.AddCoreServiceCollection();
            services.AddDatabaseServiceCollection();
            services.AddServerServiceCollection();

            services.AddAllTypes<ISingletonScript>(ServiceLifetime.Singleton);

            services.AddAllTypes<ISingletonCommand>(ServiceLifetime.Singleton);

            services.AddAllTypes<ITransientScript>();

            this.interactionsService = InteractionsService.CreateBuilder().Build();

            services.AddSingleton(this.interactionsService);

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            this.logger.Debug("Dependency Injection initialized successfully");

            serviceProvider.InstanciateStartupScripts();

            Settings.Config.ServiceProviderForInstances = serviceProvider;
            Alt.Core.RegisterCustomCommands();
        }

        public override void OnStop()
        {
            Alt.Core.UnregisterCustomCommands();
            this.interactionsService?.Dispose();
        }

        private void InitEntitySync()
        {
            AltEntitySync.Init(7, threadId => 100, threadId => false,
                (threadCount, repository) => new ServerEventNetworkLayer(threadCount, repository),
                (entity, threadCount) => entity.Id % threadCount,
                (entityId, entityType, threadCount) => entityId % threadCount,
                threadId =>
                {
                    return threadId switch
                    {
                        // Marker
                        0 => new LimitedGrid3(50_000, 50_000, 75, 10_000,
                            10_000, 64),
                        // Text
                        1 => new LimitedGrid3(50_000, 50_000, 75, 10_000,
                            10_000, 32),
                        // Props
                        2 => new LimitedGrid3(50_000, 50_000, 100, 10_000,
                            10_000, 1500),
                        // Help Text
                        3 => new LimitedGrid3(50_000, 50_000, 100, 10_000,
                            10_000, 1),
                        // Blips
                        4 => new GlobalEntity(),
                        // Dynamic Blip
                        5 => new LimitedGrid3(50_000, 50_000, 175, 10_000,
                            10_000, 200),
                        // Ped
                        6 => new LimitedGrid3(50_000, 50_000, 175, 10_000,
                            10_000, 64),
                        _ => new LimitedGrid3(50_000, 50_000, 175, 10_000,
                            10_000, 115)
                    };
                },
                new IdProvider());
        }

        #region Apply factories

        public override IEntityFactory<IPlayer> GetPlayerFactory()
        {
            return new TownPlayerFactory();
        }

        public override IEntityFactory<IVehicle> GetVehicleFactory()
        {
            return new TownVehicleFactory();
        }

        public override IBaseObjectFactory<IColShape> GetColShapeFactory()
        {
            return new TownColShapeFactory();
        }

        #endregion
    }
}