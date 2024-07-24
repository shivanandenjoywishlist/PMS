using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PMS.Controllers.Common
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BaseController : Controller
    {
       
    }
}
