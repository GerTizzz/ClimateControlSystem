using MediatR;
using Shared.Dtos;

namespace Application.MediatR.UserRepository
{
    public sealed class GetUsersQuery : IRequest<List<UserDto>>
    {

    }
}
