using MovieManagement.Domain.Enums;
using System;
using System.Collections.Generic;

namespace MovieManagement.Domain.POCO
{
    public class Movie
    {
        public int Id { get; set; }
        public string Tittle { get; set; }
        public string? Details { get; set; }
        public int? ReleaseYear { get; set; }
        public string? Genre { get; set; }
        public string? IMDB { get; set; }
        public string? Country { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime
        {
            get
            {
                return StartTime.AddMinutes(DurationInMinutes);
            }
            set { }
        }
        public int DurationInMinutes { get; set; }
        public string Status { get; set; }
        
        public string? URL { get; set; }
        public List<Ticket> Tickets { get; set; }//navigation prop

    }
}
