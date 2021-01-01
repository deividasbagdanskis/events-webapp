using EventsWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventsWebApp.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAll();
        Task<Category> Get(int id);
    }
}