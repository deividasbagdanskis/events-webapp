using EventsWebApp.Context;
using EventsWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventsWebApp.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly EventsWebAppContext _context;

        public CategoryRepository(EventsWebAppContext context)
        {
            _context = context;
        }

        public async Task<Category> Get(int id)
        {
            return await _context.Category.FindAsync(id);
        }

        public async Task<List<Category>> GetAll()
        {
            return await _context.Category.ToListAsync();
        }
    }
}
