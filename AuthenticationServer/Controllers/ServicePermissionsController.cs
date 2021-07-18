using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewProject.AuthenticationServer.Models.Dtos;
using NewProject.AuthenticationServer.Repositories;

namespace NewProject.AuthenticationServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicePermissionsController : ControllerBase
    {
        private readonly ILogger<ServicePermissionsController> _log;
        private readonly IServicePermissions _permissionsService;
        private readonly IAccounts _accounts;

        public ServicePermissionsController(ILogger<ServicePermissionsController> log, IServicePermissions permissionsService, IAccounts accounts)
        {
            _log = log;
            _permissionsService = permissionsService;
            _accounts = accounts;
        }

        /// <summary>
        /// Получить все доступные сервисы 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[AuthorizeEnum(Roles.Administrator, Roles.SuperAdministrator)]
        public async Task<ActionResult<List<AvailiableServiceDto>>> GetAll()
        {
            var services = await _permissionsService.GetAvailiableServices();
            return Ok(services);
        }

        /// <summary>
        /// Добавить новый доступный сервис
        /// </summary>
        /// <returns></returns>
        /// <response code="409">Сервис с таким ключем уже есть</response>
        [HttpPost("serviceKey={serviceKey}&description={description}&uri={uri}")]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        //[AuthorizeEnum(Roles.Administrator, Roles.SuperAdministrator)]
        public async Task<ActionResult> Create([FromBody] AvailiableServiceCreateDto serviceCreateDto)
        {
            var isServiceKeyExist = await _permissionsService.CheckExistInAvailiableServices(serviceCreateDto.Id);
            if (isServiceKeyExist) return Conflict();
            _permissionsService.CreateAvailiableService(serviceCreateDto);
            return Ok();
        }

        /// <summary>
        /// Удалить сервис
        /// </summary>
        /// <param name="serviceKey"></param>
        /// <returns></returns>
        /// <response code="409">Сервис с таким ключем отсутствует</response>
        [HttpDelete("{serviceKey}")]
        //[AuthorizeEnum(Roles.Administrator, Roles.SuperAdministrator)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> Delete(string serviceKey)
        {
            var isServiceKeyExist = await _permissionsService.CheckExistInAvailiableServices(serviceKey);
            if (!isServiceKeyExist) return Conflict();
            await _permissionsService.DeleteAvailiableService(serviceKey);
            return Ok();
        }

        /// <summary>
        /// Установить разрешения для доступа к сервисам для аккаунта
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="serviceKey"></param>
        /// <param name="isAllow"></param>
        /// <returns></returns>
        /// <response code="409">Сервис с таким ключем отсутствует</response>
        /// <response code="410">Данные аккаунта отсутствуют</response>
        [HttpPost("accountId={accountId}&serviceKey={serviceKey}&isAllow={isAllow}")]
        //[AuthorizeEnum(Roles.Administrator, Roles.SuperAdministrator)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status410Gone)]
        public async Task<ActionResult> SetAccountServicepermission(Guid accountId, string serviceKey, bool isAllow)
        {
            var isServiceKeyExist = await _permissionsService.CheckExistInAvailiableServices(serviceKey);
            if (!isServiceKeyExist) return Conflict();

            var accountPermissions = await _permissionsService.GetServicePermissions(accountId);
            if (accountPermissions == null) return StatusCode(StatusCodes.Status410Gone);

            if (accountPermissions.Values.ContainsKey(serviceKey))
            {
                accountPermissions.Values[serviceKey] = isAllow;
            }
            else
            {
                accountPermissions.Values.Add(serviceKey, isAllow);
            }

            //accountPermissions.SetByAccountId = HttpContext.User.GetAccountId();
            _permissionsService.SaveServicePermissions(accountPermissions);

            return Ok();
        }

        /// <summary>
        /// Получить разрешения для доступа к сервисам для аккаунта
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        /// <response code="410">Данные аккаунта отсутствуют</response>
        [HttpGet("{accountId}")]
        //[AuthorizeEnum(Roles.Administrator, Roles.SuperAdministrator)]
        [ProducesResponseType(StatusCodes.Status410Gone)]
        public async Task<ActionResult<AccountServicePermissionsDto>> GetPermissions(Guid accountId)
        {
            var permissions = await _permissionsService.GetServicePermissions(accountId);
            if (permissions == null) return StatusCode(StatusCodes.Status410Gone);

            return new AccountServicePermissionsDto(permissions);
        }
    }
}