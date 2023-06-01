using AutoMapper;
using Domain.Singletons;
using MediatR;

namespace Application.MediatR.ConfigsSingleton;

public sealed class UpdateConfigHandler : IRequestHandler<UpdateConfigCommand, bool>
{
    private readonly IConfigSingleton _config;
    private readonly IMapper _mapper;

    public UpdateConfigHandler(IConfigSingleton config, IMapper mapper)
    {
        _config = config;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateConfigCommand request, CancellationToken cancellationToken)
    {
        var config = _mapper.Map<Config>(request.ConfigsDto);

        var result = await _config.UpdateConfig(config);

        return result;
    }
}