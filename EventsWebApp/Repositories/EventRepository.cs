using EventsWebApp.Context;
using EventsWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsWebApp.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly EventsWebAppContext _context;

        public EventRepository(EventsWebAppContext context)
        {
            _context = context;
        }

        public async void AddEvent(Event @event)
        {
            _context.Event.Add(@event);
            await _context.SaveChangesAsync();
        }

        public async void DeleteEvent(int id)
        {
            Event @event = await GetEvent(id);
            var eventAttendees = await _context.EventAttendee.Where(e => e.EventId == id).ToListAsync();

            _context.EventAttendee.RemoveRange(eventAttendees);
            _context.Event.Remove(@event);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Event>> GetAllEvents()
        {
            return await _context.Event.Include(e => e.Category).Include(e => e.EventAttendees).ToListAsync();
        }

        public async Task<Event> GetEvent(int id)
        {
            return await _context.Event.Include(e => e.EventAttendees).Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Event>> GetEventsByCategory(int categoryId)
        {
            return await _context.Event.Include(e => e.Category)
                                        .Include(e => e.EventAttendees)
                                        .Where(e => e.CategoryId == categoryId)
                                        .ToListAsync();
        }

        public async Task<List<Event>> GetEventsByCategoryDate(int categoryId, DateTime dateTimeInterval)
        {
            return await _context.Event.Include(e => e.Category)
                                        .Include(e => e.EventAttendees)
                                        .Where(e => e.CategoryId == categoryId && e.DateAndTime <= dateTimeInterval)
                                        .ToListAsync();
        }

        public async Task<List<Event>> GetEventsByCity(string city)
        {
            return await _context.Event.Include(e => e.Category)
                                        .Include(e => e.EventAttendees)
                                        .Where(e => e.City == city)
                                        .ToListAsync();
        }

        public async Task<List<Event>> GetEventsByCityCategory(string city, int categoryId)
        {
            return await _context.Event.Include(e => e.Category)
                                        .Include(e => e.EventAttendees)
                                        .Where(e => e.City == city && e.CategoryId == categoryId)
                                        .ToListAsync();
        }

        public async Task<List<Event>> GetEventsByCityCategoryDate(string city, int categoryId, DateTime dateTimeInterval)
        {
            return await _context.Event.Include(e => e.Category)
                                        .Include(e => e.EventAttendees)
                                        .Where(e => e.City == city &&
                                        e.CategoryId == categoryId &&
                                        e.DateAndTime <= dateTimeInterval)
                                        .ToListAsync();
        }

        public async Task<List<Event>> GetEventsByCityDate(string city, DateTime dateTimeInterval)
        {
            return await _context.Event.Include(e => e.Category)
                                        .Include(e => e.EventAttendees)
                                        .Where(e => e.City == city &&
                                        e.DateAndTime <= dateTimeInterval)
                                        .ToListAsync();
        }

        public async Task<List<Event>> GetEventsByDate(DateTime dateTimeInterval)
        {
            return await _context.Event.Include(e => e.Category)
                                        .Include(e => e.EventAttendees)
                                        .Where(e => e.DateAndTime <= dateTimeInterval)
                                        .ToListAsync();
        }

        public async Task<List<Event>> GetUsersCreatedEvents(string userId)
        {
            return await _context.Event.Include(e => e.Category)
                                        .Include(e => e.EventAttendees)
                                        .Where(e => e.UserId == userId)
                                        .ToListAsync();
        }

        public async void UpdateEvent(Event newEvent, Event oldEvent)
        {
            _context.Entry(oldEvent).State = EntityState.Detached;
            _context.Update(newEvent);
            await _context.SaveChangesAsync();
        }
    }
}
