using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewProject.Authorization.Models.Dtos
{
    public class TokenDto
    {
       // public AccountDto Account { get; set; }
        public string Jwt { get; set; }
        public DateTime Expires { get; set; }
        public Guid RefreshTokenId { get; set; }
    }
}
