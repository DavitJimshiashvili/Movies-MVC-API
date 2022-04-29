using System;

namespace Movies.ITAcademy.Ge.Models.ViewModels
{
    public class MovieDetailsViewModel
    {
        public int Id { get; set; }
        public string Tittle { get; set; }
        public int ReleaseYear { get; set; }
        public string Genre { get; set; }
        public string IMDB { get; set; }
        public string Country { get; set; }
        public string Details { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int DurationInMinutes { get; set; }
       // public string Status { get; set; }
       //aq unda chavamatot dajavshna gauqmebis gilaki
        public string URL { get; set; }
    }
}
