using doItForMeBack.Entities;
using doItForMeBack.Models;
using doItForMeBack.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

            var missions = _missionService.GetMissions().Where(m => m.Claimant.Id == currentUser.Id);

            var list = missions.Select(m => new Mission()
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                MissionDate = m.MissionDate,
                Picture = m.Picture,
                Price = m.Price,
                Claimant = m.Claimant,
                Status = m.Status,
                Maker = m.Maker,
                Tag = m.Tag
            });

            return Ok(list);
        }

        /// <summary>
        /// Permet de récupérer les missions de l'utilisateur courant qui on un missionStatus à "Pending"
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin, User")]
        [HttpGet("GetCurrentMissionsPending")]
        public IActionResult GetCurrentMissionsPending()
        {
            var currentUser = (User)HttpContext.Items["User"];

            if (currentUser == null)
            {
                return BadRequest();
            }

            var missions = _missionService.GetMissions()
                .Where(m => ( m.Claimant.Id == currentUser.Id) && (m.Status == "Pending"));

            var list = missions.Select(m => new Mission()
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                MissionDate = m.MissionDate,
                Price = m.Price,
                Claimant = m.Claimant,
                Maker = m.Maker,
                Picture = m.Picture,
                Tag = m.Tag
            });

            return Ok(list);
        }

        /// <summary>
        /// Permet de récupérer toutes les missions et leurs attributs
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin, User")]
        [HttpGet("GetAllMissions")]
        public IActionResult GetAllMissions()
        {
            var missions = _missionService.GetMissions();

            var list = missions.Select(m => new Mission()
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                MissionDate = m.MissionDate,
                Price = m.Price,
                Claimant = m.Claimant,
                Picture = m.Picture,
                Tag = m.Tag
            });

            return Ok(list);
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

            var mission = _missionService.GetMissionById(id);

            Mission list = new();

            list.Id = mission.Id;
            list.Title = mission.Title;
            list.Description = mission.Description;
            list.MissionDate = mission.MissionDate;
            list.Price = mission.Price;
            list.Picture = mission.Picture;
            list.Tag = mission.Tag;


            list.Claimant = new User();

            list.Claimant.Id = mission.Claimant.Id;
            list.Claimant.Firstname = mission.Claimant.Firstname;
            list.Claimant.Lastname = mission.Claimant.Lastname;
            list.Claimant.Birthday = mission.Claimant.Birthday;
            list.Claimant.Rate = mission.Claimant.Rate;

            return Ok(list);
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
        public IActionResult CreateMission([FromBody] MissionRequest mission)
        {
            var currentUser = (User)HttpContext.Items["User"];

            if (mission == null)
            {
                return BadRequest(ModelState);
            }

            _missionService.CreateMission(mission, currentUser);

            return Ok(mission);
        }
        #endregion

        #region put
        /// <summary>
        /// Permet de modifier une mission qui appartient à l'utilisateur courant
        /// </summary>
        /// <param name="mission"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, User")]
        [HttpPut("UpdateCurrentUserMission")]
        public IActionResult UpdateCurrentUserMission(MissionRequest mission)
        {
            var currentUser = (User)HttpContext.Items["User"];
            var missionToUpdate = _missionService.GetMissionById(mission.Id);

            if (currentUser == null || missionToUpdate == null)
            {
                return BadRequest(new { message = "L'utilisateur ou la mission n'existe pas" });
            }            
            else if(missionToUpdate.Claimant.Id != currentUser.Id)
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
        /// Demander au "Claimant" pour effectuer la mission
        /// </summary>
        /// <param name="mission"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, User")]
        [HttpPut("AskToDoMission")]
        public IActionResult AskToDoMission(int missionId)
        {
            User currentUser = new();
            currentUser = (User)HttpContext.Items["User"];

            Mission missionToUpdate = new();
            missionToUpdate = _missionService.GetMissionById(missionId);

            if (currentUser == null || missionToUpdate == null)
            {
                return BadRequest(new { message = "L'utilisateur ou la mission n'existe pas" });
            }
            else if (missionToUpdate.Status == "Accepted")
            {
                return BadRequest(new { message = "Un autre DoIteur à déjà été choisi pour effectuer cette mission" });
            }

            missionToUpdate.Status = "Pending";

            // Ajoute le Maker s'il n'est pas déjà dans la liste
            if (!missionToUpdate.Maker.Contains(currentUser)){
                missionToUpdate.Maker.Add(currentUser );
            };

            _missionService.UpdateMission(missionToUpdate);

            return Ok();
        }

        /// <summary>
        /// Accepter ou non un utilisateur à effectuer sa mission
        /// </summary>
        /// <param name="missionId"></param>
        /// <param name="makerId"></param>
        /// <param name="accept"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, User")]
        [HttpPut("AcceptMaker")]
        public IActionResult AcceptMaker(int missionId, int makerId, bool accept)
        {
            // Récupération du currentUser
            User currentUser = new();
            currentUser = (User)HttpContext.Items["User"];

            // Récupération de la mission
            Mission missionToUpdate = new();
            missionToUpdate.Maker = new List<User>();
            missionToUpdate = _missionService.GetMissionById(missionId);

            // On vérifie que tout est ok avant de faire les modifications
            if (currentUser == null)
            {
                return BadRequest(new { message = "L'utilisateur n'existe pas" });
            }
            else if (missionToUpdate == null)
            {
                return BadRequest(new { message = "la mission n'existe pas" });
            }
            else if (missionToUpdate.Status != "Pending")
            {
                return BadRequest(new { message = "La mission n'a pas le bon status " + missionToUpdate.Status });
            }

            // On change missionToUpdate.MissionStatus en fonction du bouléen
            if (accept == true)
            {
                // On récupère les utilisateurs présent dans la liste "Maker"
                var makerList = missionToUpdate.Maker;

                // Suppression de tous les utilisateurs présent dans la liste, sauf le "Maker"
                foreach (User user in makerList.ToList())
                {
                    if (makerId != user.Id)
                    {
                        makerList.Remove(user);
                    }
                }

                missionToUpdate.Status = "Accepted";
                
            }
            else if (accept == false)
            {
                var makerList = missionToUpdate.Maker;

                // Suppression de l'utilisateur
                foreach (User user in makerList.ToList())
                {
                    if (makerId == user.Id)
                    {
                        makerList.Remove(user);
                    }
                }

                // Si la collection est vide, on change le "Status"
                if (makerList.Any() == false)
                {
                    missionToUpdate.Status = "Nobody";
                }
            }

            _missionService.UpdateMission(missionToUpdate);
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

            var myMission = _missionService.GetMissions().Where(m => m.Claimant.Id == currentUser.Id);

            if (myMission.Any(m => m.Claimant.Id != missionToDelete.Claimant.Id))
            {
                return BadRequest(new { message = "Vous n'êtes pas autorisé à supprimer cette mission" });
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

            return Ok(new { message = "La mission a été supprimée" });
        }
        #endregion
    }
}
