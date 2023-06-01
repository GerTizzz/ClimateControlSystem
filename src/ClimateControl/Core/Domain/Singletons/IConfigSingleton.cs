using Domain.Entities;

namespace Domain.Singletons;

public interface IConfigSingleton
{
    public float UpperTemperatureLimit { get; }
    public float LowerTemperatureLimit { get; }
    
    public float UpLimitOk { get; }
    
    public float LowLimitOk { get; }
    
    public Config Config { get; }
    
    Task<bool> UpdateConfig(Config config);
}