using ClimateControl.Server.Resources.Repository.TablesEntities;
using ClimateControl.Shared.Enums;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ClimateControl.Shared.Dtos;
using ClimateControl.Server.Infrastructure.Services;

namespace ClimateControl.Server.Services
{
    public class AuthenticateManager : IAuthenticateManager
    {
        private readonly IUserManager _userManager;
        private readonly IConfiguration _configuration;

        public AuthenticateManager(IUserManager userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<string?> GetTokenForUser(UserDto userToVerify)
        {
            var verifiedUser = await _userManager.GetUserByName(userToVerify.Name);

            if (verifiedUser is null || VerifyPasswordHash(userToVerify.Password, verifiedUser.PasswordHash, verifiedUser.PasswordSalt) is false)
            {
                return null;
            }

            var token = string.Empty;

            if (IsUserVerifyed(userToVerify, verifiedUser))
            {
                token = CreateToken(verifiedUser, _configuration.GetSection("AppSettings:Token").Value);
            }

            return token;
        }

        private static bool VerifyPasswordHash(string password, byte[] passwsordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwsordHash);
        }

        private static bool IsUserVerifyed(UserDto userToVerify, UserEntity verifiedUser)
        {
            return VerifyPasswordHash(userToVerify.Password, verifiedUser.PasswordHash, verifiedUser.PasswordSalt);
        }

        private static string CreateToken(UserEntity user, string securityKey)
        {
            var claim = GetIdentity(user);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var timeNow = DateTime.UtcNow;

            var token = new JwtSecurityToken(
                claims: claim.Claims,
                expires: timeNow.Add(TimeSpan.FromHours(5)),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private static ClaimsIdentity GetIdentity(UserEntity user)
        {
            var role = string.Empty;

            if (user.Role == UserType.Admin)
            {
                role = "Admin";
            }
            else if (user.Role != UserType.Operator)
            {
                role = "Operator";
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
            };

            var claimsIdentity =
                new ClaimsIdentity(
                    claims,
                    "Token",
                    ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            
            return claimsIdentity;
        }
    }
}
