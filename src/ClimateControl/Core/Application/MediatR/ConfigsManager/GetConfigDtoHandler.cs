using AutoMapper;
using Domain.Services;
using MediatR;
using Shared.Dtos;

namespace Application.MediatR.ConfigsManager;

public sealed class GetConfigDtoHandler : IRequestHandler<GetConfigDtoQuery, ConfigsDto>
{
    private readonly IConfigsManager _configManager;
    private readonly IMapper _mapper;

    public GetConfigDtoHandler(IConfigsManager configManager, IMapper mapper)
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