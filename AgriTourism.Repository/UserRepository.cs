using Agritourism.AggregateRoot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriTourism.Repository
{
    public class UserRepository: IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
            
        }

        public async Task<User> GetUserForCreate(string username)
        {
            var user= _context.Users.FirstOrDefault(u=>u.Username==username);
            return user;
        }
    }
}
