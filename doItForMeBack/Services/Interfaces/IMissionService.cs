using doItForMeBack.Entities;

namespace doItForMeBack.Services.Interfaces
{
    public interface IMissionService
    {
        bool CreateMission(Mission mission);
        bool UpdateMission(Mission mission);
        IQueryable<Mission> GetMissions();
        Mission GetMissionById(int id);
        IQueryable<Mission> GetBanMissions();
        bool DeleteMission(Mission mission);
        bool MissionExists(int missionId);
        bool Save();
    }
}
