using Library.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationServer.Models.Dtos
{
    public class AccountCreateDto
    {
        /// <summary>
        /// псевдоним
        /// </summary>
        [Required]
        [Length(MinLen = 1, MaxLen = 100, ErrMes = "must be in range")]
        public string NickName { get; set; }

        /// <summary>
        /// почта
        /// </summary>
        [Required]
        [Length(MinLen = 1, MaxLen = 100, ErrMes = "must be in range")]
        public string Email { get; set; }

        /// <summary>
        /// пароль
        /// </summary>
        [Required]
        [Length(MinLen = 1, MaxLen = 100, ErrMes = "must be in range")]
        public string Password { get; set; }

    }
}
