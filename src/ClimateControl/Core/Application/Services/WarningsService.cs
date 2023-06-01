using Domain.Enumerations;
using Domain.Repositories;
using Domain.Services;
using Domain.Singletons;

namespace Application.Services;

public sealed class WarningsService : IWarningsService
{
    private readonly IWarningsRepository _warningsRepository;
    private readonly IConfigSingleton _config;

    public WarningsService(IWarningsRepository warningsRepository, IConfigSingleton config)
    {
        _warningsRepository = warningsRepository;
        _config = config;
    }

    public async Task<Warning?> GetWarning(PredictedValue? predictedValue)
    {
        if (predictedValue is null)
        {
            return null;
        }
        
        var warningType = CheckPrediction(predictedValue);

        if (warningType == WarningType.Normal)
        {
            return null;
        }

        return await _warningsRepository.GetWarningByType(warningType);
    }

    private WarningType CheckPrediction(PredictedValue predictedValue)
    {
        if (predictedValue.Values[0] > _config.UpperTemperatureLimit)
        {
            return WarningType.CriticalUpper;
        }
        if (predictedValue.Values[0] < _config.LowerTemperatureLimit)
        {
            return WarningType.CriticalLower;
        }

        if (predictedValue.Values[0] > _config.UpLimitOk)
        {
            return WarningType.Upper;
        }
        if (predictedValue.Values[0] < _config.LowLimitOk)
        {
            return WarningType.Lower;
        }

        return WarningType.Normal;
    }
}