using Application.Services.Strategies;
using Domain.Enumerations;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using MediatR;
using Application.MediatR.UserRepository;
using Microsoft.Extensions.Configuration;

namespace Application.Services.Implementations
{
    public class AuthenticateManager : IAuthenticateManager
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        public AuthenticateManager(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        public async Task<string?> TryGetToken(Guid userId, string password)
        {
            var verifiedUser = await _mediator.Send(new GetUserByIdQuery(userId));

            if (verifiedUser is null || VerifyPasswordHash(password, verifiedUser.PasswordHash, verifiedUser.PasswordSalt) is false)
            {
                return null;
            }

            var token = string.Empty;

            if (VerifyPasswordHash(password, verifiedUser.PasswordHash, verifiedUser.PasswordSalt))
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

        private static string CreateToken(User user, string securityKey)
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

        private static ClaimsIdentity GetIdentity(User user)
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
