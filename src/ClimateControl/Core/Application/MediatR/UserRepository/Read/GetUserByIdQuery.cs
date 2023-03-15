using MediatR;

namespace Application.MediatR.UserRepository.Read
{
    public sealed class GetUserByIdQuery : IRequest<User?>
    {
        public Guid Id { get; }

        public GetUserByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
