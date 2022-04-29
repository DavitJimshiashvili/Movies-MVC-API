using Microsoft.AspNetCore.Identity;
using MovieManagement.Domain.Enums.UserEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MovieManagement.Domain.POCO
{
    public class User:IdentityUser
    {
       // public override string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public override string UserName { get; set; }
        public string Password { get; set; }//tu apishi ar sheitana paroli mashin iqneba defolti
        public List<Ticket> Tickets { get; set; }
    }
}
