using Domain.Entities;
using MediatR;

namespace WebApi.Services.MediatR.Commands.ConfigRepository
{
    public sealed class UpdateConfigsEntityCommand : IRequest<bool>
    {
        public Config Config { get; }

        public UpdateConfigsEntityCommand(Config config)
        {
            Config = config;
        }
    }
}