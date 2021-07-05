using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NewProject.Auth;
using NewProject.Authorization.Services;
using NewProject.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace NewProject.Authorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public class TokenController : Controller
    {
        private List<Account> people = new List<Account>
        {
            new Account {Login="admin@gmail.com", Password="12345", Role = "admin" },
            new Account { Login="qwerty@gmail.com", Password="55555", Role = "user" },
            new Account { Login="perf@gmail.com", Password="55555", Role = "performer" }
        };

        private readonly IAccounts _accounts;
        private readonly IToken _tokens;

        public TokenController(IAccounts accounts, IToken tokens)
        {
            _accounts = accounts;
            _tokens = tokens;
        }

        /// <summary>
        /// Создание jwt токена
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost("token")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [AllowAnonymous]
        public IActionResult Token(string username, string password)
        {
            var identity = GetIdentity(username, password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return Json(response);
        }

        /// <summary>
        /// Создание refresh-токена
        /// </summary>
        /// <returns></returns>
        [HttpPost("/resfreshtoken")]
        public IActionResult RefreshToken()
        {
            //TokenDto
            var result = _tokens.GenerateRefreshToken();
            return Json(result);
        }

        /// <summary>
        /// Получить хэш пароля
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost("/hashpassword")]
        public IActionResult HashPassword(string password)
        {
            var result = _tokens.CreateHash(password);
            return Json(result);
        }

        private ClaimsIdentity GetIdentity(string username, string password)
        {
            //находит пользователя 
            Account account = people.FirstOrDefault(x => x.Login == username && x.Password == password);
            //если нашел
            if (account != null)
            {

                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, account.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, account.Role)
                };

                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
    }
}
