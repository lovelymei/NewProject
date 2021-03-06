using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NewProject.AuthenticationServer.Certificates;
using NewProject.AuthenticationServer.Extensions;
using NewProject.AuthenticationServer.Models.Dtos;
using NewProject.AuthenticationServer.Models.Entities;
using NewProject.AuthenticationServer.Repositories;
using NewProject.Authorization.Models.Dtos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NewProject.AuthenticationServer.Controllers
{
    [Route("identity/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {

        public Func<DateTime> GetCurentDtFunc = () => DateTime.Now;
        private readonly IConfiguration _config;
        private readonly IAccounts _accounts;
        private readonly IRefreshTokens _refreshTokens;
        private readonly IServicePermissions _permissionsService;

        public AuthorizationController(IAccounts accounts, IConfiguration config, IRefreshTokens refreshTokens,
            IServicePermissions permissionsService)
        {
            _permissionsService = permissionsService;
            _refreshTokens = refreshTokens;
            _accounts = accounts;
            _config = config;
        }

        /// <summary>
        /// Создание JWT
        /// </summary>
        /// <response code="401">Не верные логин/пароль</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        [HttpPost("signin")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [AllowAnonymous]
        public async Task<ActionResult<TokenDto>> CreateToken([FromBody] SignIn signIn)
        {
            var account = await _accounts.Authenticate(signIn.Email, signIn.Password);

            if (account == null) return Unauthorized();

            var expiresSec = int.Parse(_config["Jwt:ExpiresSec"]);

            var refresh = await _refreshTokens.CreateRefreshToken(account, 864000); //TODO В конфиг

            var token = await BuildToken(account, refresh.RefreshTokenId, expiresSec);

            return Ok(token);
        }

        /// <summary>
        /// Обновление JWT
        /// </summary>
        /// <response code="401">Токен просрочен. Вход по логину и паролю (/api/Token/signin)</response>
        /// <response code="403">Аккаунт деактивирован</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        [HttpPost("refreshId={id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [AllowAnonymous]
        public async Task<ActionResult<TokenDto>> RefreshToken(Guid id)
        {
            //var tokenIp = HttpContext.User.GetAccountLastIp();
            //var currentIp = HttpContext.Connection.RemoteIpAddress.ToString();

            //if (tokenIp != HttpContext.Connection.RemoteIpAddress.ToString())
            //{
            //    _log.Warn($"Client ip address does not match the address in the JWT. jwt ip=[{tokenIp}] current=[{currentIp}]");
            //}

            var expiresSec = int.Parse(_config["Jwt:ExpiresSec"]);
            var newRefreshToken = await _refreshTokens.ReCreateRefreshToken(id, 864000); //TODO В конфиг
            if (newRefreshToken == null) return Unauthorized();

            var account = await _accounts.GetAccount(newRefreshToken.AccountId);
            if (account == null) return Forbid();

            var token = await BuildToken(account, newRefreshToken.RefreshTokenId, expiresSec);
            return Ok(token);
        }

        /// <summary>
        /// Получить список всех RefreshToken 
        /// </summary>
        /// <returns></returns>
        /// <response code = "204" > Список RefreshToken пуст</response>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[AuthorizeEnum(Roles.Administrator, Roles.SuperAdministrator)]
        public async Task<ActionResult<RefreshToken[]>> GetAll()
        {
            var tokens = await _refreshTokens.GetAllRefreshTokens();
            if (tokens.Count == 0) return NoContent();
            return Ok(tokens);
        }

        /// <summary>
        /// Получить список всех RefreshToken для аккаунта
        /// </summary>
        /// <returns></returns>
        /// <response code="204">Список RefreshToken пуст</response>
        [HttpGet("accountId={accountId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[AuthorizeEnum(Roles.Administrator, Roles.SuperAdministrator)]
        public async Task<ActionResult<RefreshToken[]>> GetAll(Guid accountId)
        {
            var tokens = await _refreshTokens.GetAllRefreshTokens(accountId);
            if (tokens.Count == 0) return NoContent();
            return Ok(tokens);
        }

        /// <summary>
        /// Удалить RefreshToken 
        /// </summary>
        /// <returns></returns>
        [HttpDelete("tokenId={tokenId}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[AuthorizeEnum(Roles.Administrator, Roles.SuperAdministrator)]
        public async Task<ActionResult<RefreshToken[]>> DeleteToken(Guid tokenId)
        {
            bool isDeleted = await _refreshTokens.DeleteRefreshToken(tokenId);
            return isDeleted ? Ok() : NoContent();
        }

        /// <summary>
        /// Удалить RefreshToken для аккаунта
        /// </summary>
        /// <returns></returns>
        [HttpDelete("accountId={accountId}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[AuthorizeEnum(Roles.Administrator, Roles.SuperAdministrator)]
        public ActionResult<RefreshToken[]> DeleteTokensForAccount(Guid accountId)
        {
            //_refreshTokens.DeleteRefreshTokensForAccount(accountId);
            return Ok();
        }

        private async Task<TokenDto> BuildToken(Account account, Guid refreshId, int expiresSec)
        {
            var expiresDt = GetCurentDtFunc.Invoke().AddSeconds(expiresSec);

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, account.NickName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, account.Role.ToString()),
                new Claim(ClaimTypes.PrimarySid, account.AccountId.ToString()),
            };

           
            //if (account.Role.RoleId < Roles)
            //{
            //    var accountServicePermissions = await _permissionsService.GetServicePermissions(account.AccountId);
            //    if (accountServicePermissions == null)
            //    {
            //        //_log.Error($"No service permissions for account id ({account.Id})");
            //        accountServicePermissions = new AccountServicePermissions();
            //    }
            //    claims.Add(new Claim(ClaimsPrincipalExtention.SERVICE_PERMISSIONS, JsonSerializer.Serialize(accountServicePermissions.Values.ToList())));
            //}

            SigningAudienceCertificate signingAudienceCertificate = new SigningAudienceCertificate(_config);
            var creds = await signingAudienceCertificate.GetAudienceSigningKey();

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: expiresDt,
                signingCredentials: creds);

            return new TokenDto()
            {
                Expires = expiresDt,
                Jwt = new JwtSecurityTokenHandler().WriteToken(token),
                Account = account,
                RefreshTokenId = refreshId
            };
        }
    }
}
