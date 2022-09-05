using doItForMeBack.Entities;
using doItForMeBack.Models;

namespace doItForMeBack.Services.Interfaces;

public interface IUserService
{
    bool CreateUser(RegistrationRequest user);
    bool UpdatePassword(int userId, string newPassword);
    bool UpdateUser(UserRequest user);
    bool BanUser(Ban userBanned, BanRequest ban, User adminBanner);
    IQueryable<User> GetUsers();
    User GetUserById(int id);
    User GetUserByEmail(string email);
    IQueryable<User> GetBanUsers();
    bool UserExists(int userId);
    bool Save();
    bool DeleteUser(User user);

}
