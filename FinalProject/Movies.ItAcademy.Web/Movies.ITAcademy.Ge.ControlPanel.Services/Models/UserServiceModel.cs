using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Movies.ITAcademy.Ge.ControlPanel.Services.Models
{
    public class UserServiceModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }//tu ar gvchirdeba wavshalot
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<TicketServiceModel> Tickets { get; set; }
    }
}
