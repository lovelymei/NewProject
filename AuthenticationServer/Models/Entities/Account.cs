using EntitiesLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NewProject.AuthenticationServer.Models.Entities
{
    public class Account : AccountBase 
    {
        public string NickName { get; set; }
        public int RoleId { get; set; }

        public Login Login { get; set; }
        public Role Role { get; set; }

        public Account() : base() { }
        
    }
}
