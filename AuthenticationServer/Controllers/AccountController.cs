using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using NewProject.Models; 
using NewProject.Auth;
using NewProject.Services;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using NewProject.Authorization.Models;

namespace TokenApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AccountController : Controller
    {

        //[HttpPost("login")]
        //[AllowAnonymous]
        //public IActionResult Login([FromBody] LoginModel user)
        //{
        //}
    }
}