using NewProject.Authorization.Models;
using NewProject.Authorization.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationServer.Services
{
    public class AuthenticationService
    {
        //private readonly IAccounts _accounts;
        private readonly TokenService _tokenService;

        public AuthenticationService(TokenService tokenService) //IAccounts accounts, 
        {
            //_accounts = accounts;
            _tokenService = tokenService;
        }

        public async Task<string> Authenticate(LoginModel loginModel)
        {
            //validation for loginModel 
            string securityToken = await _tokenService.GetToken(loginModel.Login);
            return securityToken;
        }
    }
}
