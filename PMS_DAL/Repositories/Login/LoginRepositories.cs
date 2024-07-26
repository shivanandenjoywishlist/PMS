using Microsoft.EntityFrameworkCore;
using PMS_DAL.IRepositories.Login;
using PMS_DATA;
using PMS_Entity;
using PMS_Models; // Assuming ApplicationUser or relevant models are used
using System;

namespace PMS_DAL.Repositories.Login
{
    public class LoginRepositories : ILoginRepositories
    {
        private readonly ApplicationContext _context;

        public LoginRepositories(ApplicationContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<User> LoginUser(ApplicationUser user)
        {
            return await _context.Users.Include(u => u.userRole) // Eager loading userRole
                           .FirstOrDefaultAsync(u => u.Username == user.Username && u.Password == user.Password);
        }

    }
}
