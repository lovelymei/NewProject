using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewProject.Authorization.Services;
using NewProject.Models;
using System.Linq;
using System.Security.Claims;

namespace TokenApp.Controllers
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

    }
}