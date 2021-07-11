using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewProject.AuthenticationServer.Models.Dtos;
using NewProject.AuthenticationServer.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewProject.AuthenticationServer.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
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
        
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AccountDto>> GetAllAccounts()
        {
            var accounts = await _accounts.GetAllAccounts();
            if (accounts == null) return NotFound();
            return Ok(accounts);
        }

        /// <summary>
        /// Получить все удаленные аккаунты
        /// </summary>
        /// <returns></returns>
        [HttpGet("allDeleted")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<AccountDto>>> GetAllDeletedAccounts()
        {
            var deletedAccounts = await _accounts.GetAllDeletedAccounts();
            List<AccountDto> accountsDto = new List<AccountDto>();

            foreach (var account in deletedAccounts)
            {
                accountsDto.Add(account);
            }

            return Ok(accountsDto);
        }


        /// <summary>
        /// Получить текущий аккаунт
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns></returns>
        [HttpGet("current")]
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
        /// <param name="id">Идентификатор</param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteAccount(Guid id)
        {
            var isDeleted = await _accounts.DeleteAccount(id);
            return isDeleted ? Ok() : NotFound();
        }

        /// <summary>
        /// Создать новый аккаунт для слушателя
        /// </summary>
        /// <param name="accountCreateDto"> Данные слушателя </param>
        /// <returns></returns>
        [HttpPost("listener")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AccountDto>> RegisterListenerAccount([FromBody] AccountCreateDto listenerCreateDto)
        {
            var createdListener = await _accounts.RegisterListenerAccount(listenerCreateDto);
            return createdListener;
        }

        /// <summary>
        /// Создать новый аккаунт для исполнителя
        /// </summary>
        /// <param name="performerCreateDto"> Данные исполнителя </param>
        /// <returns></returns>
        [HttpPost("performer")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AccountDto>> RegisterPerformerAccount([FromBody] AccountCreateDto performerCreateDto)
        {
            var createdPerformer = await _accounts.RegisterPerformerAccount(performerCreateDto);
            return createdPerformer;
        }

        /// <summary>
        /// Обновить аккаунт
        /// </summary>
        /// <param name="id"> Идентификатор</param>
        /// <param name="accounCreateDto"> Данные для обновления </param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> UpdateAccount(Guid id, [FromBody] AccountCreateDto accounCreateDto)
        {
            var isUpdated = await _accounts.UpdateAccount(id, accounCreateDto);

            return isUpdated?Ok() : NotFound();
        }

        /// <summary>
        /// Восстановить аккаунт
        /// </summary>
        /// <param name="id"> Идентификатор </param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> RestoreAccount(Guid id)
        {
            var isRestored = await _accounts.RestoreAccount(id);
            
            return Ok(isRestored);
        }
    }
}