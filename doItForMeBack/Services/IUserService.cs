using doItForMeBack.Entities;
using doItForMeBack.Models;

namespace doItForMeBack.Service;

public interface IUserService
{
    bool CreateUser(User user);
    bool UpdatePassword(int userId, string oldPassword, string newPassword);
    IQueryable<User> GetUsers();
    User GetUserById(int id);
    bool UserExists(int userId);
    bool Save();
}
