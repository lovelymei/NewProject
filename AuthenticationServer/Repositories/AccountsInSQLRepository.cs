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
using AuthenticationServer.Extension;

namespace NewProject.Services
{
    public abstract class AccountsInSQLRepository : IAccounts
    {
        AuthContext _db;
        public AccountsInSQLRepository(AuthContext db)
        {
            _db = db;
        }

        public async Task<AccountDto> GetAccount(string login)
        {
            await Task.CompletedTask;
            ICollection<Account> collection = (ICollection<Account>)_db.GetCollection<Account>();
            var account = collection.FirstOrDefault(c => c.Login == login);

            if (account == null) return null;
            return AccountDto.FromModel(account);
        }

        public async Task<AccountDto> Get(Guid accountId)
        {
            await Task.CompletedTask;
            ICollection<Account> collection = (ICollection<Account>)_db.GetCollection<Account>();
            var account = collection.FirstOrDefault(c => c.AccountId == accountId);
            if (account == null) return null;
            return AccountDto.FromModel(account);
        }

        public async Task<bool> Delete(Guid accountId)
        {
            await Task.CompletedTask;
            ICollection<Account> collection = (ICollection<Account>)_db.GetCollection<Account>();
            var account  = collection.FirstOrDefault(c => c.AccountId == accountId);
            bool isDeleted = true;

            if (account == null) isDeleted = false;
            
            await _db.SaveChangesAsync();
            await _db.DisposeAsync();
            
            return isDeleted;
        }

    }
}
