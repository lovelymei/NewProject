using AuthenticationServer.Models;
using AuthenticationServer.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewProject.Services
{
    public interface IAccounts
    {
        Task<AccountReturnDto> CreateAccount(AccountCreateDto accountCreateDto, Role role);
        Task<bool> DeleteAccount(Guid id);
        Task<AccountReturnDto> GetAccount(Guid id);
        Task<List<AccountReturnDto>> GetAllDeletedAccounts();
        Task<List<AccountReturnDto>> GetAllListeners();
        Task<AccountReturnDto> RegisterListenerAccount(AccountCreateDto accountCreateDto);
        Task<AccountReturnDto> RegisterPerformerAccount(AccountCreateDto accountCreateDto);
        Task<bool> RestoreAccount(Guid id);
        Task<bool> UpdateAccount(Guid id, AccountUpdateDto accountUpdateDto);
    }
}