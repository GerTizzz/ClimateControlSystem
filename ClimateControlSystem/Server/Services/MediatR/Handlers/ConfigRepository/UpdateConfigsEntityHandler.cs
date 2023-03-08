using AutoMapper;
using ClimateControl.Server.Infrastructure.Repositories;
using ClimateControl.Server.Resources.Repository.TablesEntities;
using ClimateControl.Server.Services.MediatR.Commands.ConfigRepository;
using MediatR;

namespace ClimateControl.Server.Services.MediatR.Handlers.ConfigRepository;

public sealed class UpdateConfigsEntityHandler : IRequestHandler<UpdateConfigsEntityCommand, bool>
{
    private readonly IMapper _mapper;
    private readonly IConfigRepository _configRepository;

    public UpdateConfigsEntityHandler(IMapper mapper, IConfigRepository configRepository)
    {
        _mapper = mapper;
        _configRepository = configRepository;
    }


    public async Task<bool> Handle(UpdateConfigsEntityCommand request, CancellationToken cancellationToken)
    {
        var configEntity = _mapper.Map<ConfigsEntity>(request.Config);

        var result = await _configRepository.UpdateConfigAsync(configEntity);

        return result;
    }
}