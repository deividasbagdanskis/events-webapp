﻿using EventsWebApp.Context;
using EventsWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EventsWebApp.Repositories
{
    public class EventAttendeeRepository : IEventAttendeeRepository
    {
        private readonly EventsWebAppContext _context;

        public EventAttendeeRepository(EventsWebAppContext context)
        {
            _context = context;
        }

        public async Task Add(EventAttendee eventAttendee)
        {
            _context.EventAttendee.Add(eventAttendee);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(EventAttendee eventAttendee)
        {
            _context.EventAttendee.Remove(eventAttendee);
            await _context.SaveChangesAsync();
        }

        public async Task<EventAttendee> GetEventAttendee(string userId, int eventId)
        {
            return await _context.EventAttendee.Include(e => e.Event)
                                                .Where(e => e.UserId == userId && e.EventId == eventId)
                                                .FirstOrDefaultAsync();
        }
    }
}
