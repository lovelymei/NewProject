using NewProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationServer.Models.Dtos
{
    public class AccountReturnDto
    {
        public AccountReturnDto(Account account)
        {
            NickName = account.NickName;
            RoleId = account.RoleId;
        }

        public string NickName { get; set; }
        public int RoleId { get; set; }

    }
}
