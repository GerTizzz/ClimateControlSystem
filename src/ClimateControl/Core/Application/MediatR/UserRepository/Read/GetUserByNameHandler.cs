using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.MediatR.UserRepository.Read
{
    public sealed class GetUserByNameHandler : IRequestHandler<GetUserByNameQuery, User?>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserByNameHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<User?> Handle(GetUserByNameQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByName(request.UserName);

            return user;
        }
    }
}
