using Application.MediatR.ConfigRepository;
using Domain.Singletons;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Singletons
{
    public sealed class ConfigSingleton : IConfigSingleton
    {
        private Config _config;

        private readonly object _lock;

        public Config Config
        {
            get
            {
                lock (_lock)
                {
                    return _config;
                }
            }
        }

        public ConfigSingleton(IServiceScopeFactory scopeFactory)
        {
            _lock = new();

            using (var scope = scopeFactory.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                _config = mediator.Send(new GetConfigQuery()).Result;
            }
        }

        public void UpdateConfig(Config config)
        {
            lock (_lock)
            {
                _config = config;
            }
        }
    }
}
