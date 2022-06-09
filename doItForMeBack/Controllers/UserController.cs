using doItForMeBack.Models;
using doItForMeBack.Service;
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
