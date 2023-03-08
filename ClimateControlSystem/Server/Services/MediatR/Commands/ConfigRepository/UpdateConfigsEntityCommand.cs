using MediatR;

namespace ClimateControl.Server.Services.MediatR.Commands.ConfigRepository;

public sealed class UpdateConfigsEntityCommand : IRequest<bool>
{
    public Config Config { get; }

    public UpdateConfigsEntityCommand(Config config)
    {
        Config = config;
    }
}