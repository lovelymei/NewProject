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
using AuthenticationServer.Models;

namespace NewProject.Services
{
    public abstract class AccountsInSQlRepository : IAccounts
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

        public async Task<AccountReturnDto> CreateAccount(AccountCreateDto accountCreateDto, Role role)
        {
            var salt = GenerateSalt();
            var enteredPassHash = accountCreateDto.Password.ToPasswordHash(salt);

            LoginModel newLoginModel = new LoginModel()
            {
                Email = accountCreateDto.Email,
                Salt = Convert.ToBase64String(salt),
                PasswordHash = Convert.ToBase64String(enteredPassHash),
            };

            Account account = new Account()
            {
                loginModel = newLoginModel,
                NickName = accountCreateDto.NickName,
                Role = role
            };

            await _db.Accounts.AddAsync(account);
            await _db.SaveChangesAsync();
            await _db.DisposeAsync();

            return new AccountReturnDto(account);

        }

        public async Task<AccountReturnDto> RegisterListenerAccount(AccountCreateDto accountCreateDto)
        {
            Role accountRole = await _db.Roles.FirstOrDefaultAsync(r => r.Name == "listener");
            var listenerAccontDto = await CreateAccount(accountCreateDto, accountRole);

            return listenerAccontDto;
        }

        public async Task<AccountReturnDto> RegisterPerformerAccount(AccountCreateDto accountCreateDto)
        {
            Role accountRole = await _db.Roles.FirstOrDefaultAsync(r => r.Name == "performer");
            var performerAccountDto = await CreateAccount(accountCreateDto, accountRole);

            return performerAccountDto;
        }


        public async Task<bool> UpdateAccount(Guid id, AccountUpdateDto accountUpdateDto)
        {
            var account = await _db.Accounts.FirstOrDefaultAsync(c => c.AccountId == id);

            if (account == null) return false;

            var salt = GenerateSalt();
            var enteredPassHash = accountUpdateDto.Password.ToPasswordHash(salt);

            account.NickName = accountUpdateDto.NickName;
            account.LoginModel.Email = accountUpdateDto.Email;
            account.LoginModel.Salt = Convert.ToBase64String(salt);
            account.LoginModel.PasswordHash = Convert.ToBase64String(enteredPassHash);

            _db.Accounts.Update(account);
            await _db.SaveChangesAsync();
            await _db.DisposeAsync();

            return true;
        }


        public async Task<bool> DeleteAccount(Guid id)
        {
            var account = await _db.Accounts.FirstOrDefaultAsync(c => c.AccountId == id);

            if (account == null) return false;

            account.IsDeleted = true;

            await _db.SaveChangesAsync();
            await _db.DisposeAsync();

            return true;
        }

        public async Task<List<AccountReturnDto>> GetAllDeletedAccounts()
        {
            var deletedAccounts = await _db.Accounts.Where(c => c.IsDeleted == true).ToListAsync();

            List<AccountReturnDto> accountsDto = new List<AccountReturnDto>();

            foreach (var account in deletedAccounts)
            {
                accountsDto.Add(new AccountReturnDto(account));
            }

            return accountsDto;
        }

        public async Task<bool> RestoreAccount(Guid id)
        {
            var account = await _db.Accounts.FirstOrDefaultAsync(l => l.AccountId == id);

            if (account == null) return false;

            account.IsDeleted = false;

            await _db.SaveChangesAsync();
            await _db.DisposeAsync();

            return true;
        }

        public static byte[] GenerateSalt()
        {
            using var randomNumberGenerator = new RNGCryptoServiceProvider();
            var randomNumber = new byte[16];
            randomNumberGenerator.GetBytes(randomNumber);

            return randomNumber;
        }

    }
}
