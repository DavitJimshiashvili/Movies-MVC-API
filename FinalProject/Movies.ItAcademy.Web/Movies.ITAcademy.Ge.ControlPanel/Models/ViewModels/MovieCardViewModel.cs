using System;

namespace Movies.ITAcademy.Ge.ControlPanel.Models.ViewModels
{
    public class MovieCardViewModel
    {
        public int Id { get; set; }
        public string Tittle { get; set; }
        public int ReleaseYear { get; set; }
        public string? Genre { get; set; }
        public string? Details { get; set; }
        public string? IMDB { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int DurationInMinutes { get; set; }
        public string? URL { get; set; }
        public string Status { get; set; }
    }
}
