using Domain.Entities;

namespace Domain.Services
{
    public interface IForecastService
    {
        public Task<Label> Predict(Feature feature);
    }
}
