using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Account    //наследование 
    {
        public Guid AccountId { get; set; }
        public string NickName { get; set; }
        public bool IsDeleted { get; set; }
        public int RoleId { get; set; }

        public LoginModel loginModel { get; set; }
        public Role Role { get; set; }

        public Account()
        {
            IsDeleted = false;
        }
    }
}
