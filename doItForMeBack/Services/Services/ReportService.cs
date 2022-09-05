using doItForMeBack.Data;
using doItForMeBack.Entities;
using doItForMeBack.Services.Interfaces;
using Microsoft.AspNetCore.Http;


namespace doItForMeBack.Services.Services
{
    public class ReportService: IReportService
    {
        private readonly DataContext _db;

        public ReportService(DataContext db)
        {
            _db = db;
        }

        public Report GetReportById(int id)
        {
            return _db.Reports.FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<Report> GetAllReports()
        {
            return _db.Reports.AsQueryable();
        }

        public bool CreateReport(Report report)
        {
            report.CreationDate = DateTime.Now;

            _db.Reports.Add(report);
            return Save();
        }

        public bool DeleteReport(Report report)
        {
            _db.Reports.Remove(report);
            return Save();
        }

        public bool ReportExists(int id)
        {
            return _db.Reports.Any(x => x.Id == id);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateReport(Report report)
        {
            _db.Reports.Update(report);
            return Save();
        }
    }
}
