using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewProject.AuthenticationServer.Models.Dtos;
using NewProject.AuthenticationServer.Repositories;
using System;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteAccount(Guid id)
        {
            var isDeleted = await _accounts.DeleteAccount(id);
            return isDeleted ? Ok() : NotFound();
        }

        /// <summary>
        /// Создать новый аккаунт для слушателя
        /// </summary>
        /// <param name="accountCreateDto"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AccountDto>> RegisterListenerAccount([FromBody] AccountCreateDto listenerCreateDto)
        {
            var createdListener = await _accounts.RegisterListenerAccount(listenerCreateDto);
            return createdListener;
        }

        /// <summary>
        /// Создать новый аккаунт для исполнителя
        /// </summary>
        /// <param name="performerCreateDto"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AccountDto>> RegisterPerformerAccount([FromBody] AccountCreateDto performerCreateDto)
        {
            var createdPerformer = await _accounts.RegisterPerformerAccount(performerCreateDto);
            return createdPerformer;
        }

    }
}