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
