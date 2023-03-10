using MediatR;

namespace WebApi.Services.MediatR.Commands.UserRepository
{
    public sealed class DeleteUserCommand : IRequest<bool>
    {
        public int Id { get; }

        public DeleteUserCommand(int id)
        {
            Id = id;
        }
    }
}