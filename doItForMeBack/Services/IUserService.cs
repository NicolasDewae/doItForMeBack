using doItForMeBack.Entities;
using doItForMeBack.Models;

namespace doItForMeBack.Service;

public interface IUserService
{
    AuthenticateResponse Authenticate(AuthenticateRequest model);
    bool CreateUser(User user);
    IQueryable<User> GetUsers();
    User GetUserById(int id);
    bool UserExists(int userId);
    bool Save();
}
