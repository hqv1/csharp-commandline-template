using CommandLine;
using MediatR;

namespace CommandLineTemplate.Options
{
    //todo: delete once you create more options
    [Verb("no-op", HelpText = "Only for demostration. Delete")]
    public class NoOpOptions : IRequest
    {
    }

    //todo: delete once you create more options
    [Verb("no-op-2", HelpText = "Only for demostration. Delete")]
    public class NoOpOptions2 : IRequest
    {
    }
}
