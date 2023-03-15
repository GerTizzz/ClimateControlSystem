using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.MediatR.ConfigRepository
{
    public sealed class UpdateConfigHandler : IRequestHandler<UpdateConfigCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IConfigRepository _configRepository;

        public UpdateConfigHandler(IMapper mapper, IConfigRepository configRepository)
        {
            _mapper = mapper;
            _configRepository = configRepository;
        }


        public async Task<bool> Handle(UpdateConfigCommand request, CancellationToken cancellationToken)
        {
            var configEntity = _mapper.Map<Config>(request.Config);

            var result = await _configRepository.UpdateConfigAsync(configEntity);

            return result;
        }
    }
}
