using AuthenticationServer.Certificates;
using Microsoft.IdentityModel.Tokens;
using NewProject.Authorization.Services;
using NewProject.Models;
using NewProject.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthenticationServer.Services
{
    public class TokenService
    {
        private readonly IAccounts _accounts;
        private readonly SigningAudienceCertificate _signingAudienceCertificate;

        public TokenService(AccountsInSQlRepository accounts)
        {
            _accounts = accounts;
            _signingAudienceCertificate = new SigningAudienceCertificate();
        }

        public async Task<string> GetToken(string userLogin)
        {
            AccountDto accountDto = await _accounts.GetAccount(userLogin);
            SecurityTokenDescriptor tokenDescriptor = GetTokenDescriptor(accountDto);

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            string token = tokenHandler.WriteToken(securityToken);

            return token;
        }

        private SecurityTokenDescriptor GetTokenDescriptor(AccountDto accountDto)
        {
            const int expiringDays = 7;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddDays(expiringDays),
                SigningCredentials = _signingAudienceCertificate.GetAudienceSigningKey()
            };

            return tokenDescriptor;
        }

    }
}
