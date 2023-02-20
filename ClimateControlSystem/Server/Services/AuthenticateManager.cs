using ClimateControlSystem.Server.Domain.Services;
using ClimateControlSystem.Server.Resources.Repository.TablesEntities;
using ClimateControlSystem.Shared.Common;
using ClimateControlSystem.Shared.Enums;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace ClimateControlSystem.Server.Services
{
    public class AuthenticateManager : IAuthenticateManager
    {
        private IUserManager _userManager;
        private readonly IConfiguration _configuration;

        public AuthenticateManager(IUserManager userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<string> GetTokenForUser(UserDTO request)
        {
            UserEntity user = await _userManager.GetUserByName(request.Name);

            if (user is null || VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt) is false)
            {
                return null;
            }

            string token = string.Empty;

            if (await IsUserVerifyed(request, user))
            {
                token = CreateToken(user, _configuration.GetSection("AppSettings:Token").Value);
            }

            return token;
        }

        private bool VerifyPasswordHash(string password, byte[] passwsordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwsordHash);
            }
        }

        private async Task<bool> IsUserVerifyed(UserDTO request, UserEntity user)
        {
            if (VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return true;
            }

            return false;
        }

        private string CreateToken(UserEntity user, string securityKey)
        {
            ClaimsIdentity claim = GetIdentity(user);

            if (claim is null)
            {
                return string.Empty;
            }

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(securityKey));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var timeNow = DateTime.UtcNow;

            var token = new JwtSecurityToken(
                claims: claim.Claims,
                expires: timeNow.Add(TimeSpan.FromHours(5)),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private ClaimsIdentity GetIdentity(UserEntity user)
        {
            if (user != null)
            {
                string role = string.Empty;

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

                //List<Claim> claim = new List<Claim>()
                //{
                //    new Claim(ClaimTypes.Name, user.Name),
                //    new Claim(ClaimTypes.Role, role)
                //};

                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }
    }
}
