using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Musicalog.Core.Extensions;
using Musicalog.Data.Extensions;
using Musicalog.Middleware;
using Newtonsoft.Json.Converters;
using ZymLabs.NSwag.FluentValidation;

namespace Musicalog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    // Ensure enums are sent/received as strings in DTOs
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(namingPolicy: null, allowIntegerValues: false));
                })
                // Show enums as strings in Swagger
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                });
            
            services.AddConverters();
            services.AddValidators();
            services.AddDataAccess(Configuration.GetConnectionString("Musicalog"));
            services.AddCoreServices();
            services.AddHandlers();
            
            // Add support FluentValidation to show up in Swagger output
            ValidatorOptions.Global.DisplayNameResolver = (type, member, expression) => member?.Name;
            services.AddSingleton<FluentValidationSchemaProcessor>();
            services.AddSingleton<IValidatorFactory, ServiceProviderValidatorFactory>();
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddOpenApiDocument((settings, serviceProvider) =>
            {
                settings.Title = "Musicalog API";

                // Add the FluentValidation schema processor
                 var fluentValidationSchemaProcessor = serviceProvider.GetService<FluentValidationSchemaProcessor>();
                settings.SchemaProcessors.Add(fluentValidationSchemaProcessor);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseOpenApi();
            app.UseSwaggerUi3();
        }
    }
}