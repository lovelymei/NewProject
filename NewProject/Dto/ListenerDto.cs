using AspNetCoreValidationLibrary;
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
    public class ListenerDto
    {
        public ListenerDto(Listener user)
        {
            BirthDate = user.BirthDate;
        }

        public DateTime BirthDate { get; set; }
        public virtual List<PerformerDto> Performers { get; set; }
    }
}
