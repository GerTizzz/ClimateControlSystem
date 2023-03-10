using AutoMapper;
using MediatR;
using WebApi.Infrastructure.Repositories;
using WebApi.Resources.Repository.TablesEntities;
using WebApi.Services.MediatR.Commands.ConfigRepository;

namespace WebApi.Services.MediatR.Handlers.ConfigRepository
{
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
}