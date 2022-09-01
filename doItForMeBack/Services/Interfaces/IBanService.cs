using doItForMeBack.Entities;

namespace doItForMeBack.Services.Interfaces
{
    public interface IBanService
    {
        Ban GetBanById(int banId);
        bool UpdateBanMission(Ban ban);
        bool Save();
    }
}
