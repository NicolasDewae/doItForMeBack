using doItForMeBack.Entities;
using doItForMeBack.Models;
using doItForMeBack.Services.Interfaces;
using doItForMeBack.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace doItForMeBack.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IBanService _banService;
        public AdminController(IUserService userService, IBanService banService)
        {
            _userService = userService;
            _banService = banService;
        }

        #region get

        /// <summary>
        /// Permet de récupérer un utilisateur selon l'id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetUserById")]

        public IActionResult GetUserById(int id)
        {
            if (!_userService.UserExists(id))
            {
                return BadRequest(new { message = "L'utilisateur n'existe pas" });
            }

            return Ok(_userService.GetUserById(id));
        }

        /// <summary>
        /// Permet de récupérer tous les utilisateurs et leurs attributs
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllUsers")]
        public IQueryable GetAllUsers()
        {
            return _userService.GetUsers();
        }

        /// <summary>
        /// Permet de récupérer les missions ayant un status ban à true
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetBanUsers")]

        public IQueryable GetBanUsers()
        {
            return _userService.GetBanUsers();
        }

        #endregion

        #region update

        /// <summary>
        /// Modifier les coordonnées de n'importe quel utilisateur.
        /// la modification exclu: le mot de passe
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser(UserRequest user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            _userService.UpdateUser(user);

            return Ok();
        }

        /// <summary>
        /// Permet de bannir une mission
        /// </summary>
        /// <param name="ban"></param>
        /// <returns></returns>
        [HttpPut("ChangeBanUserStatus")]
        public IActionResult ChangeBanUserStatus(BanRequest ban)
        {
            var currentAdmin= (User)HttpContext.Items["User"];
            
            if (ban == null)
            {
                return BadRequest(new { message = "L'utilisateur n'existe pas" });
            }
            if(currentAdmin == null)
            {
                return BadRequest(new { message = "L'utilisateur courant n'existe pas" });
            }

            try
            {
                User user = _userService.GetUserById(ban.WhoIsBannedId);
                Ban userBanned = _banService.GetBanById(user.BanId);
                _userService.BanUser(userBanned, ban, currentAdmin);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return Ok();

        }
        #endregion

        #region delete

        /// <summary>
        /// Permet à l'admin de supprimer n'importe quel compte à condition qu'il n'ait pas le role "Admin"
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser(int userId)
        {
            var user = _userService.GetUserById(userId);

            if (user == null || user.Role == "Admin")
            {
                return BadRequest(new { message = "L'utilisateur n'existe pas ou vous n'avez pas l'autorisation de le supprimer" });
            }



            return Ok(_userService.DeleteUser(user));
        }
        #endregion
    }
}
