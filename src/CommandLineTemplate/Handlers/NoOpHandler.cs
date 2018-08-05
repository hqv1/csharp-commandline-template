using System.Threading;
using System.Threading.Tasks;
using CommandLineTemplate.Options;
using MediatR;
using Serilog;

namespace CommandLineTemplate.Handlers
{
    public class NoOpHandler : IRequestHandler<NoOpOptions>
    {
        private readonly ILogger _logger;

        public NoOpHandler(ILogger logger)
        {
            _logger = logger;
        }

        public Task<Unit> Handle(NoOpOptions request, CancellationToken cancellationToken)
        {
            _logger.Information("Saying hi from NoOpHandler");
            return Task.FromResult(new Unit());
        }
    }
}
