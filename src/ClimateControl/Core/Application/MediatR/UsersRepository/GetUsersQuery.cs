using MediatR;
using Shared.Dtos;

namespace Application.MediatR.UsersRepository;

public sealed class GetUsersQuery : IRequest<List<UserDto>>
{

}