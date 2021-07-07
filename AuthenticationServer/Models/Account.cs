﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NewProject.Models
{
    public class Account
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
        /// Salt
        /// </summary>
        public string Salt { get; set; }

        /// <summary>
        /// Роль 
        /// </summary>
        public IEnumerable<string> Roles { get; set; }


    }
}
