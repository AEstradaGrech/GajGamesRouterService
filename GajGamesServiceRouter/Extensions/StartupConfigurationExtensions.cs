using System;
using System.Net;
using Consul;
using GajGamesServiceRouter.Infrastructure.ApiConfigurations;
using GajGamesServiceRouter.Infrastructure.ConsulConfig;
using GajGamesServiceRouter.Infrastructure.Dtos;
using GajGamesServiceRouter.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GajGamesServiceRouter.Extensions
{
    public static class StartupConfigurationExtensions
    {

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IGajImgsRestService, GajImgsRestService>();
            services.AddScoped<IGajUsersRestService, GajUsersRestService>();
            services.AddScoped<IGajStoreRestService, GajStoreRestService>();
            services.AddScoped<IGajStoreMgmtService, GajStoreMgmtService>();

            return services;
        }

        public static IServiceCollection AddApiConfigurations(this IServiceCollection services, IConfiguration config)
        {
            return services.Configure<GajImgsApiConfiguration>(config.GetSection(nameof(GajImgsApiConfiguration)))
                           .Configure<GajUsersApiConfiguration>(config.GetSection(nameof(GajUsersApiConfiguration)))
                           .Configure<GajStoreApiConfiguration>(config.GetSection(nameof(GajStoreApiConfiguration)));
        }

        public static IApplicationBuilder ConfigureGlobalExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {
                        await context.Response.WriteAsync(new ErrorDetail()
                        {

                            StatusCode = context.Response.StatusCode,
                            Message = $"Internal Server Error. Trace :: {contextFeature.Error}"

                        }.ToString());
                    }
                });
            });

            return app;
        }

        public static IServiceCollection ConfigureConsul(this IServiceCollection services, IConfiguration configuration)
        {
            var serviceConfig = GetServiceConfig(configuration);

            return services.RegisterConsulService(serviceConfig);
        }

        /// <summary>
        /// read the configuration required for service discovery from environment variables,
        /// that were passed through the docker-compose.override.yml file.
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static ServiceConfig GetServiceConfig(this IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            return new ServiceConfig
            {
                ServiceDiscoveryAddress = configuration.GetValue<Uri>("ServiceConfig:serviceDiscoveryAddress"),
                ServiceAddress = configuration.GetValue<Uri>("ServiceConfig:serviceAddress"),
                ServiceName = configuration.GetValue<string>("ServiceConfig:serviceName"),
                ServiceId = configuration.GetValue<string>("ServiceConfig:serviceId")
            };
        }
        /// <summary>
        ///  register configuration and hosted service with Consul dependencies
        ///  to dependency injection container
        /// </summary>
        /// <param name="services"></param>
        /// <param name="serviceConfig"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterConsulService(this IServiceCollection services, ServiceConfig serviceConfig)
        {
            if (serviceConfig == null)
                throw new ArgumentNullException(nameof(serviceConfig));

            var consulClient = CreateConsulClient(serviceConfig);

            services.AddSingleton(serviceConfig);
            services.AddSingleton<IHostedService, ServiceDiscoveryHostedService>();
            services.AddSingleton<IConsulClient, ConsulClient>(s => consulClient);

            return services;
        }

        private static ConsulClient CreateConsulClient(ServiceConfig serviceConfig)
        {
            return new ConsulClient(config =>
            {
                config.Address = serviceConfig.ServiceDiscoveryAddress;
            });
        }
    }
}
