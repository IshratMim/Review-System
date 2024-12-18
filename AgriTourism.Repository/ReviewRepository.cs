using Agritourism.AggregateRoot.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriTourism.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context) 
        {
            _context = context;
            
        }

       public async Task<IEnumerable<Review>> GetAllWithUsernameAsync(string username)
        {
            var reviews= _context.Reviews.Include(r=>r.User).Where(r=>r.User.Username==username).ToListAsync();
            return await reviews;
        }

    }
}
