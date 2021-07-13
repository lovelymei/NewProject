using NewProject.AuthenticationServer.Models.Dtos;
using NewProject.AuthenticationServer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewProject.Authorization.Models.Dtos
{
    public class TokenDto
    {
        public Account Account { get; set; } // Создавать отдельное Dto????? 
        public string Jwt { get; set; }
        public DateTime Expires { get; set; }
        public Guid RefreshTokenId { get; set; }
    }
}
