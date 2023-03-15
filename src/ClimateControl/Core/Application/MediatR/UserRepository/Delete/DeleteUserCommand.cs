using MediatR;

namespace Application.MediatR.UserRepository.Delete
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
