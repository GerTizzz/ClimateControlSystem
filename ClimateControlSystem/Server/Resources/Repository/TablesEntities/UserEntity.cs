﻿using ClimateControlSystem.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace ClimateControlSystem.Server.Resources.Repository.TablesEntities
{
    public sealed class UserEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public UserType Role { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
    }
}
