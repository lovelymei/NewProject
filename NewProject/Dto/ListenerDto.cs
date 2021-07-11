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
    public partial class ListenerDto
    {
        public ListenerDto(Listener user)
        {
            Name = user.Name;
            Surname = user.Surname;
            BirthDate = user.BirthDate;
            Performers = user.Performers
                .Select(c => new PerformerDto(c))
                .ToList();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        public DateTime BirthDate { get; set; }
        public virtual List<PerformerDto> Performers { get; set; }
    }
}
