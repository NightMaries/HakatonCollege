using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Hakaton.API.Application.Interfaces; 

namespace Hakaton.API.Application.Services;
public class TokenService: ITokenService
{
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]!));
        }
        public async Task<string> CreateToken(string userLogin)
        {
            var claims =  new List<Claim>{
                new Claim(JwtRegisteredClaimNames.Name, userLogin)};

        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDecriptor = new SecurityTokenDescriptor(){
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDecriptor);
        return  tokenHandler.WriteToken(token);
        }
}