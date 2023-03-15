using MediatR;
using Shared.Dtos;

namespace Application.MediatR.UserRepository.Read
{
    public sealed class GetUsersQuery : IRequest<List<UserDto>>
    {

    }
}
