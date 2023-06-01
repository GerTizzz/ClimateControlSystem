using Domain.Entities;

namespace Domain.Services;

public interface IForecastsService
{
    public Task<PredictedValue> Predict(Feature feature);
}