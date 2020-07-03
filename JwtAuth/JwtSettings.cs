using System.Text;
using Microsoft.IdentityModel.Tokens;


namespace JwtAuth
{
    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string SecurityKey { get; set; }
        public SymmetricSecurityKey SymmetricSecurityKey => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecurityKey));
        public SigningCredentials SigningCredentials => new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256);
    }
}