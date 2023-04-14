using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.MediatR.ConfigsRepository;

public sealed class GetConfigHandler : IRequestHandler<GetConfigQuery, Config>
{
    private readonly IConfigsRepository _configRepository;
    private readonly IMapper _mapper;

    public GetConfigHandler(IConfigsRepository configRepository, IMapper mapper)
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