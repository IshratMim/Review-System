using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Agritourism.AggregateRoot.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
//using static AgriTourism.Repository.IGenericRepository; 

namespace AgriTourism.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _entities;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id) => await _entities.FindAsync(id);

        public async Task<IEnumerable<T>> GetAllAsync() => await _entities.ToListAsync();
        public async Task<IEnumerable<T>> GetAllWithIncludesAsync()
        {
            var reviewWithUser = await _context.Reviews.Include(o => o.User).ToListAsync();
            /*IQueryable<T> query = _context.Set<T>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }*/
            return (IEnumerable<T>)reviewWithUser;
            //return await query.ToListAsync();
        }

        public async Task<int> AddAsync(T entity)
        {
            await _entities.AddAsync(entity);
           return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            _entities.Update(entity);
           return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(T entity)
        {
            // Ensure the entity is tracked by the context
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _entities.Attach(entity);  // Attach it if it's not being tracked
            }
           
                _entities.Remove(entity);
               return await _context.SaveChangesAsync();
            
        }
    }
}
