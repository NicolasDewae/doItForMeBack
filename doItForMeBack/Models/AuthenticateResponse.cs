﻿using doItForMeBack.Entities;

namespace doItForMeBack.Models
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            FirstName = user.Firstname;
            LastName = user.Lastname;
            Email = user.Email;
            Role = user.Role;
            Token = token;
        }
    }
}
