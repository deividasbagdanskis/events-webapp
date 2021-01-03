using EventsWebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace EventsWebApp.UnitTests
{
    public class HomeControllerTests
    {
        [Fact]
        public void Index_Pass()
        {
            HomeController homeController = new HomeController();

            var result = homeController.Index();

            Assert.IsType<ViewResult>(result);
        }
    }
}
