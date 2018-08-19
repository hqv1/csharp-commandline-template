using System;
using CommandLineTemplate.Handlers;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Exceptions;

namespace CommandLineTemplate
{
    public static class Ioc
    {
        public static IServiceProvider RegisterComponents(IConfiguration configuration)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddOptions();

            //todo: Add options. Delete the following line
            services.Configure<NoOpHandler.Options>(configuration.GetSection(NoOpHandler.Options.ConfigurationName));

            services.AddScoped<ILogger>(provider => new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.WithExceptionDetails()
                .Enrich.FromLogContext()
                .CreateLogger());

            services.AddMediatR();

            //todo: Add 
            //services.AddScoped<IInterface, Implementation>();

            //var connectionString = configuration.GetConnectionString(Some.ConnectionStringName);
            //services.AddSingleton(provider => new ThrawContextFactory(connectionString));

            return services.BuildServiceProvider();
        }
    }
}