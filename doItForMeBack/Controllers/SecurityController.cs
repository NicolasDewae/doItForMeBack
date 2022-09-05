using doItForMeBack.Entities;
using doItForMeBack.Models;
using doItForMeBack.Services.Interfaces;
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

            // Vérifier identifiants sont correctes
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
        public IActionResult CreateUser([FromBody] RegistrationRequest user)
        {
            var email = _userService.GetUserByEmail(user.Email);
            if (user == null)
            {
                return BadRequest(ModelState);
            }
            else if (email != null)
            {
                return BadRequest(new { message = "l'adresse email existe déjà" });
            }

            _userService.CreateUser(user);

            return Ok(user);
        }

        #endregion
    }
}
