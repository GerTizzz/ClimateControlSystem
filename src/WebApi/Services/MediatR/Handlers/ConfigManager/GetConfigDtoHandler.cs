using AutoMapper;
using MediatR;
using Shared.Dtos;
using WebApi.Services.MediatR.Queries.ConfigManager;

namespace WebApi.Services.MediatR.Handlers.ConfigManager
{
    public sealed class GetConfigDtoHandler : IRequestHandler<GetConfigDtoQuery, ConfigsDto>
    {
        private readonly IConfigManager _configManager;
        private readonly IMapper _mapper;

        public GetConfigDtoHandler(IConfigManager configManager, IMapper mapper)
        {
            _configManager = configManager;
            _mapper = mapper;
        }

        public Task<ConfigsDto> Handle(GetConfigDtoQuery request, CancellationToken cancellationToken)
        {
            var config = _configManager.Config;

            var configDto = _mapper.Map<ConfigsDto>(config);

            return Task.FromResult(configDto);
        }
    }
}