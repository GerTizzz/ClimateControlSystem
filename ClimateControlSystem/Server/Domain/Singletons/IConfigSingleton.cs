using ClimateControlSystem.Server.Resources.Common;

namespace ClimateControlSystem.Server.Domain.Singletons
{
    public interface IConfigSingleton
    {
        public Config Config { get; }

        Task UpdateConfig(Config config);

        void TrySetInitialConfig(Config config);
    }
}
