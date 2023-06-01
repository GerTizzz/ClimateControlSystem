using Domain.Entities;

namespace Domain.Services;

public interface IWarningsService
{
    Task<Warning?> GetWarning(PredictedValue? predictedValue);
}