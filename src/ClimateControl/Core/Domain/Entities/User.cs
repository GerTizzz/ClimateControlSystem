using Domain.Enumerations;
using Domain.Primitives;

namespace Domain.Entities;

public sealed class User : Entity
{
    public string Name { get; set; }
        
    public UserType Role { get; set; }
        
    public byte[] PasswordHash { get; set; }

    public byte[] PasswordSalt { get; set; }

    public User(Guid id, string name, UserType role, byte[] passwordHash, byte[] passwordSalt) : base(id)
    {
        Name = name;
        Role = role;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
    }
}