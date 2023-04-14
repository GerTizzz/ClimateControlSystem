﻿using AutoMapper;
using Domain.Services;
using MediatR;

namespace Application.MediatR.ConfigsManager;

public sealed class UpdateConfigHandler : IRequestHandler<UpdateConfigCommand, bool>
{
    private readonly IConfigsManager _configManager;
    private readonly IMapper _mapper;

    public UpdateConfigHandler(IConfigsManager configManager, IMapper mapper)
    {
        _configManager = configManager;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateConfigCommand request, CancellationToken cancellationToken)
    {
        var config = _mapper.Map<Config>(request.ConfigsDto);

        var result = await _configManager.UpdateConfig(config);

        return result;
    }
}