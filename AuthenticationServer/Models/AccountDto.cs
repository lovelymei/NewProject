using System;
using System.Collections.Generic;
using System.Linq;
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
        public string Role { get; set; }

        public AccountDto(Account account)
        {
            Login = account.Login;
            Password = account.Password;
            Role = account.Role;
        }
    }
}
