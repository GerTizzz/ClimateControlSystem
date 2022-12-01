using ClimateControlSystem.Shared;
using System.ComponentModel.DataAnnotations;

namespace ClimateControlSystem.Server.Resources.RepositoryResources
{
    public sealed class UserRecord
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public UserType Role { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
    }
}
