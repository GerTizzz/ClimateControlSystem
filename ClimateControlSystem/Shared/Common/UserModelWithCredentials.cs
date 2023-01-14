using ClimateControlSystem.Shared.Enums;

namespace ClimateControlSystem.Shared.Common
{
    public sealed class UserModelWithCredentials
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public UserType Role { get; set; } = UserType.Undefined;

        public string Password { get; set; } = string.Empty;
    }
}
