using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;


namespace CaisseEnregistreuse.Service;

public class JwtService
{
    private readonly IConfiguration _config;

    public JwtService(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateToken(string subject, string email, string role)
    {
        // Il faut commencer par définir les claims, entre guillemets les lignes d'informations de la carte d'identité virtuelle
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, subject),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(ClaimTypes.Role, role)
        };

        // On va rechercher la clé servant à signer le token
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        
        // On va encrypter la clé via un algorithme particulier
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        // On génère le token à partir des informations que l'on vient de créer ainsi que celles présentes dans la configuration de l'application
        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddSeconds(Convert.ToInt32(_config["Jwt:ExpiresInSeconds"])),
            signingCredentials: creds
        );
        
        // On retourne la version String de ce token de sorte à pouvoir l'envoyer facilement au sein d'une requête
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}