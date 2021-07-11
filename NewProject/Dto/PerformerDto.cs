﻿using AspNetCoreValidationLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

#nullable disable

namespace NewProject.Models
{
    [DateFormat]
    [OnlyLatin]
    [Length]
    public partial class PerformerDto
    {
        public PerformerDto(Performer performer)
        {
            NickName = performer.NickName;
            BirthDate = performer.BirthDate;
            Songs = performer.Songs
                .Select(c => new SongDto(c))
                .ToList();
        }

        [Required]
        public string NickName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }
        public virtual List<SongDto> Songs { get; set; }
    }
}
