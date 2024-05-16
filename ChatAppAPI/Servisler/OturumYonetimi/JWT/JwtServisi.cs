using ChatAppAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChatAppAPI.Servisler.OturumYonetimi.JWT
{
    public class JwtServisi : IJwtServisi
    {
        private readonly IConfiguration _configuration;

        public JwtServisi(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string JwtTokenOlustur(Kullanici kullanici)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            SymmetricSecurityKey key =
                new(Encoding.UTF8.GetBytes(_configuration["JWT:Key"] ?? string.Empty));


            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, kullanici.Id),
            };
            var tokendesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Audience = _configuration["Jwt:Audience"],
                Issuer = _configuration["Jwt:Issuer"],
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenhandler.CreateToken(tokendesc);
            var finaltoken = tokenhandler.WriteToken(token);
            return finaltoken;
        }
    }
}
