using AutoMapper;
using MediatR;
using Shared.Dtos;
using WebApi.Infrastructure.Repositories;
using WebApi.Services.MediatR.Queries.UserRepository;

namespace WebApi.Services.MediatR.Handlers.UserRepository
{
    public sealed class GetUsersHandler : IRequestHandler<GetUsersQuery, List<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUsersHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var usersEntities = await _userRepository.GetUsers();

            var usersDtos = usersEntities.Select(user => _mapper.Map<UserDto>(user)).ToList();

            return usersDtos;
        }
    }
}