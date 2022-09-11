using EVoucherSystem.Models;
using EVoucherSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EVoucherSystem.Controllers.api
{
    [Route("api/token")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly ITokenSevice _tokenService;

        public AccountController(IConfiguration config, ITokenSevice tokenService)
        {
            _configuration = config;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public ActionResult Login(string username, string password)
        {
            if (username != "admin" && password != "admin")
                return Unauthorized("Invalid Credentials");
            else
                return new JsonResult(new { userName = username, token = _tokenService.CreateToken(username) });
        }
    }
}
