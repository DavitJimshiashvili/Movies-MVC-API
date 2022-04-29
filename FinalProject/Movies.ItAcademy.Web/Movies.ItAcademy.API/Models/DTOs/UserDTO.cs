using System.Collections.Generic;

namespace Movies.ItAcademy.API.Models.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<TicketDTO> Tickets { get; set; }

    }
}
