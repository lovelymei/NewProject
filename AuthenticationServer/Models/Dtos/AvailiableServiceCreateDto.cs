using NewProject.AuthenticationServer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewProject.AuthenticationServer.Models.Dtos
{
    public class AvailiableServiceCreateDto
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Uri { get; set; }

        public AvailiableService ToEntity()
        {
            var service = new AvailiableService()
            {
                Id = Id,
                Description = Description,
                Uri = Uri
            };

            return service;
        }
    }
}
