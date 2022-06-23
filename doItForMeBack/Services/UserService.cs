using doItForMeBack.Data;
using doItForMeBack.Entities;
using doItForMeBack.Service;


namespace doItForMeBack.Services
{
    public class UserService: IUserService
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
        public bool CreateUser(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            _db.Users.Add(user);
            return Save();
        }

        /// <summary>
        /// Encrypt le nouveau mot de passe et l'insert en base de données
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public bool UpdatePassword(int userId, string oldPassword, string newPassword)
        {
            var user = GetUserById(userId);

            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);

            _db.Users.Update(user);

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