using doItForMeBack.Entities;
using doItForMeBack.Models;

namespace doItForMeBack.Services;

public interface ISecurityService
{
    AuthenticateResponse Login(AuthenticateRequest model);
    string GenerateJwtToken(User user);
}

