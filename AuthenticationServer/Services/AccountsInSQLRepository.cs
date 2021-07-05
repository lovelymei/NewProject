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


    }
}
