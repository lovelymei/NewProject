using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NewProject.Models
{
    public class AccountDto
    {
        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid AccountId { get; set; }
        /// <summary>
        /// Роль 
        /// </summary>
        public IEnumerable<string> Roles { get; set; }


        public Account ToModel()
        {
            return new Account()
            {
                AccountId = AccountId,
                Login = Login,
                Roles = Roles,
            };
        }

        public static AccountDto FromModel(Account model)
        {
            return new AccountDto()
            {
                AccountId = model.AccountId,
                Login = model.Login,
                Roles = model.Roles,
            };
        }
    }
}
