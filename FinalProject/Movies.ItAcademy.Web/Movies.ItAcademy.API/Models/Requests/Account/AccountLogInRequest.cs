using Movies.ItAcademy.API.Models.DTOs;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Movies.ItAcademy.API.Models.Requests.Account
{
    public class AccountLogInRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

        //aq sheidzleba chavamatot filmebi
    }
}
