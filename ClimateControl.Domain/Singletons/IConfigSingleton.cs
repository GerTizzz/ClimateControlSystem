using ClimateControl.Domain.Resources;

namespace ClimateControl.Domain.Singletons
{
    public interface IConfigSingleton
    {
        public Config Config { get; }

        void UpdateConfig(Config config);
    }
}
