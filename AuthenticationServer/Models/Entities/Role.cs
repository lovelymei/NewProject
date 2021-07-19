using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewProject.AuthenticationServer.Models.Entities
{
    public class Role
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public IEnumerable<Account> Accounts { get; set; }

        public Role()
        {
            Accounts = new List<Account>();
        }

    }
}
