﻿using Domain.Entities;
using Domain.Primitives;

namespace Domain.Repositories;

public interface IForecastsRepository
{
    Task<bool> SaveForecastAsync(Forecast forecast);

    Task<long> GetForecastsCountAsync();

    Task<Label?> TryGetLastPredictionAsync();

    Task<IEnumerable<Forecast>> GetForecastsAsync(IDbRequest requestLimits);
}