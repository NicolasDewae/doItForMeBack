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

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if(response == null)
            {
                
                return BadRequest(new { message = "Username or password is incorrect" });

            }

            return Ok(response);
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
        /// Permet de créer un utilisateur
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("id")]
        public User GetUserById(int id) 
        {
            return _userService.GetUserById(id);
        }

        [HttpPost]
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
