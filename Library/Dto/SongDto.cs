using NewProject.Validation;
using System;
using System.Collections.Generic;

#nullable disable

namespace NewProject.Models
{
    [SongValidation]
    public partial class SongDto
    {
        public SongDto(Song song)
        {
            Title = song.Title;
            DurationMs = song.DurationMs;
            ProductionDate = song.ProductionDate;
        }
        public string Title { get; set; }
        public long DurationMs { get; set; }
        public DateTime ProductionDate { get; set; }
        public TimeSpan Duration => TimeSpan.FromMilliseconds(DurationMs);
    }
}
