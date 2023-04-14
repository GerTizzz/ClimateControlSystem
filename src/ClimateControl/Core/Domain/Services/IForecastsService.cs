using Domain.Entities;

namespace Domain.Services;

public interface IForecastsService
{
    public Task<Label> Predict(Feature feature);
}