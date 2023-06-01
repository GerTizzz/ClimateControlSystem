using Domain.Entities;

namespace Domain.Services;

public interface IForecastsService
{
    public Task<Forecast?> Predict(Feature feature);
}