using doItForMeBack.Data;
using doItForMeBack.Entities;
using doItForMeBack.Models;
using doItForMeBack.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace doItForMeBack.Services.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _db;

        public UserService(DataContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Service qui ajoute un utilisateur en base de données
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool CreateUser(RegistrationRequest userRequest)
        {
            userRequest.Password = BCrypt.Net.BCrypt.HashPassword(userRequest.Password);

            // Instanciation de User
            User user = new();
            user.Ban = new Ban();

            // Instanciation de Ban + set à false
            Ban ban = new Ban();
            ban.IsBan = false;
            
            // Enregistrement des données de userRequest dans new user
            user.Firstname = userRequest.Firstname;
            user.Lastname = userRequest.Lastname;
            user.Email = userRequest.Email;
            user.Password = userRequest.Password;
            user.Role = userRequest.Role;
            user.Adress = userRequest.Adress;
            user.PostCode = userRequest.PostCode;
            user.City = userRequest.City;
            user.State = userRequest.State;
            user.Birthday = userRequest.Birthday;
            user.Ban.IsBan = ban.IsBan;

            // Ajout du nouvel utilisateur en BDD
            _db.Users.Add(user);
            return Save();
        }

        /// <summary>
        /// Encrypt le nouveau mot de passe et l'insert en base de données
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public bool UpdatePassword(int userId, string newPassword)
        {
            var user = GetUserById(userId);

            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);

            _db.Users.Update(user);

            return Save();
        }

        /// <summary>
        /// Modifier un utilisateur
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateUser(UserRequest model)
        {
            var user = this.GetUserById(model.Id);

            //Seul les données dans cette liste pourrons être changées
            user.Firstname = model.Firstname;
            user.Lastname = model.Lastname;
            user.Email = model.Email;
            user.Adress = model.Adress;
            user.PostCode = model.PostCode;
            user.City = model.City;
            user.State = model.State;
            user.Birthday = model.Birthday;

            _db.Users.Update(user);

            return Save();
        }

        /// <summary>
        /// Bannir/Débannir un utilisateur
        /// </summary>
        /// <param name="userBanned"></param>
        /// <param name="ban"></param>
        /// <param name="adminBanner"></param>
        /// <returns></returns>
        public bool BanUser(Ban userBanned, BanRequest ban, User adminBanner)
        {                
            
            userBanned.Banner = adminBanner;
            userBanned.BanDate = DateTime.Now;
            userBanned.Description = ban.Description;
            userBanned.IsBan = ban.IsBan;

            _db.Bans.Update(userBanned);

            return Save();
        }

        /// <summary>
        /// Service qui récupère tous les utilisateurs
        /// </summary>
        /// <returns></returns>
        public IQueryable<User> GetUsers()
        {
            return _db.Users
                .Include(navigationPropertyPath: b => b.Ban)
                .AsQueryable();
        }

        /// <summary>
        /// Récupérer un utilisateur par son Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetUserById(int id)
        {
            return _db.Users
                .Include(navigationPropertyPath: b => b.Ban)
                .FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Récupérer un utilisateur par son email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public User GetUserByEmail(string email)
        {
            return _db.Users
                .Include(navigationPropertyPath: b => b.Ban)
                .FirstOrDefault(x => x.Email == email);
        }

        /// <summary>
        /// Récupérer les utilisateurs ayant le status ban à true
        /// </summary>
        /// <returns></returns>
        public IQueryable<User> GetBanUsers()
        {
            return _db.Users
                .Include(navigationPropertyPath: b => b.Ban)
                .AsQueryable()
                .Where(u => u.Ban.IsBan == true);
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

        public bool DeleteUser(User user)
        {
            _db.Users.Remove(user);
            return Save();
        }
    }
}