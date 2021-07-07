using NewProject.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationServer.Extension
{
    public static class AccountExtension
    {
        public static Account FindById(this ICollection<Account> accounts, Guid Id)
        {
            var account = accounts.FirstOrDefault(c => c.AccountId == Id);

            return account;
        }

        public static bool Delete(this ICollection<Account> accounts, Guid Id)
        {
            var account = accounts.FirstOrDefault(c => c.AccountId == Id);

            if (account == null) return false;
            return true;
        }
        
    }

}
