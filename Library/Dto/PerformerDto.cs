using NewProject.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace NewProject.Models
{
    [PerformerValidation]
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

        public string NickName { get; set; }
        public DateTime BirthDate { get; set; }
        public virtual List<SongDto> Songs { get; set; }
    }
}
