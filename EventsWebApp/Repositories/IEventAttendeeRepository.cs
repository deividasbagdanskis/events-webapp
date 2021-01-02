using EventsWebApp.Models;
using System.Threading.Tasks;

namespace EventsWebApp.Repositories
{
    public interface IEventAttendeeRepository
    {
        Task<EventAttendee> GetUserAttendEvent(string userId, int eventId);
        Task<EventAttendee> GetEventAttendee(string userId, int eventId);
        Task Add(EventAttendee eventAttendee);
        Task Delete(EventAttendee eventAttendee);
    }
}