using doItForMeBack.Data;
using doItForMeBack.Entities;
using doItForMeBack.Services.Interfaces;

namespace doItForMeBack.Services.Services
{
    public class BanService: IBanService
    {
        private readonly DataContext _db;

        public BanService(DataContext db)
        {
            _db = db;
        }
        public Ban GetBanById(int banId)
        {
            return _db.Bans.FirstOrDefault(b => b.Id == banId);
        }

        public bool UpdateBanMission(Ban ban)
        {
            _db.Bans.Update(ban);
            return Save();
        }
        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}
