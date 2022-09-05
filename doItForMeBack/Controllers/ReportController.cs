using doItForMeBack.Entities;
using doItForMeBack.Services.Interfaces;
using doItForMeBack.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace doItForMeBack.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IUserService _userService;
        public ReportController(IReportService reportService, IUserService userService)
        {
            _reportService = reportService;
            _userService = userService;
        }


        #region get

        /// <summary>
        /// Permet de récupérer toutes les reports
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllReports")]
        public IQueryable GetAllReports()
        {
            return _reportService.GetAllReports();
        }

        /// <summary>
        /// Permet de récupérer une mission selon l'id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetReportById")]
        public IActionResult GetReportById(int id)
        {
            if (!_reportService.ReportExists(id))
            {
                return BadRequest(new { message = "Le report n'existe pas" });
            }

            return Ok(_reportService.GetReportById(id));
        }

        #endregion

        #region post

        /// <summary>
        /// Permet de créer un report
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, User")]
        [HttpPost("CreateReport")]
        public IActionResult CreateReport([FromBody] Report report)
        {
            if (report == null)
            {
                return BadRequest(ModelState);
            }
            if (_reportService.ReportExists(report.Id))
            {
                ModelState.AddModelError("", "Report already exist");
                return StatusCode(500, ModelState);
            }
            if (!_reportService.CreateReport(report))
            {
                ModelState.AddModelError("", "Something went wrong while saving mission");
                return StatusCode(500, ModelState);
            }

            return Ok(report);
        }

        #endregion

        #region update

        /// <summary>
        /// Modifier de modifier un repoty.
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        [HttpPut("UpdateReport")]
        public IActionResult UpdateReport(Report report)
        {
            var reportToUpdate = _reportService.GetReportById(report.Id);

            if (report == null || report.Id != reportToUpdate.Id)
            {
                return BadRequest();
            }

            //Seul les données dans cette liste pourrons être changés
            reportToUpdate.Description = report.Description;
            reportToUpdate.Picture = report.Picture;
            reportToUpdate.Subject = report.Subject;
            reportToUpdate.LastUpdate = DateTime.Now;

            _reportService.UpdateReport(reportToUpdate);

            return Ok();
        }

        #endregion

        #region delete

        /// <summary>
        /// Permet de supprimer n'importe quelle report
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        [HttpDelete("DeleteReport")]
        public IActionResult DeleteReport(int reportId)
        {
            var currentUser = (User)HttpContext.Items["User"];
            var report = _reportService.GetReportById(reportId);

            if (currentUser == null)
            {
                return BadRequest(new { message = "L'utilisateur ou la mission n'existe pas" });
            }
            else if (currentUser.Role != "Admin")
            {
                return BadRequest(new { message = "Vous n'avez pas le bon rôle" });
            }
            else if (report == null)
            {
                return BadRequest(new { message = "Le report n'existe pas" });
            }

            _reportService.DeleteReport(report);

            return Ok();
        }

        #endregion
    }
}
