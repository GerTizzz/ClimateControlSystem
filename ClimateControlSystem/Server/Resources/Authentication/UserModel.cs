using System.ComponentModel.DataAnnotations;

namespace ClimateControlSystem.Server.Resources.Authentication
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Role { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
    }
}
