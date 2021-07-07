using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationServer.Models.Jwt
{
    public class Jwt
    {       
        public string Token { get; set; }
        
        public DateTime Expires { get; set; }

        public Guid RefreshTokenId { get; set; }

        public long ExpiresJsTicks
        {
            get
            {
                var janFirst1970 = new DateTime(1970, 1, 1);
                return (long)((DateTime.Now.ToUniversalTime() - janFirst1970).TotalMilliseconds);
            }
        }
    }
}
