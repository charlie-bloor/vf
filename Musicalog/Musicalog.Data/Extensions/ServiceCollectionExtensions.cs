﻿using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Musicalog.Data.Contexts;
using Musicalog.Data.Repositories;

namespace Musicalog.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<IMusicalogContext, MusicalogContext>(o =>
            {
                o.UseSqlServer(connectionString);
            });

            //services.AddSingleton<IPager, Pager>();

            var executingAssembly = Assembly.GetExecutingAssembly();

            services.Scan(scan =>
            {
                scan.FromAssemblies(executingAssembly)
                    .AddClasses(classes => classes.AssignableToAny(typeof(IRepositoryBase<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime();
            });

            // services.Scan(scan =>
            // {
            //     scan.FromAssemblies(executingAssembly)
            //         .AddClasses(classes => classes.AssignableToAny(typeof(IConverter<,>),
            //                                                        typeof(IConverter<,,>),
            //                                                        typeof(IConverter<,,,>),
            //                                                        typeof(IConverter<,,,,>),
            //                                                        typeof(IConverter<,,,,,>)))
            //         .AsImplementedInterfaces()
            //         .WithTransientLifetime();
            // });

            return services;
        }
    }
}
