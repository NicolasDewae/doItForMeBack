using doItForMeBack.Data;
using doItForMeBack.Entities;
using doItForMeBack.Models;
using doItForMeBack.Services.Interfaces;


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
        public bool CreateUser(RegistrationRequest model)
        {
            var user = new User();
            
            user.Firstname = model.Firstname;
            user.Lastname = model.Lastname;
            user.Email = model.Email;
            user.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            user.Role = model.Role;
            user.Adress = model.Adress;
            user.PostCode = model.PostCode;
            user.City = model.City;
            user.State = model.State;
            user.Birthday = model.Birthday;
            user.PhoneNumber = model.PhoneNumber;
            user.Picture = model.Picture;
            user.Ban.IsBan = false;


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
        /// Supprimer un utilisateur
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateUser(UserRequest model)
        {
            var user = this.GetUserByEmail(model.Email);

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
            return _db.Users.AsQueryable();
        }

        /// <summary>
        /// Récupérer un utilisateur par son Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetUserById(int id)
        {
            return _db.Users.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Récupérer un utilisateur par son email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public User GetUserByEmail(string email)
        {
            return _db.Users.FirstOrDefault(x => x.Email == email);
        }

        /// <summary>
        /// Récupérer les utilisateurs ayant le status ban à true
        /// </summary>
        /// <returns></returns>
        public IQueryable<User> GetBanUsers()
        {
            return _db.Users.AsQueryable().Where(u => u.Ban.IsBan == true);
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