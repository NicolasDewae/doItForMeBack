using doItForMeBack.Entities;
using doItForMeBack.Models;
using doItForMeBack.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace doItForMeBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        #region User
        
        /// <summary>
        /// Permet de récupérer les informations de l'utilisateur existant
        /// </summary>
        /// <returns></returns>
        [HttpGet("currentUser")]
        [Authorize(Roles = "Admin, User")]
        public IActionResult currentUser()
        {
            var currentUser = (User)HttpContext.Items["User"];

            if(currentUser == null)
            {
                return BadRequest();
            }

            return Ok(currentUser);
        }

        /// <summary>
        /// Vérifie si l'ancien mot de passe est correct, si le nouveau mot de passe est saisi et si oui, il appelle le service UpdatePassword
        /// </summary>
        /// <returns></returns>
        [HttpPut("updatePassword")]
        [Authorize(Roles = "Admin, User")]
        public IActionResult UpdatePassword(string oldPassword, string newPassword, string confirmNewPassword)
        {
            var currentUser = (User)HttpContext.Items["User"];

            if(currentUser == null || !BCrypt.Net.BCrypt.Verify(oldPassword, currentUser.Password) || newPassword == null || newPassword != confirmNewPassword)
            {
                return BadRequest();
            }

            _userService.UpdatePassword(currentUser.Id, oldPassword, newPassword);

            return Ok(new { message = "Votre mot de passe a été changé" });
        }

        #endregion

        #region admin

        /// <summary>
        /// Permet de récupérer un utilisateur selon l'id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetUserById")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetUserById(int id)
        {
            if (_userService.GetUserById(id) == null)
            {
                return BadRequest();
            }

            return Ok(_userService.GetUserById(id));
        }

        /// <summary>
        /// Permet de récupérer tous les utilisateurs et leurs attributs
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUsers")]
        [Authorize(Roles = "Admin")]
        public IQueryable GetUsers()
        {
            return _userService.GetUsers();
        }

        #endregion
    }
}
