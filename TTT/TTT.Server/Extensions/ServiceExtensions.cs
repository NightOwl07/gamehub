using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TTT.Server.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private static readonly ILogger _logger;
        private static readonly Dictionary<Type, object> _singletonInstances;

        private static readonly List<Type> servicesToInstanciate = new();

        static ServiceCollectionExtensions()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddConfiguration(config.GetSection("Logging"))
                    .AddDebug()
                    .AddConsole();
            });

            _logger = loggerFactory.CreateLogger(typeof(ServiceCollectionExtensions));

            _singletonInstances = new Dictionary<Type, object>();
        }

        public static void AddAllTypes<T>(this IServiceCollection services,
            ServiceLifetime lifetime = ServiceLifetime.Transient)
            where T : class
        {
            #region T is interface

            IEnumerable<TypeInfo> typesOfInterface = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(t => t.DefinedTypes)
                .Where(x => x.IsClass && !x.IsAbstract && x.GetInterfaces().Contains(typeof(T)));

            foreach (TypeInfo type in typesOfInterface)
            {
                _logger.LogDebug(
                    $"Registering {type.Name} (implements interface {typeof(T).Name}) with lifetime {Enum.GetName(lifetime)}");

                if (services.Any(e => e.ServiceType == type))
                {
                    _logger.LogDebug($"Skipping registration of {type.Name} -> already registered!");
                    continue;
                }

                if (lifetime == ServiceLifetime.Singleton) servicesToInstanciate.Add(type);

                // add as resolvable by implementation type
                services.Add(new ServiceDescriptor(type, type, lifetime));

                if (typeof(T) != type)
                    // add as resolvable by service type (forwarding)
                    services.Add(new ServiceDescriptor(typeof(T), x => x.GetRequiredService(type), lifetime));
            }

            #endregion

            #region T is class

            IEnumerable<Type> typesOfClasses = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(t => t.GetTypes())
                .Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(typeof(T)));

            foreach (Type type in typesOfClasses)
            {
                _logger.LogDebug(
                    $"Registering {type.Name} (inherits class {typeof(T).Name}) with lifetime {Enum.GetName(lifetime)}");

                if (services.Any(e => e.ServiceType == type))
                {
                    _logger.LogDebug($"Skipping registration of {type.Name} -> already registered!");
                    continue;
                }

                if (lifetime == ServiceLifetime.Singleton) servicesToInstanciate.Add(type);

                // add as resolvable by implementation type
                services.Add(new ServiceDescriptor(type, type, lifetime));

                if (typeof(T) != type)
                    // add as resolvable by service type (forwarding)
                    services.Add(new ServiceDescriptor(typeof(T), x => x.GetRequiredService(type), lifetime));
            }

            #endregion
        }

        private static object GetSingletonInstance(IServiceProvider serviceProvider, Type type)
        {
            if (_singletonInstances.ContainsKey(type)) return _singletonInstances[type];

            object instance = serviceProvider.GetRequiredService(type);
            _singletonInstances.Add(type, instance);

            return instance;
        }

        public static void InstanciateStartupScripts(this ServiceProvider provider)
        {
            _logger.LogDebug("Dependency Injection: Instanciating registered scripts");

            foreach (Type type in servicesToInstanciate)
            {
                _ = provider.GetService(type);

                _logger.LogDebug($"Instanciated {type.Name}");
            }
        }
    }
}