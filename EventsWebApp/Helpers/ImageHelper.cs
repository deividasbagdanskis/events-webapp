using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace EventsWebApp.Helpers
{
    public class ImageHelper : IImageHelper
    {
        private readonly string _imageFolder;

        public ImageHelper(IWebHostEnvironment webHostEnvironment)
        {
            _imageFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
        }

        public string Save(IFormFile imageFile)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
            string filePath = Path.Combine(_imageFolder, uniqueFileName);

            using FileStream fileStream = new FileStream(filePath, FileMode.Create);

            imageFile.CopyTo(fileStream);

            return uniqueFileName;
        }

        public bool Delete(string imageName)
        {
            string imagePath = Path.Combine(_imageFolder, imageName);

            FileInfo fileInfo = new FileInfo(imagePath);

            fileInfo.Delete();

            return !fileInfo.Exists;
        }
    }
}
