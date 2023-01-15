﻿using ClimateControlSystem.Shared;
using ClimateControlSystem.Shared.SendToClient;

namespace ClimateControlSystem.Client.Services.ClimateService
{
    public interface IMicroclimateService
    {
        Task<int> GetMicroclimatesRecordsCount(int recordsPerPage);
        Task<IReadOnlyCollection<MonitoringResponse>> GetMonitoringsDataAsync(int start, int count);
        Task<IReadOnlyCollection<MicroclimateResponse>> GetMicroclimatesDataAsync(int start, int count);
        Task<IReadOnlyCollection<TemperatureEventResponse>> GetTemperatureEventDataAsync(int start, int count);
        Task<IReadOnlyCollection<HumidityEventResponse>> GetHumidityEventDataAsync(int start, int count);
    }
}
