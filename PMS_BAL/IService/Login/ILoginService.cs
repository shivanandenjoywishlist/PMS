using COMMON;
using PMS_BAL.IService.Common;
using PMS_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS_BAL.IService.Login
{
    public interface ILoginService : IBaseService
    {
        Task<JsonModel> Login(ApplicationUser applicationUser);
    }
}
