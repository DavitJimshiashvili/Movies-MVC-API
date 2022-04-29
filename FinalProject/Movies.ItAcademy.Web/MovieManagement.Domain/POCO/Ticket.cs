
using MovieManagement.Domain.Enums.TicketEnums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManagement.Domain.POCO
{
    public class Ticket
    {
        private string _status = TKTStatuses.Booked;
        public string UserId { get; set; }
        public int MovieId { get; set; }
        public User User { get; set; }
        public Movie Movie {get; set; }

        public string Status {
            get
            {
                return _status;
            }

            set 
            { 
                _status = value;
            }
        
        }

    }
}
