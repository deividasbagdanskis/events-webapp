using Microsoft.AspNetCore.Http;

namespace EventsWebApp.Helpers
{
    public interface IImageHelper
    {
        string Save(IFormFile imageFile);
        bool Delete(string imageName);
    }
}