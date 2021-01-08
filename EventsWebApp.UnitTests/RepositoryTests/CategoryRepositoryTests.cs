using EventsWebApp.Context;
using EventsWebApp.Models;
using EventsWebApp.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EventsWebApp.UnitTests.RepositoryTests
{
    public class CategoryRepositoryTests
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly EventsWebAppContext _context;

        public CategoryRepositoryTests()
        {
            _context = new EventsWebAppContext(Utilities.Utilities.TestDbContextOptions());
            _categoryRepository = new CategoryRepository(_context);
        }

        [Fact]
        public async Task Get_Id_1_Pass()
        {
            Category category = GetTestCategory();

            _context.Category.Add(category);
            await _context.SaveChangesAsync();
            
            Category returnedCategory = await _categoryRepository.Get(1);
            
            Assert.Equal(category.Id, returnedCategory.Id);
            Assert.Equal(category.Name, returnedCategory.Name);
            Assert.Equal(category.Events.Count, returnedCategory.Events.Count);

            _context.Category.Remove(category);
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task GetAll_Pass()
        {
            List<Category> categories = GetTestCategories();

            _context.Category.AddRange(categories);
            await _context.SaveChangesAsync();

            List<Category> returnedCategories = await _categoryRepository.GetAll();

            Assert.Equal(categories.Count, returnedCategories.Count);

            _context.RemoveRange(categories);
            await _context.SaveChangesAsync();
        }

        private List<Category> GetTestCategories()
        {
            List<Category> categories = new List<Category>()
            {
                new Category() { Id = 1, Name = "Music" },
                new Category() { Id = 2, Name = "Arts" },
                new Category() { Id = 3, Name = "Film" },
                new Category() { Id = 4, Name = "Food" },
                new Category() { Id = 5, Name = "Networking" }
            };

            return categories;
        }

        private Category GetTestCategory()
        {
            Category category = new Category() 
            { 
                Id = 1, 
                Name = "Music", 
                Events = new List<Event>() 
                {
                    new Event() { Id = 1, Name = "Lorem ipsum" }
                } 
            };

            return category;
        }
    }
}
