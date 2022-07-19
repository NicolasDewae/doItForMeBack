using doItForMeBack.Entities;
using doItForMeBack.Service;
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
        public AdminController(IUserService userService)
        {
            _userService = userService;
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
        [HttpGet("GetUsers")]
        public IQueryable GetUsers()
        {
            return _userService.GetUsers();
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
        public IActionResult UpdateUser(User user)
        {
            var userToUpdate = _userService.GetUserById(user.Id);

            if (user == null || user.Id != userToUpdate.Id)
            {
                return BadRequest();
            }

            //Seul les données dans cette liste pourrons être changées
            userToUpdate.Firstname = user.Firstname;
            userToUpdate.Lastname = user.Lastname;
            userToUpdate.Email = user.Email;
            userToUpdate.Adress = user.Adress;
            userToUpdate.PostCode = user.PostCode;
            userToUpdate.City = user.City;
            userToUpdate.State = user.State;
            userToUpdate.Birthday = user.Birthday;

            _userService.UpdateUser(userToUpdate);

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
