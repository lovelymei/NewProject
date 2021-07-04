using NewProject.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace NewProject.Models
{
    [UserValidation]
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

        public string Name { get; set; }
        public string Surname { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public virtual List<PerformerDto> Performers { get; set; }
    }
}
