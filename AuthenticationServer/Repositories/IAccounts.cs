﻿using NewProject.AuthenticationServer.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewProject.AuthenticationServer.Repositories
{
    public interface IAccounts
    {
        Task<AccountDto> Authenticate(string email, string password);
        Task<bool> DeleteAccount(Guid id);
        Task<AccountDto> GetAccount(Guid id);
        Task<IEnumerable<AccountDto>> GetAllAccounts();
        Task<List<AccountDto>> GetAllDeletedAccounts();
        Task<AccountDto> RegisterListenerAccount(AccountCreateDto accountCreateDto);
        Task<AccountDto> RegisterPerformerAccount(AccountCreateDto accountCreateDto);
        Task<bool> RestoreAccount(Guid id);
        Task<bool> UpdateAccount(Guid id, AccountCreateDto accountCreateDto);
    }
}