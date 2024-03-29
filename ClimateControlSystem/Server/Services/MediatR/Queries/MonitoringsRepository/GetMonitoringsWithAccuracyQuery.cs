﻿using ClimateControlSystem.Server.Infrastructure;
using ClimateControlSystem.Shared.Responses;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Queries.MonitoringsRepository
{
    public sealed class GetMonitoringsWithAccuracyQuery : IRequest<List<MonitoringWithAccuracyDto>>
    {
        public RequestLimits RequestLimits { get; }

        public GetMonitoringsWithAccuracyQuery(RequestLimits requestLimits)
        {
            RequestLimits = requestLimits;
        }
    }
}
