using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Musicalog.Core.Services;

namespace Musicalog.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            services.Scan(scan =>
            {
                scan.FromAssemblies(executingAssembly)
                    .AddClasses(classes => classes.AssignableToAny(typeof(IRequestHandler<>), typeof(IRequestHandler<,>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime();
            });

            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), ServiceLifetime.Singleton);
            return services;
        }

        public static IServiceCollection AddConverters(this IServiceCollection services)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            services.Scan(scan =>
            {
                scan.FromAssemblies(executingAssembly)
                    
                    .AddClasses(classes => classes.AssignableToAny(typeof(IConverter<,>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime();
            });

            return services;
        }

        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddTransient<IAlbumService, AlbumService>();
            
            // Other services...
            
            return services;
        }
    }
}
