using MediatR;
using Shared.Dtos;

namespace WebApi.Services.MediatR.Queries.UserRepository
{
    public sealed class GetUsersQuery : IRequest<List<UserDto>>
    {

    }
}