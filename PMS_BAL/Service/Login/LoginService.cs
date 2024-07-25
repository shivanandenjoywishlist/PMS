using PMS_BAL.IService.Login;
using PMS_DAL.IRepositories.Login;
using PMS_Models; // Ensure ApplicationUser is correctly imported
using COMMON; // Ensure ResponseModel is correctly imported
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using COMMON.Enums;
using PMS_BAL.IService.Common;
using PMS_BAL.Service.Common;
using PMS_Entity;

namespace PMS_BAL.Service.Login
{
    public class LoginService : BaseService, ILoginService // Assuming ApplicationUser is the correct type
    {
        private readonly ILoginRepositories _LoginRepositories;
        private readonly IConfiguration _Configuration;
        private readonly ITokenService _TokenService;


        public LoginService(ILoginRepositories loginRepositories, ITokenService tokenService, IConfiguration Configuration)
        {
            _LoginRepositories = loginRepositories;
            _TokenService = tokenService;
            _Configuration = Configuration;
        }
       
        public async Task<JsonModel> Login(ApplicationUser applicationUser)
        {
            JsonModel result = new JsonModel()
            {
                Data = false,
                Message = "server Error",
                StatusCode = 404
            };
            var userdata = await _LoginRepositories.LoginUser(applicationUser);
            applicationUser.Role= userdata.userRole.RoleNameName;
            if (result != null)
            {
               string token= await _TokenService.GenerateAccessToken(applicationUser);

                result.AccessToken = token;
                result.StatusCode = 200;
                result.Message = "Success";
            }

            return result;
        }



    }
}
