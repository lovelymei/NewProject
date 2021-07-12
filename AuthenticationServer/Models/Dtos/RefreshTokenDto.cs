using NewProject.AuthenticationServer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationServer.Models.Dtos
{
    public class RefreshTokenDto
    {
        public RefreshTokenDto(RefreshToken refreshToken)
        {
            RefreshTokenId = refreshToken.RefreshTokenId;
            CreateDate = refreshToken.CreateDate;
            ExpiresDate = refreshToken.ExpiresDate;
            AccountId = refreshToken.AccountId;
        }
        public Guid RefreshTokenId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpiresDate { get; set; }
        public Guid AccountId { get; set; }
    }
}
