using AutoMapper;
using Domain.Singletons;
using MediatR;
using Shared.Dtos;

namespace Application.MediatR.ConfigsSingleton;

public sealed class GetConfigDtoHandler : IRequestHandler<GetConfigDtoQuery, ConfigsDto>
{
    private readonly IConfigSingleton _config;
    private readonly IMapper _mapper;

    public GetConfigDtoHandler(IConfigSingleton config, IMapper mapper)
    {
        _config = config;
        _mapper = mapper;
    }

    public Task<ConfigsDto> Handle(GetConfigDtoQuery request, CancellationToken cancellationToken)
    {
        var config = _config.Config;

        var configDto = _mapper.Map<ConfigsDto>(config);

        return Task.FromResult(configDto);
    }
}