using MediatR;
using Shared.Dtos;

namespace WebApi.Services.MediatR.Queries.UserRepository
{
    public sealed class GetUserByIdQuery : IRequest<UserDto?>
    {
        public int Id { get; }

        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}