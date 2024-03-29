﻿using doItForMeBack.Entities;
using doItForMeBack.Models;
using doItForMeBack.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace doItForMeBack.Controllers
{
    [Authorize(Roles = "Admin, User")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        #region get

        /// <summary>
        /// Permet de récupérer les informations de l'utilisateur courant
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCurrentUser")]
        public IActionResult CurrentUser()
        {
            var currentUser = (User)HttpContext.Items["User"];

            if(currentUser == null)
            {
                return BadRequest();
            }

            return Ok(currentUser);
        }

        #endregion

        #region update

        /// <summary>
        /// Modifier les coordonnées de l'utilisateur courant.
        /// la modification exclu: le mot de passe et le role
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("UpdateCurrentUser")]
        public IActionResult UpdateCurrentUser(UserRequest user)
        {
            var currentUser = (User)HttpContext.Items["User"];

            if (currentUser == null)
            {
                return BadRequest( new { message = "une erreur est survenue." });
            }
            else if (currentUser.Id != user.Id)
            {
                return BadRequest(new { message = "Vous n'êtes pas autorisé à effectuer cette modification" });
            }

            _userService.UpdateUser(user);

            return Ok();
        }

        /// <summary>
        /// Vérifie si l'ancien mot de passe est correct, si le nouveau mot de passe est saisi et si oui, il appelle le service UpdatePassword
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("UpdatePassword")]
        public IActionResult UpdatePassword(ChangePasswordRequest model)
        {
            var oldPassword = model.OldPassword;
            var newPassword = model.NewPassword;
            var confirmNewPassword = model.ConfirmNewPassword;

            var currentUser = (User)HttpContext.Items["User"];

            if(currentUser == null)
            {
                return BadRequest(new { message = "une erreur est survenue." });
            }
            else if (newPassword != confirmNewPassword)
            {
                return BadRequest(new { message = "Votre nouveau mot de passe et sa confirmation ne sont pas identiques" });
            }
            else if (oldPassword == null || newPassword == null || confirmNewPassword == null)
            {
                return BadRequest(new { message = "Vous devez remplir tous les champs" });
            }
            else if (!BCrypt.Net.BCrypt.Verify(oldPassword, currentUser.Password))
            {
                return BadRequest(new { message = "Mot de passe incorrect" });
            }

            _userService.UpdatePassword(currentUser.Id, newPassword);

            return Ok(new { message = "Votre mot de passe a été changé" });
        }

        #endregion

        #region delete

        /// <summary>
        /// Supprimer le compte de l'utilisateur courant
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete("DeleteAccount")]
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
    }
}
