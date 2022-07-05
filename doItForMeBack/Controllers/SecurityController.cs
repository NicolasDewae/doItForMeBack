using doItForMeBack.Entities;
using doItForMeBack.Models;
using doItForMeBack.Service;
using doItForMeBack.Services;
using Microsoft.AspNetCore.Mvc;

namespace doItForMeBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityService _securityService;
        private readonly IUserService _userService;
        public SecurityController(ISecurityService securityService, IUserService userService)
        {
            _securityService = securityService;
            _userService = userService;
        }

        #region post

        /// <summary>
        /// Fonction de connexion
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public IActionResult Login(AuthenticateRequest model)
        {
            var response = _securityService.Login(model);

            if (response == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(response);
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

        #endregion
    }
}
