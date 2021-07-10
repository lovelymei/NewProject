using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewProject.AuthenticationServer.Models.Dtos;
using NewProject.AuthenticationServer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NewProject.AuthenticationServer.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccounts _accounts;

        public AccountController(IAccounts accounts)
        {
            _accounts = accounts;
        }

        /// <summary>
        /// Получить все аккаунты
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public async Task<ActionResult<AccountDto>> GetAllAccounts()
        {
            var accounts = await _accounts.GetAllAccounts();
            if (accounts == null) return NotFound();
            return Ok(accounts);
        }

        /// <summary>
        /// Получить текущий аккаунт
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("current")]
        public async Task<ActionResult<AccountDto>> GetCurrentAccount(Guid id)
        {
            var account = await _accounts.GetAccount(id);
            if (account == null) return NotFound();
            return account;
        }

        /// <summary>
        /// Удалить аккаунт
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult> DeleteAccount(Guid id)
        {
            var isDeleted = await _accounts.DeleteAccount(id);
            return isDeleted ? Ok() : NotFound();
        }

        /// <summary>
        /// Создать новый аккаунт
        /// </summary>
        /// <param name="accountCreateDto"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<AccountDto>> CreateAccount([FromBody] AccountCreateDto accountCreateDto, Role role //Роли не должно быть она автоматически присваивается)
        {
            var createdAccount = await _accounts.CreateAccount(accountCreateDto, role);  //ОШИБКА нужно вызвать RegisterListenerAccount
            // и потом метод еще один создать но уже с RegisterPerformeAccount

            return Ok(createdAccount);
        }

        
        

    }
}