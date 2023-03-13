namespace Shared.Dtos
{
    public sealed record UserDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
