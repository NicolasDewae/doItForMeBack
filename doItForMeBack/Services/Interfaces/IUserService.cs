using doItForMeBack.Entities;

namespace doItForMeBack.Services.Interfaces;

public interface IUserService
{
    bool CreateUser(User user);
    bool UpdatePassword(int userId, string oldPassword, string newPassword);
    bool UpdateUser(User user);
    IQueryable<User> GetUsers();
    User GetUserById(int id);
    bool UserExists(int userId);
    bool Save();
    bool DeleteUser(User user);
}
