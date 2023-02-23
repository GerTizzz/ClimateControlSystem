using ClimateControlSystem.Server.Domain.Services;
using ClimateControlSystem.Server.Resources.Repository.TablesEntities;
using ClimateControlSystem.Shared.Common;
using ClimateControlSystem.Shared.Enums;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ClimateControlSystem.Server.Services
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

        public async Task<string?> GetTokenForUser(UserDto request)
        {
            var user = await _userManager.GetUserByName(request.Name);

            if (user is null || VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt) is false)
            {
                return null;
            }

            var token = string.Empty;

            if (await IsUserVerifyed(request, user))
            {
                token = CreateToken(user, _configuration.GetSection("AppSettings:Token").Value);
            }

            return token;
        }

        private static bool VerifyPasswordHash(string password, byte[] passwsordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwsordHash);
        }

        private static Task<bool> IsUserVerifyed(UserDto request, UserEntity user)
        {
            return Task.FromResult(VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt));
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
