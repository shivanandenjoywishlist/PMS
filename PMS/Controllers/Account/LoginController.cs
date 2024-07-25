using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using Microsoft.AspNetCore.Authorization;
using PMS_Models;
using PMS_BAL.IService.Login;

using COMMON;

[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class LoginController : Controller
{
    private readonly ILoginService _loginService;

    public LoginController(IConfiguration configuration, ILoginService iLoginService)
    {
        _loginService = iLoginService;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(ApplicationUser login)
    {
        return Json(await _loginService.ExcecuteFunction<Task<JsonModel>>(() => _loginService.Login(login)));
    }
}
