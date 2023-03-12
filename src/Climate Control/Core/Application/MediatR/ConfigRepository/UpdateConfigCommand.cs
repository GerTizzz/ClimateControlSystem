using MediatR;

namespace Application.MediatR.ConfigRepository
{
    public sealed class UpdateConfigCommand : IRequest<bool>
    {
        public Config Config { get; }

        public UpdateConfigCommand(Config config)
        {
            Config = config;
        }
    }
}
