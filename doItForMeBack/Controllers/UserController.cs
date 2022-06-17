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

        /// <summary>
        /// Permet de récupérer tous les utilisateurs et leurs attributs
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IQueryable GetUsers()
        {
            return _userService.GetUsers();
        }

        /// <summary>
        /// Permet de récupérer un utilisateur selon l'id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("id")]
        public IActionResult GetUserById(int id) 
        {
            if(_userService.GetUserById(id) == null)
            {
                return BadRequest();
            }

            return Ok(_userService.GetUserById(id));
        }

        /// <summary>
        /// Permet de récupérer les informations de l'utilisateur existant
        /// </summary>
        /// <returns></returns>
        [HttpGet("currentUser")]
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
        /// Permet de créer un utilisateur, sans restriction de role
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("Registration")]
        public IActionResult CreateUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest(ModelState);
            }
            if (_userService.UserExists(user.Id))
            {
                ModelState.AddModelError("", "User already exist");
                return StatusCode(500, ModelState);
            }
            if (!_userService.CreateUser(user))
            {
                ModelState.AddModelError("", "Something went wrong while saving user");
                return StatusCode(500, ModelState);
            }
            return Ok(user);
        }
    }
}
