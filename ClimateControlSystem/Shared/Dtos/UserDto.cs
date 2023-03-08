using ClimateControl.Shared.Enums;

namespace ClimateControl.Shared.Dtos
{
    public sealed record UserDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public UserType Role { get; set; } = UserType.Undefined;

        public string Password { get; set; } = string.Empty;
    }
}
