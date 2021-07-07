using NewProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewProject.Authorization.Services
{
    public interface IAccounts
    {
        Task<AccountDto> Get(Guid accountId);
        Task<bool> Delete(Guid accountId);
        Task<AccountDto> GetAccount(string login);
    }
}
