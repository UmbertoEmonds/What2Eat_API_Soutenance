using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace What2EatAPI.Utils
{
    public class TokenUtils
    {

        public static string GenerateJWT(IConfiguration _config)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
        public async static Task<Boolean> VerifyJWT(string token, what2eatContext _context, int idUtilisateur)
        {
            var utilisateur = await _context.Utilisateurs.FindAsync(idUtilisateur);

            if (utilisateur != null)
            {
                if (utilisateur.Token != null && token.Equals(utilisateur.Token))
                {
                    return true;
                }
            }
            
            return false;
        }

    }
}