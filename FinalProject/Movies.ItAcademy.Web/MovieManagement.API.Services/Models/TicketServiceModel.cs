using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManagement.Services.Models
{
    public class TicketServiceModel
    {
        public string UserId { get; set; }
        public int MovieId { get; set; }
        public string Status { get; set; }
    }
}
