using doItForMeBack.Entities;

namespace doItForMeBack.Services.Interfaces
{
    public interface IReportService
    {
        IQueryable<Report> GetAllReports();
        Report GetReportById(int id);
        bool CreateReport(Report report);
        bool UpdateReport(Report report);
        bool DeleteReport(Report report);
        bool ReportExists(int reportId);
        bool Save();
    }
}
