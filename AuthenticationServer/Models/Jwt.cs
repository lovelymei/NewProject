using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewProject.Authorization.Models
{
    public class Jwt
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public Guid RefreshTokenId { get; set; }
    }
}
