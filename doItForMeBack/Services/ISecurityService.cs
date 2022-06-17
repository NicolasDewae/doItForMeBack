using doItForMeBack.Entities;
using doItForMeBack.Models;

namespace doItForMeBack.Services;

public interface ISecurityService
{
    AuthenticateResponse login(AuthenticateRequest model);
}

