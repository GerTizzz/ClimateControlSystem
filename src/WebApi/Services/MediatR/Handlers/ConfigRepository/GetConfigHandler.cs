using AutoMapper;
using Domain.Entities;
using MediatR;
using WebApi.Infrastructure.Repositories;
using WebApi.Services.MediatR.Queries.ConfigRepository;

namespace WebApi.Services.MediatR.Handlers.ConfigRepository
{
    public sealed class GetConfigHandler : IRequestHandler<GetConfigQuery, Config>
    {
        private readonly IConfigRepository _configRepository;
        private readonly IMapper _mapper;

        public GetConfigHandler(IConfigRepository configRepository, IMapper mapper)
        {
            _configRepository = configRepository;
            _mapper = mapper;
        }

        public async Task<Config> Handle(GetConfigQuery request, CancellationToken cancellationToken)
        {
            var configEntity = await _configRepository.GetConfigAsync();

            var config = _mapper.Map<Config>(configEntity);

            return config;
        }
    }
}