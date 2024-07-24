using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using PMS_Models;
using PMS_BAL.IService.Login;
using PMS_BAL.Service.Login;
using PMS_BAL.IService.Common;

[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
  //private readonly ITokenService _tokenService;
  private readonly ILoginService _loginService;

    public LoginController(IConfiguration configuration, ILoginService iLoginService)
    {
        _loginService = iLoginService;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(ApplicationUser login)
    {
       var result=await _loginService.Login(login);
        return Ok(result);
    }

}
