using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Musicalog.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSignalRServices(this IServiceCollection services)
        {
            // services.AddSignalR()
            //         .AddNewtonsoftJsonProtocol(options =>
            //         {
            //             // Serialize enums as Pascal-case strings in DTOs
            //             options.PayloadSerializerSettings.Converters.Add(new StringEnumConverter(namingStrategy: new DefaultNamingStrategy(),
            //                                                                                      allowIntegerValues: false));
            //             options.PayloadSerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            //             CommonJsonSerializerSettings.Apply(options.PayloadSerializerSettings);
            //         });

            // services.Scan(scan =>
            // {
            //     scan.FromAssemblyOf<CallbackServiceBase>()
            //         .AddClasses(classes => classes.AssignableTo<CallbackServiceBase>())
            //         .AsImplementedInterfaces()
            //         .WithTransientLifetime();
            // });

            // services.AddSingleton<ICallbackUserManager, CallbackUserManager>();

            return services;
        }

        // public static IServiceCollection AddBootstrapperServices(this IServiceCollection services)
        // {
        //     services.AddTransient<IBootstrapper, Bootstrapper>();
        //     services.AddTransient<IMutexFactory, MutexWrapperFactory>();
        //     services.AddSingleton<IShutdownService, ShutdownService>();
        //     services.AddSingleton<ITestableScopeFactory, TestableScopeFactory>();
        //
        //     return services;
        // }

        // public static IServiceCollection AddAuthorizationHandlers(this IServiceCollection services)
        // {
        //     services.AddScoped<IResourceAuthorizer, ResourceAuthorizer>();
        //     services.AddScoped<IEndpointSourceAuthorizationService, EndpointSourceAuthorizationService>();
        //
        //     var executingAssembly = Assembly.GetExecutingAssembly();
        //     services.Scan(scan =>
        //     {
        //         scan.FromAssemblies(executingAssembly)
        //             .AddClasses(classes => classes.AssignableToAny(typeof(IAuthorizationHandler)))
        //             .AsImplementedInterfaces()
        //             .WithScopedLifetime();
        //     });
        //
        //     return services;
        // }
    }
}
