using AuthenticationServer.Models.Dtos;
using NewProject.AuthenticationServer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewProject.AuthenticationServer.Repositories
{
    public interface IRefreshTokens
    {
        Func<DateTime> GetDtFunc { get; set; }

        Task<RefreshToken> CreateRefreshToken(Account account, int expiresSec);
        Task<bool> DeleteRefreshToken(Guid id);
        Task DeleteRefreshTokensForAccount(Guid accountId);
        Task<List<RefreshTokenDto>> GetAllRefreshTokens();
        Task<List<RefreshTokenDto>> GetAllRefreshTokens(Guid accountId);
        Task<RefreshToken> ReCreateRefreshToken(Guid previousRefreshId, int expiresSec);
    }
}