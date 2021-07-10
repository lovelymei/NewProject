using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EntitiesLibrary
{
    public abstract class AccountBase 
    {
        public Guid AccountId { get; set; }
        public bool IsDeleted { get; set; }

        public AccountBase()
        {
            IsDeleted = false;
        }
    }
}
