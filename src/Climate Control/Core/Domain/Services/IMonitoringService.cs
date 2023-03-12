using Domain.Entities;

namespace Domain.Services
{
    public interface IMonitoringService
    {
        public Task<Label> Predict(Feature feature);
    }
}
