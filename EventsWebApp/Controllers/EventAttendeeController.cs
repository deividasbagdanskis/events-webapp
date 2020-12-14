using EventsWebApp.Context;
using EventsWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EventsWebApp.Controllers
{
    public class EventAttendeeController : Controller
    {
        private readonly EventsWebAppContext _context;

        public EventAttendeeController(EventsWebAppContext context)
        {
            _context = context;
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
                    _context.EventAttendee.Add(eventAttendee);
                    await _context.SaveChangesAsync();

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
                EventAttendee eventAttendee = _context.EventAttendee.Where(e => e.EventId == eventId && e.UserId == userId).FirstOrDefault();

                try
                {
                    _context.EventAttendee.Remove(eventAttendee);

                    await _context.SaveChangesAsync();

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
