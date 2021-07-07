using AuthenticationServer;
using NewProject.Authorization.Services;
using NewProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using AuthenticationServer.Extensions;
using AuthenticationServer.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using NewProject.Authorization.Models;

namespace NewProject.Services
{
    public abstract class AccountsInSQlRepository 
    {
        private const int NUMBER_OF_ROUNDS = 1000;
        private readonly AuthorizationDbContext _db;
        private readonly ILogger<AccountsInSQlRepository> _logger;

        public AccountsInSQlRepository(AuthorizationDbContext db,
            ILogger<AccountsInSQlRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<List<AccountReturnDto>> GetAllListeners()
        {
  
            var accounts = await _db.Accounts.Where(c => c.IsDeleted == false).ToListAsync();

            List<AccountReturnDto> accountsDto = new List<AccountReturnDto>();

            foreach (var account in accounts)
            {
                accountsDto.Add(new AccountReturnDto(account));
            }

            return accountsDto;
        }

        public async Task<AccountReturnDto> GetAccount(Guid id)
        {
            var accounts = await _db.Accounts.ToListAsync();

            var account = accounts.FirstOrDefault(c => c.AccountId == id && c.IsDeleted == false);

            if (account == null) return null;

            return new AccountReturnDto(account);
        }

        public async Task<AccountReturnDto> CreateAccount(AccountCreateDto newAccount) // добавить роль
        {
            var salt = GenerateSalt();
            var enteredPassHash = newAccount.Password.ToPasswordHash(salt);

            LoginModel newLoginModel = new LoginModel()
            {
                Email = newAccount.Email,
                Salt = Convert.ToBase64String(salt),
                PasswordHash = Convert.ToBase64String(enteredPassHash),
            };

            Account account = new Account()
            {
                LoginModel = newLoginModel,
                NickName = newAccount.NickName,
            };

            await _db.Accounts.AddAsync(account);
            await _db.SaveChangesAsync();
            await _db.DisposeAsync();

            return new AccountReturnDto(account);

        }

        //public async Task<Listener> RegisterListenerAccount(AccountCreateDto newAccount)
        //{
        //    Role accountRole = await _db.Roles.FirstOrDefaultAsync(r => r.Name == "listener");
        //    var userAccountDto = await CreateAccount(login, password, accountCreateDto, accountRole);

        //    return userAccountDto;
        //}

        //public async Task<bool> UpdateListenerAccount(Guid id, ListenerCreateDto accountCreateDto)
        //{
        //    var listener = await _db.Listeners.FirstOrDefaultAsync(c => c.UserId == id);

        //    if (listener == null) return false;

        //    listener = accountCreateDto.ToEntity();

        //    _db.Users.Update(listener);
        //    await _db.SaveChangesAsync();
        //    await _db.DisposeAsync();

        //    return true;
        //}


        //public async Task<bool> DeleteListenerAccount(Guid id)
        //{
        //    var listener = await _db.Listeners.FirstOrDefaultAsync(c => c.UserId == id);

        //    if (listener == null) return false;

        //    listener.IsDeleted = true;

        //    await _db.SaveChangesAsync();
        //    await _db.DisposeAsync();

        //    return true;
        //}

        //public async Task<List<Listener>> GetAllDeletedListeners()
        //{
        //    await Task.CompletedTask;
        //    return _db.Listeners.Where(c => c.IsDeleted == true).ToList();
        //}

        //public async Task<bool> RestoreListener(Guid id)
        //{
        //    var listener = await _db.Listeners.FirstOrDefaultAsync(l => l.UserId == id);

        //    if (listener == null) return false;

        //    listener.IsDeleted = false;

        //    await _db.SaveChangesAsync();
        //    await _db.DisposeAsync();

        //    return true;
        //}

        public static byte[] GenerateSalt()
        {
            using var randomNumberGenerator = new RNGCryptoServiceProvider();
            var randomNumber = new byte[16];
            randomNumberGenerator.GetBytes(randomNumber);

            return randomNumber;
        }

    }
}
