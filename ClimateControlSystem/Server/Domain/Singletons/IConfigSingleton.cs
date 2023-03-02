using ClimateControlSystem.Server.Resources.Domain;

namespace ClimateControlSystem.Server.Domain.Singletons
{
    public interface IConfigSingleton
    {
        public Config Config { get; }

        void UpdateConfig(Config config);
    }
}
