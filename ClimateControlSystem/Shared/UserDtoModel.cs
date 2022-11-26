namespace ClimateControlSystem.Shared
{
    public sealed class UserDtoModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserType Role { get; set; } = UserType.Operator;
    }
}
