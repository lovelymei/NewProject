using AuthenticationServer;
using NewProject.Authorization.Services;
using NewProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace NewProject.Services
{
    public class AccountsInSQLRepository : IAccounts
    {
        AuthContext _db;
        public AccountsInSQLRepository(AuthContext db)
        {
            _db = db;
        }

        public async Task<List<Account>> GetAllAccounts()
        {
            await Task.CompletedTask;
            return _db.Accounts.ToList();
        }

        public async Task<Account> GetAccount(string login)
        {
            await Task.CompletedTask;
            return _db.Accounts.SingleOrDefault(u => u.Login == login);
        }
    }
}
