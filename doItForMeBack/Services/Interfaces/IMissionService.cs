using doItForMeBack.Entities;
using doItForMeBack.Models;

namespace doItForMeBack.Services.Interfaces
{
    public interface IMissionService
    {
        bool CreateMission(MissionRequest mission, User currentUser);
        bool UpdateMission(Mission mission);
        IQueryable<Mission> GetMissions();
        Mission GetMissionById(int id);
        bool DeleteMission(Mission mission);
        bool MissionExists(int missionId);
        bool Save();
    }
}
