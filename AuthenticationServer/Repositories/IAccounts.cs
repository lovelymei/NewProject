using NewProject.AuthenticationServer.Models.Dtos;
using NewProject.AuthenticationServer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewProject.AuthenticationServer.Repositories
{
    public interface IAccounts
    {
        Task<Account> Authenticate(string email, string password);
        Task<bool> DeleteAccount(Guid id);
        Task<Account> GetAccount(Guid id);
        Task<IEnumerable<AccountDto>> GetAllAccounts();
        Task<List<AccountDto>> GetAllDeletedAccounts();
        Task<AccountDto> RegisterListenerAccount(AccountCreateDto accountCreateDto);
        Task<AccountDto> RegisterPerformerAccount(AccountCreateDto accountCreateDto);
        Task<bool> RestoreAccount(Guid id);
        Task<bool> UpdateAccount(Guid id, AccountCreateDto accountCreateDto);
    }
}