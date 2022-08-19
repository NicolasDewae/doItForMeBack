using doItForMeBack.Data;
using doItForMeBack.Entities;
using doItForMeBack.Services.Interfaces;

namespace doItForMeBack.Services.Services
{
    public class MissionService : IMissionService
    {
        private readonly DataContext _db;

        public MissionService(DataContext db)
        {
            _db = db;
        }

        public bool CreateMission(Mission mission)
        {
            _db.Missions.Add(mission);
            return Save();
        }

        public bool DeleteMission(Mission mission)
        {
            _db.Missions.Remove(mission);
            return Save();
        }

        public Mission GetMissionById(int id)
        {
            return _db.Missions.FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<Mission> GetMissions()
        {
            return _db.Missions.AsQueryable();
        }

        public IQueryable<Mission> GetBanMissions()
        {
            return _db.Missions.AsQueryable().Where(m => m.Ban.IsBan == true);
        }

        public bool MissionExists(int id)
        {
            return _db.Missions.Any(x => x.Id == id);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateMission(Mission mission)
        {
            _db.Missions.Update(mission);
            return Save();
        }
    }
}
