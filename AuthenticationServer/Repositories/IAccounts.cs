using NewProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewProject.Authorization.Services
{
    public interface IAccounts
    {
        Task<List<Account>> GetAllAccounts();
        Task<Account> GetAccount(string login);
    }
}
