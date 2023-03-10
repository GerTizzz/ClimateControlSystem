using Domain.Entities;
using MediatR;
using WebApi.Services.MediatR.Queries.ConfigRepository;

namespace WebApi.Services
{
    public sealed class ConfigSingleton : IConfigSingleton
    {
        private Config _config;

        private readonly object _lock;
        private readonly IServiceScopeFactory _scopeFactory;

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
            _scopeFactory = scopeFactory;

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
