using EventsWebApp.Models;
using EventsWebApp.Repositories;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EventsWebApp.UnitTests.RepositoryTests
{
    public class CategoryRepositoryTests
    {
        private readonly Mock<ICategoryRepository> _categoryRepository;
        private readonly Category _category;
        private readonly List<Category> _categories;

        public CategoryRepositoryTests()
        {
            _categoryRepository = new Mock<ICategoryRepository>();
            _category = new Category() { Id = 1, Name = "Music" };
            _categories = new List<Category>()
            {
                _category,
                new Category() { Id = 2, Name = "Arts" },
                new Category() { Id = 3, Name = "Film" },
                new Category() { Id = 4, Name = "Food" },
                new Category() { Id = 5, Name = "Networking" }
            };
        }

        [Fact]
        public async Task Get_Id_1_Pass()
        {
            _categoryRepository.Setup(cr => cr.Get(It.IsAny<int>())).ReturnsAsync(_category);

            Category returnedCategory = await _categoryRepository.Object.Get(1);

            Assert.Equal(_category.Name, returnedCategory.Name);
        }

        [Fact]
        public async Task GetAll_Pass()
        {
            _categoryRepository.Setup(cr => cr.GetAll()).ReturnsAsync(_categories);

            List<Category> returnedCategories = await _categoryRepository.Object.GetAll();

            Assert.Equal(_categories.Count, returnedCategories.Count);
        }
    }
}
