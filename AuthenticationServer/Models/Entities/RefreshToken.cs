using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewProject.AuthenticationServer.Models.Entities
{
    public class RefreshToken
    {
        public Guid RefreshTokenId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpiresDate { get; set; }
        public Guid AccountId { get; set; }
        public RefreshToken(Guid accountId, DateTime createDate, int expiresSec)
        {
            AccountId = accountId;
            CreateDate = createDate;
            ExpiresDate = CreateDate.AddSeconds(expiresSec);
        }
    }
}
