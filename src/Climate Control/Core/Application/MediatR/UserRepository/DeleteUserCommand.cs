using MediatR;

namespace Application.MediatR.UserRepository
{
    public sealed class DeleteUserCommand : IRequest<bool>
    {
        public Guid Id { get; }

        public DeleteUserCommand(Guid id)
        {
            Id = id;
        }
    }
}
