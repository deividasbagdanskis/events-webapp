using Microsoft.AspNetCore.Http;

namespace EventsWebApp.Helpers
{
    public interface IImageHelper
    {
        string Save(IFormFile imageFile);
        void Delete(string imageName);
    }
}