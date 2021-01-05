using EventsWebApp.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Moq;
using System.IO;
using Xunit;

namespace EventsWebApp.UnitTests.HelpersTests
{
    public class ImageHelperTests
    {
        private readonly ImageHelper _imageHelper;
        private readonly string _imagePath;

        public ImageHelperTests()
        {
            Mock<IWebHostEnvironment> webHostEnvironment = new Mock<IWebHostEnvironment>();

            webHostEnvironment.Setup(whe => whe.WebRootPath).Returns(@"..\..\..\..\EventsWebApp\wwwroot");

            _imageHelper = new ImageHelper(webHostEnvironment.Object);

            _imagePath = @"..\..\..\HelpersTests\test_image.jpg";
        }

        [Fact]
        public void Save_ImageFile_Pass()
        {
            using FileStream fileStream = new FileStream(_imagePath, FileMode.Open);

            IFormFile formFile = new FormFile(fileStream, 0, 9568, "test_image", "test_image.jpg");

            string newImageName = _imageHelper.Save(formFile);
            _imageHelper.Delete(newImageName);

            Assert.NotNull(newImageName);
        }

        [Fact]
        public void Delete_ImageFile_Pass()
        {
            using FileStream fileStream = new FileStream(_imagePath, FileMode.Open);

            IFormFile formFile = new FormFile(fileStream, 0, 9568, "test_image", "test_image.jpg");

            string newImageName = _imageHelper.Save(formFile);
            bool imageWasDeleted = _imageHelper.Delete(newImageName);

            Assert.True(imageWasDeleted);
        }
    }
}
