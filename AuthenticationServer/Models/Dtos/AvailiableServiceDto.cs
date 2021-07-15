using NewProject.AuthenticationServer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewProject.AuthenticationServer.Models.Dtos
{
    public class AvailiableServiceDto
    {
        public AvailiableServiceDto(AvailiableService service)
        {
            Id = service.Id;
            Description = service.Description;
            Uri = service.Uri;
        }
        public string Id { get; set; }
        public string Description { get; set; }
        public string Uri { get; set; }
    }
}
