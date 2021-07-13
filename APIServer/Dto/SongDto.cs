using AspNetCoreValidationLibrary;
using NewProject.Models;
using System;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace NewProject.Models
{ 
    [DateFormat]
    [OnlyLatin]
    [Length]
    public partial class SongDto
    {
        public SongDto(Song song)
        {
            Title = song.Title;
            DurationMs = song.DurationMs;
            ProductionDate = song.ProductionDate;
        }
        [Required]
        public string Title { get; set; }
        public long DurationMs { get; set; }
        public DateTime ProductionDate { get; set; }
        public TimeSpan Duration => TimeSpan.FromMilliseconds(DurationMs);
    }
}
