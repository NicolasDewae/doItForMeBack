using doItForMeBack.Data;
using doItForMeBack.Entities;
using doItForMeBack.Helpers;
using doItForMeBack.Models;
using doItForMeBack.Service;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace doItForMeBack.Services
{
    public class UserService: IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly DataContext _db;

        public UserService(DataContext db, IOptions<AppSettings> appSettings)
        {
            _db = db;
            _appSettings = appSettings.Value;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _db.Users.SingleOrDefault(x => x.Email == model.Email && x.Password == model.Password);

            if(user == null)
            {
                return null;
            }

            var token = GenerateJwtToken(user);

            return new AuthenticateResponse(user, token);

        }

        public string GenerateJwtToken(User user)
        {
            // generate token that is valid for 7 days
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

        /// <summary>
        /// Service qui ajoute un utilisateur en base de données
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool CreateUser(User user)
        {
            _db.Users.Add(user);
            return Save();
        }
        /// <summary>
        /// Service qui récupère tous les utilisateurs
        /// </summary>
        /// <returns></returns>
        public IQueryable<User> GetUsers()
        {
            return _db.Users.AsQueryable();
        }

        public User GetUserById(int id)
        {
            return _db.Users.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Service qui vérifie en base de données si l'utilisateur existe
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UserExists(int id)
        {
            return _db.Users.Any(x => x.Id == id);
        }
        /// <summary>
        /// Service qui retourne 
        /// "true" si le changement est effectué en base de données
        /// "false" si le changement n'est pas effectué en base de données
        /// </summary>
        /// <returns>boolean</returns>
        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}
