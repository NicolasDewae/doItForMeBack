using doItForMeBack.Data;
using doItForMeBack.Entities;
using doItForMeBack.Models;
using doItForMeBack.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace doItForMeBack.Services.Services
{
    public class MissionService : IMissionService
    {
        private readonly DataContext _db;
        private readonly IUserService _userService;

        public MissionService(DataContext db, IUserService userService)
        {
            _db = db;
            _userService = userService;
        }

        public bool CreateMission(MissionRequest missionRequest, User currentUser)
        {
            // Instanciation de Mission
            Mission mission = new();

            //var nobody = nameof(MissionsStatus.Status.Nobody);

            // Enregistrement des données de missionRequest dans new mission
            
            mission.Claimant = currentUser;
            mission.Status = "Nobody";
            //mission.MissionStatus.Status = MissionsStatus.Status.Nobody;
            mission.Picture = missionRequest.Picture;
            mission.Price = missionRequest.Price;
            mission.Title = missionRequest.Title;
            mission.Description = missionRequest.Description;
            mission.Tag = missionRequest.Tag;
            mission.CreationDate = missionRequest.CreationDate;
            mission.MissionDate = missionRequest.MissionDate;

            // Ajout du nouvel utilisateur en BDD
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
            return _db.Missions
                .Include(m => m.Maker)
                .FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<Mission> GetMissions()
        {
            return _db.Missions
                .Include(m => m.Maker)
                .AsQueryable();
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
