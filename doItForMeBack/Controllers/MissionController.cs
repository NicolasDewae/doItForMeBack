using doItForMeBack.Entities;
using doItForMeBack.Services.Interfaces;
using doItForMeBack.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace doItForMeBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionController : ControllerBase
    {
        private readonly IMissionService _missionService;
        private readonly IBanService _banService;

        public MissionController(IMissionService missionService, IBanService banService)
        {
            _missionService = missionService;
            _banService = banService;
        }

        #region get

        /// <summary>
        /// Permet de récupérer les informations concernant les missions de l'utilisateur courant
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin, User")]
        [HttpGet("GetCurrentUserMissions")]
        public IActionResult GetCurrentUserMissions()
        {
            var currentUser = (User)HttpContext.Items["User"];

            if (currentUser == null)
            {
                return BadRequest();
            }
            var currentUserId = currentUser.Id;

            var missions = _missionService.GetMissions().Where(m => m.ClaimantId == currentUserId);

            return Ok(missions);
        }

        /// <summary>
        /// Permet de récupérer toutes les missions et leurs attributs
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin, User")]
        [HttpGet("GetAllMissions")]
        public IQueryable GetAllMissions()
        {
            return _missionService.GetMissions();
        }

        /// <summary>
        /// Permet de récupérer une mission selon l'id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, User")]
        [HttpGet("GetMissionById")]
        public IActionResult GetMissionById(int id)
        {
            if (!_missionService.MissionExists(id))
            {
                return BadRequest(new { message = "La mission n'existe pas" });
            }

            return Ok(_missionService.GetMissionById(id));
        }

        /// <summary>
        /// Permet de récupérer une mission selon l'id
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("GetBanMission")]

        public IQueryable GetBanMission()
        {
            return _missionService.GetBanMissions();
        }
        #endregion

        #region post
        /// <summary>
        /// Permet de créer une mission
        /// </summary>
        /// <param name="mission"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, User")]
        [HttpPost("CreateMission")]
        public IActionResult CreateMission([FromBody] Mission mission)
        {
            if (mission == null)
            {
                return BadRequest(ModelState);
            }
            if (_missionService.MissionExists(mission.Id))
            {
                ModelState.AddModelError("", "Mission already exist");
                return StatusCode(500, ModelState);
            }
            if (!_missionService.CreateMission(mission))
            {
                ModelState.AddModelError("", "Something went wrong while saving mission");
                return StatusCode(500, ModelState);
            }

            return Ok(mission);
        }
        #endregion

        #region update
        /// <summary>
        /// Permet de modifier une mission qui appartient à l'utilisateur courant
        /// </summary>
        /// <param name="mission"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, User")]
        [HttpPut("UpdateCurrentUserMission")]
        public IActionResult UpdateCurrentUserMission(Mission mission)
        {
            var currentUser = (User)HttpContext.Items["User"];
            var missionToUpdate = _missionService.GetMissionById(mission.Id);

            if (currentUser == null || missionToUpdate == null)
            {
                return BadRequest(new { message = "L'utilisateur ou la mission n'existe pas" });
            }
            
            var myMission = _missionService.GetMissions().Where(m => m.ClaimantId == currentUser.Id);

            if(myMission.Any(m => m.ClaimantId != mission.ClaimantId))
            {
                return BadRequest(new { message = "Vous n'êtes pas autorisé à changer cette mission" });
            }

            //Seul les données dans cette liste pourrons être changées
            missionToUpdate.Picture = mission.Picture;
            missionToUpdate.Price = mission.Price;
            missionToUpdate.Title = mission.Title;
            missionToUpdate.Description = mission.Description;
            missionToUpdate.Tag = mission.Tag;
            missionToUpdate.MissionDate = mission.MissionDate;

            _missionService.UpdateMission(missionToUpdate);

            return Ok();
        }

        /// <summary>
        /// Permet de bannir une mission
        /// </summary>
        /// <param name="mission"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("ChangeBanMissionStatus")]
        public IActionResult ChangeBanMissionStatus(Mission mission)
        {

            var currentUser = (User)HttpContext.Items["User"];
            var banToUpdate = _banService.GetBanById(mission.Ban.Id);

            if (mission == null)
            {
                return BadRequest( new { message = "La mission n'existe pas" });
            }
            else if(banToUpdate.Id != mission.Ban.Id)
            {
                return BadRequest(new { message = "Vous ne pouvez pas bannir cet utilisateur" });
            }
            else if (currentUser.Role != "Admin")
            {
                return BadRequest(new { message = "Vous n'avez pas le bon rôle" });
            }

            banToUpdate.BanDate = DateTime.Now;
            banToUpdate.Description = mission.Ban.Description;
            banToUpdate.IsBan = mission.Ban.IsBan;
            banToUpdate.UserBanId = currentUser.Id;

            _banService.UpdateBanMission(banToUpdate);

            return Ok();
        }
        #endregion

        #region delete
        /// <summary>
        /// Pertmet de Supprimer une mission appartenant à l'utilisateur courant
        /// </summary>
        /// <param name="mission"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, User")]
        [HttpDelete("DeleteCurrentUserMission")]
        public IActionResult DeleteCurrentUserMission(int missionId)
        {
            var currentUser = (User)HttpContext.Items["User"];
            var missionToDelete = _missionService.GetMissionById(missionId);

            if (currentUser == null || missionToDelete == null)
            {
                return BadRequest(new { message = "l'utilisateur ou la mission n'existe pas" });
            }

            var myMission = _missionService.GetMissions().Where(m => m.ClaimantId == currentUser.Id);

            if (myMission.Any(m => m.ClaimantId != missionToDelete.ClaimantId))
            {
                return BadRequest(new { message = "Vous n'êtes pas autorisé à changer cette mission" });
            }

            _missionService.DeleteMission(missionToDelete);

            return Ok();
        }

        /// <summary>
        /// Permet de supprimer n'importe quelle mission
        /// </summary>
        /// <param name="mission"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteMission")]
        public IActionResult DeleteMission(int missionId)
        {
            var currentUser = (User)HttpContext.Items["User"];
            var mission = _missionService.GetMissionById(missionId);
            
            if (currentUser == null || mission == null)
            {
                return BadRequest(new { message = "L'utilisateur ou la mission n'existe pas" });
            }
            else if (currentUser.Role != "Admin")
            {
                return BadRequest(new { message = "Vous n'avez pas le bon rôle" });
            }

            _missionService.DeleteMission(mission);

            return Ok();
        }
        #endregion
    }
}
