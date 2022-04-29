using System;
using System.ComponentModel.DataAnnotations;

namespace Movies.ITAcademy.Ge.ControlPanel.Models.ViewModels
{
    public class MovieCreateViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Tittle { get; set; }
        public string? Details { get; set; }
        [Required]
        public int ReleaseYear { get; set; }
        public string? Genre { get; set; }

        [Required]
        public string? IMDB { get; set; }
        public string? Country { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        public string? Status { get; set; }

        [Required]
        public int DurationInMinutes { get; set; }
        public string? URL { get; set; }
    }
}
