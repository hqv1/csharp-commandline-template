using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandLine;
using CommandLineTemplate.Options;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace CommandLineTemplate
{
    internal class Program
    {
        private static IConfigurationRoot _configuration;
        private static IServiceProvider _services;

        private static int Main(string[] args)
        {
            GetConfigurationRoot();
            _services = Ioc.RegisterComponents(_configuration);

            var mediator = _services.GetService<IMediator>();
            try
            {
                //todo: NoOpOptions is used for demostrations. 
                var result = Parser.Default.ParseArguments<
                        NoOpOptions,
                        NoOpOptions2>(args)
                    .WithParsed<IRequest>(opts => Send(mediator, opts).Wait())
                    .WithNotParsed(errs => HandleParseError(errs, args));

                return 0;
            }
            catch (Exception ex)
            {
                var logger = _services.GetService<ILogger>();
                logger.Error(ex, "Fatal exception");
                Console.WriteLine($"Exception. See logs: {ex.Message}");
                return 1;
            }
        }

        private static void GetConfigurationRoot()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        private static int HandleParseError(IEnumerable<Error> errs, string[] args)
        {
            var logger = _services.GetService<ILogger>();
            var exception = new Exception("Unable to parse command");
            exception.Data["args"] = string.Join("; ", args) + " --- ";
            exception.Data["errors"] = string.Join("; ", errs.Select(x => x.Tag));
            logger.Error(exception, "Exiting programming");

            return 1;
        }

        private static async Task Send(IMediator mediator, IRequest opts)
        {
            await mediator.Send(opts);
        }
    }
}
