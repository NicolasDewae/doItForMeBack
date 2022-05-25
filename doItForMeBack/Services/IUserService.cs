using doItForMeBack.Models;

namespace doItForMeBack.Service;

public interface IUserService
{
    bool CreateUser(User user);
    IQueryable<User> GetUsers();
    bool UserExists(int userId);
    bool Save();
}
