using ClimateControlSystem.Shared;
using System.ComponentModel.DataAnnotations;

namespace ClimateControlSystem.Server.Resources.Authentication
{
    public class AuthenticatedUserModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public UserType Role { get; set; }
    }
}
