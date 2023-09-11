using JuniorWeb.DetaAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace JuniorWeb.Api.CORE
{
    public class JwtManager
    {
        private readonly JuniorWebDbContext _context;
        private readonly JwtSettings _settings;

        public JwtManager(JuniorWebDbContext context, JwtSettings settings)
        {
            _settings = settings;
            _context = context;
        }

        public string MakeToken(string name, string password)
        {
            var user = _context.Users.FirstOrDefault(x => x.Username == name && x.Password == password);

            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            //var valid = BCrypt.Net.BCrypt.Verify(password, user.Password);

            //if (!valid)
            //{
            //    throw new UnauthorizedAccessException();
            //}

            var actor = new JwtUser
            {
                Name = user.Username,
                Email = user.Email,
                Password = user.Password,
            };

            var claims = new List<Claim> // Jti : "",
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iss, _settings.Issuer),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64, _settings.Issuer),
                new Claim("Name", actor.Name.ToString(), ClaimValueTypes.String, _settings.Issuer),
                new Claim("Password", actor.Password.ToString(), ClaimValueTypes.String, _settings.Issuer),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var now = DateTime.UtcNow;
            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: "Any",
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(_settings.Minutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
