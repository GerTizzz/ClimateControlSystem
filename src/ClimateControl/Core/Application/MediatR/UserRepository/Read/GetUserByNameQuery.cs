using MediatR;

namespace Application.MediatR.UserRepository.Read
{
    public sealed class GetUserByNameQuery : IRequest<User?>
    {
        public string UserName { get; }

        public GetUserByNameQuery(string userName)
        {
            UserName = userName;
        }
    }
}
