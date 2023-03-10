using Domain.Entities;

namespace Domain.Singletons
{
    public interface IConfigSingleton
    {
        public Config Config { get; }

        void UpdateConfig(Config config);
    }
}
