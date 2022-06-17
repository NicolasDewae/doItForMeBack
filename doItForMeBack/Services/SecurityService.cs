using doItForMeBack.Data;
using doItForMeBack.Entities;
using doItForMeBack.Helpers;
using doItForMeBack.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace doItForMeBack.Services
{
    public class SecurityService: ISecurityService
    {
        private readonly AppSettings _appSettings;
        private readonly DataContext _db;

        public SecurityService(DataContext db, IOptions<AppSettings> appSettings)
        {
            _db = db;
            _appSettings = appSettings.Value;

        }

        /// <summary>
        /// Fonction de connection, si l'email et le mot de passe sont correctes, le token est généré
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public AuthenticateResponse login(AuthenticateRequest model)
        {
            var user = _db.Users.SingleOrDefault(x => x.Email == model.Email && x.Password == model.Password);

            if (user == null /*|| !BCrypt.Net.BCrypt.Verify(model.Password, user.Password)*/)
            {
                return null;
            }

            var token = GenerateJwtToken(user);

            return new AuthenticateResponse(user, token);

        }

        /// <summary>
        /// Fonction qui crée le token de connexion
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
