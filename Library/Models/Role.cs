using NewProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
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
