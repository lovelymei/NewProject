using AuthenticationServer.Models;
using NewProject.Authorization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NewProject.Models
{
    public class Account
    {
        public Guid AccountId { get; set; }
        public string NickName { get; set; }
        public bool IsDeleted { get; set; }
        public int RoleId { get; set; }


        public LoginModel LoginModel { get; set; }
        public Role Role { get; set; }

        public Account()
        {
            IsDeleted = false;
        }
    }
}
