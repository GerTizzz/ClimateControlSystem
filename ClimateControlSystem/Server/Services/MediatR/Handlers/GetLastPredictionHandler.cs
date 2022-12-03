﻿using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Server.Services.MediatR.Queries;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Handlers
{
    public class GetLastPredictionHandler : IRequestHandler<GetLastPredictionQuery, PredictionResult>
    {
        private readonly IClimateRepository _predictionRepository;

        public GetLastPredictionHandler(IClimateRepository predictionRepository)
        {
            _predictionRepository = predictionRepository;
        }

        public async Task<PredictionResult> Handle(GetLastPredictionQuery request, CancellationToken cancellationToken)
        {
            return await _predictionRepository.GetLastPredictionAsync();
        }
    }
}
