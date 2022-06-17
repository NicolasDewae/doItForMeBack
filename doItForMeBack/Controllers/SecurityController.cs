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

        /// <summary>
        /// Fonction de connexion
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public IActionResult login(AuthenticateRequest model)
        {
            var response = _securityService.login(model);

            if (response == null)
            {

                return BadRequest(new { message = "Username or password is incorrect" });

            }

            return Ok(response);
        }


    }
}
