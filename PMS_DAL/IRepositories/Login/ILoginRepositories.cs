using PMS_Entity;
using PMS_Models;

namespace PMS_DAL.IRepositories.Login
{
    public interface ILoginRepositories
    {
         Task<User> LoginUser(ApplicationUser user);
    }
}
