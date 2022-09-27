using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using NetKubernet.Models;

namespace NetKubernet.Token;

    public class JwtGenerator : IJwtGenerator
    {
    public string BuildToken(User user)
    {
        var claims = new List<Claim> {
            new Claim(JwtRegisteredClaimNames.NameId, user.UserName!),
            new Claim("userId", user.Id),
            new Claim("userEmail", user.Email!)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("My secret word"));
        
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescription = new SecurityTokenDescriptor{
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(30),
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescription);

        return tokenHandler.WriteToken(token);  
    }

    public string CreateToken(User user)
    {
        throw new NotImplementedException();
    }
}
