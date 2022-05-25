using doItForMeBack.Data;
using doItForMeBack.Models;
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
