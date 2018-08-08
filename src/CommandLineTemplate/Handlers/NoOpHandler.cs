using System.Threading;
using System.Threading.Tasks;
using CommandLineTemplate.Options;
using FluentValidation;
using Hqv.Seedwork.Validations;
using MediatR;
using Microsoft.Extensions.Options;
using Serilog;

namespace CommandLineTemplate.Handlers
{
    public class NoOpHandler : IRequestHandler<NoOpOptions>
    {
        public class Options
        {
            public const string ConfigurationName = "NoOp";

            public void Validate()
            {
                Validator.Validate<Options, OptionValidator>(this);
            }

            private class OptionValidator : AbstractValidator<Options>
            {
                public OptionValidator()
                {
                    //RuleFor(x => x.TemplatePath).Must(File.Exists);
                }
            }
        }

        private readonly ILogger _logger;
        private readonly Options _options;

        public NoOpHandler(IOptions<Options> options, ILogger logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        public Task<Unit> Handle(NoOpOptions request, CancellationToken cancellationToken)
        {
            _logger.Information("Saying hi from NoOpHandler");
            _options.Validate();
            return Task.FromResult(new Unit());
        }
    }
}
