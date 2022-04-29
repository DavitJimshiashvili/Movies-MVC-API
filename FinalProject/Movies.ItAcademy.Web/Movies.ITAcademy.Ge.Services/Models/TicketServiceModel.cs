using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.ITAcademy.Ge.Services.Models
{
    public class TicketServiceModel
    {
        public string UserId { get; set; }
        public int MovieId { get; set; }
        public string Status { get; set; }
    }
}
