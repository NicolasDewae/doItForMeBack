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
        /// Permet de récupérer les informations de l'utilisateur courant
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCurrentUser")]
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
        /// Modifier les coordonnées de l'utilisateur courant.
        /// la modification exclu: le mot de passe et le role
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("UpdateCurrentUser")]
        [Authorize(Roles = "Admin, User")]
        public IActionResult UpdateCurrentUser(User user)
        {
            var currentUser = (User)HttpContext.Items["User"];

            if (currentUser == null || currentUser.Id != user.Id)
            {
                return BadRequest();
            }

            //Seul les données dans cette liste pourrons être changées
            currentUser.Firstname = user.Firstname;
            currentUser.Lastname = user.Lastname;
            currentUser.Email = user.Email;
            currentUser.Adress = user.Adress;
            currentUser.PostCode = user.PostCode;
            currentUser.City = user.City;
            currentUser.State = user.State;
            currentUser.Birthday = user.Birthday;

            _userService.UpdateUser(currentUser);

            return Ok();
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

        [HttpDelete("deleteCurrentUser")]
        [Authorize(Roles = "Admin, User")]
        public IActionResult DeleteCurrentUser(int userId)
        {
            var currentUser = (User)HttpContext.Items["User"];

            if (currentUser == null || currentUser.Id != userId)
            {
                return BadRequest();
            }

            _userService.DeleteUser(currentUser);

            return Ok();
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
        [Authorize(Roles = "Admin")]
        public IQueryable GetUsers()
        {
            return _userService.GetUsers();
        }

        /// <summary>
        /// Modifier les coordonnées de , n'importe quel utilisateur.
        /// la modification exclu: le mot de passe
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("UpdateUser")]
        [Authorize(Roles = "Admin")]
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
            userToUpdate.Role = user.Role;
            userToUpdate.Adress = user.Adress;
            userToUpdate.PostCode = user.PostCode;
            userToUpdate.City = user.City;
            userToUpdate.State = user.State;
            userToUpdate.Birthday = user.Birthday;

            _userService.UpdateUser(userToUpdate);

            return Ok();
        }

        [HttpDelete("deleteUser")]
        [Authorize(Roles = "Admin")]
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
