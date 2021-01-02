using EventsWebApp.Models;
using EventsWebApp.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EventsWebApp.Controllers
{
    public class EventAttendeeController : Controller
    {
        private readonly IEventAttendeeRepository _eventAttendeeRepository;

        public EventAttendeeController(IEventAttendeeRepository eventAttendeeRepository)
        {
            _eventAttendeeRepository = eventAttendeeRepository;
        }

        // POST: EventAttendeeController/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(int eventId, string userId)
        {
            if (eventId != 0 && !string.IsNullOrWhiteSpace(userId))
            {
                EventAttendee eventAttendee = new EventAttendee()
                {
                    EventId = eventId,
                    UserId = userId
                };

                try
                {
                    await _eventAttendeeRepository.Add(eventAttendee);

                    return RedirectToAction("Details", "Events", new { id = eventId});
                }
                catch
                {
                    return RedirectToAction("Details", "Events", new { id = eventId });
                }

            }

            return RedirectToAction("Details", "Events", new { id = eventId });
        }

        // POST: EventAttendeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int eventId, string userId)
        {
            if (eventId != 0 && !string.IsNullOrWhiteSpace(userId))
            {
                EventAttendee eventAttendee = await _eventAttendeeRepository.GetEventAttendee(userId, eventId);

                try
                {
                    await _eventAttendeeRepository.Delete(eventAttendee);

                    return RedirectToAction("Details", "Events", new { id = eventId });
                }
                catch
                {
                    return RedirectToAction("Details", "Events", new { id = eventId });
                }

            }

            return RedirectToAction("Details", "Events", new { id = eventId });
        }
    }
}
