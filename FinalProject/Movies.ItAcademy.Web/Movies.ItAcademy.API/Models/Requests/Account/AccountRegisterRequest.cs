using Movies.ItAcademy.API.Models.DTOs;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Movies.ItAcademy.API.Models.Requests.Account

{

    public class AccountRegisterRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
      
    }
}
