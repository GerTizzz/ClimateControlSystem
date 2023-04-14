using MediatR;

namespace Application.MediatR.ConfigsRepository;

public sealed class UpdateConfigCommand : IRequest<bool>
{
    public Config Config { get; }

    public UpdateConfigCommand(Config config)
    {
        Config = config;
    }
}