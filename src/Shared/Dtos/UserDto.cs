using Domain.Enumerations;

namespace Shared.Dtos
{
    public sealed record UserDto
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public UserType Role { get; set; } = UserType.Undefined;

        public string Password { get; set; } = string.Empty;
    }
}
