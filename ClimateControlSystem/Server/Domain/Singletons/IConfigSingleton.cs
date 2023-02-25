using ClimateControlSystem.Server.Resources.Common;

namespace ClimateControlSystem.Server.Domain.Singletons
{
    public interface IConfigSingleton
    {
        public Config Config { get; }

        void UpdateConfig(Config config);
    }
}
