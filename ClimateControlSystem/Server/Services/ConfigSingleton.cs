using ClimateControlSystem.Server.Domain.Singletons;
using ClimateControlSystem.Server.Resources.Common;

namespace ClimateControlSystem.Server.Services
{
    public sealed class ConfigSingleton : IConfigSingleton
    {
        private Config _config;

        public Config Config => _config;

        public Task UpdateConfig(Config config)
        {
            _config = config;

            return Task.CompletedTask;
        }

        public Task TrySetInitialConfig(Config config)
        {
            if (_config is null)
            {
                _config = config;
            }

            return Task.CompletedTask;
        }
    }
}
