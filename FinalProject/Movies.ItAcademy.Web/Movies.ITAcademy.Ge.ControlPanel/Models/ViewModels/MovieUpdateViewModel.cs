using System;
using System.ComponentModel.DataAnnotations;

namespace Movies.ITAcademy.Ge.ControlPanel.Models.ViewModels
{
    public class MovieUpdateViewModel
    {

        public int Id { get; set; }

        [Required]
        public string Tittle { get; set; }
        public string? Details { get; set; }
        public int? ReleaseYear { get; set; }
        public string? Genre { get; set; }
        public string? IMDB { get; set; }
        public string? Country { get; set; }
        public DateTime StartTime { get; set; }
        public int DurationInMinutes { get; set; }

        public string? URL { get; set; }
    }
}
