using Dominio.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Servicos.Services.ServiceInterfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Servicos.Services;

public class TokenService : ITokenService
{
    private readonly Options options;
    public TokenService(IOptions<Options> options)
    {
        this.options = options.Value;
    }
    public string GenerateToken(Usuario user)
    {
        var key = Encoding.ASCII.GetBytes(options.Secret);
        var tokenConfig = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
            new Claim("userId", user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role!.Name)
            }),
            Expires = DateTime.UtcNow.AddHours(3),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenConfig);
        var tokenString = tokenHandler.WriteToken(token);

        return tokenString;
    }

    public class Options
    {
        public required string Secret { get; set; }
    }
}
