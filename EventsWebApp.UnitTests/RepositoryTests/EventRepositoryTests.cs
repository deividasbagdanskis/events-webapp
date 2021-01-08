using EventsWebApp.Context;
using EventsWebApp.Models;
using EventsWebApp.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EventsWebApp.UnitTests.RepositoryTests
{
    public class EventRepositoryTests
    {
        private readonly EventsWebAppContext _context;
        private readonly EventRepository _eventRepository;

        public EventRepositoryTests()
        {
            _context = new EventsWebAppContext(Utilities.Utilities.TestDbContextOptions());
            _eventRepository = new EventRepository(_context);
        }

        [Fact]
        public async Task GetEvent_Id1_Pass()
        {
            Event @event = GetTestEvent();

            _context.Event.Add(@event);
            await _context.SaveChangesAsync();

            Event returnedEvent = await _eventRepository.GetEvent(@event.Id);

            Assert.Equal(@event.Name, returnedEvent.Name);
            Assert.Equal(@event.City, returnedEvent.City);

            _context.Event.Remove(@event);
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task GetAllEvents_Pass()
        {
            List<Event> events = GetTestEvents();

            foreach (Event @event in events)
            {
                @event.Category.Id = 0;
                if (@event.EventAttendees != null)
                {
                    @event.EventAttendees.ForEach(ea => ea.Id = 0);
                }
            }

            _context.Event.AddRange(events);
            await _context.SaveChangesAsync();

            List<Event> returnedEvents = await _eventRepository.GetAllEvents();

            Assert.Equal(events.Count, returnedEvents.Count);

            _context.Event.RemoveRange(events);
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task GetEventsByCategory_Id2_Pass()
        {
            List<Event> events = GetTestEvents();

            foreach (Event @event in events)
            {
                @event.Category.Id = 0;
                if (@event.EventAttendees != null)
                {
                    @event.EventAttendees.ForEach(ea => ea.Id = 0);
                }
            }

            _context.Event.AddRange(events);
            await _context.SaveChangesAsync();

            int categoryId = 2;

            List<Event> returnedEvents = await _eventRepository.GetEventsByCategory(categoryId);

            string expected = events.Where(e => e.CategoryId == categoryId).ToList()[0].Description;
            string actual = returnedEvents[0].Description;

            Assert.Equal(expected, actual);

            _context.Event.RemoveRange(events);
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task GetEventsByCategoryDate_Id1_5_Days_From_Now_Pass()
        {
            List<Event> events = GetTestEvents();

            foreach (Event @event in events)
            {
                @event.Category.Id = 0;
                if (@event.EventAttendees != null)
                {
                    @event.EventAttendees.ForEach(ea => ea.Id = 0);
                }
            }

            _context.Event.AddRange(events);
            await _context.SaveChangesAsync();

            int categoryId = 1;
            DateTime dateTimeInterval = DateTime.Today.AddDays(5);

            List<Event> returnedEvents = await _eventRepository.GetEventsByCategoryDate(categoryId, dateTimeInterval);

            string expected = events.Where(e => e.CategoryId == categoryId && e.DateAndTime <= dateTimeInterval)
                                    .ToList()[0].Address;
            string actual = returnedEvents[0].Address;

            Assert.Equal(expected, actual);

            _context.Event.RemoveRange(events);
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task GetEventsByCity_Vilnius_Pass()
        {
            List<Event> events = GetTestEvents();

            foreach (Event @event in events)
            {
                @event.Category.Id = 0;
                if (@event.EventAttendees != null)
                {
                    @event.EventAttendees.ForEach(ea => ea.Id = 0);
                }
            }

            _context.Event.AddRange(events);
            await _context.SaveChangesAsync();

            string city = "Vilnius";

            List<Event> returnedEvents = await _eventRepository.GetEventsByCity(city);

            DateTime expected = DateTime.Today.AddDays(10);
            DateTime actual = returnedEvents[0].DateAndTime;

            Assert.Equal(expected, actual);

            _context.Event.RemoveRange(events);
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task GetEventsByCityCategory_Vilnius_Id3_Pass()
        {
            List<Event> events = GetTestEvents();

            foreach (Event @event in events)
            {
                @event.Category.Id = 0;
                if (@event.EventAttendees != null)
                {
                    @event.EventAttendees.ForEach(ea => ea.Id = 0);
                }
            }

            _context.Event.AddRange(events);
            await _context.SaveChangesAsync();

            string city = "Vilnius";
            int categoryId = 3;

            List<Event> returnedEvents = await _eventRepository.GetEventsByCityCategory(city, categoryId);

            User expected = events.Where(e => e.City == city).ToList()[0].User;
            User actual = returnedEvents[0].User;

            Assert.Equal(expected, actual);

            _context.Event.RemoveRange(events);
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task GetEventsByCityCategoryDate_NewYork_Id2_5_Days_From_Now_Pass()
        {
            List<Event> events = GetTestEvents();

            foreach (Event @event in events)
            {
                @event.Category.Id = 0;
                if (@event.EventAttendees != null)
                {
                    @event.EventAttendees.ForEach(ea => ea.Id = 0);
                }
            }

            _context.Event.AddRange(events);
            await _context.SaveChangesAsync();

            string city = "New York";
            int categoryId = 2;
            DateTime dateTimeInterval = DateTime.Today.AddDays(5);

            List<Event> returnedEvents = await _eventRepository.GetEventsByCityCategoryDate(city, categoryId, 
                dateTimeInterval);

            string expected = events.Where(e => e.City == city &&
                                        e.CategoryId == categoryId &&
                                        e.DateAndTime <= dateTimeInterval)
                                        .ToList()[0].ImageName;
            string actual = returnedEvents[0].ImageName;

            Assert.Equal(expected, actual);

            _context.Event.RemoveRange(events);
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task GetEventsByCityDate_NewYork_5_Days_From_Now_Pass()
        {
            List<Event> events = GetTestEvents();

            foreach (Event @event in events)
            {
                @event.Category.Id = 0;
                if (@event.EventAttendees != null)
                {
                    @event.EventAttendees.ForEach(ea => ea.Id = 0);
                }
            }

            _context.Event.AddRange(events);
            await _context.SaveChangesAsync();

            string city = "New York";
            DateTime dateTimeInterval = DateTime.Today.AddDays(5);

            List<Event> returnedEvents = await _eventRepository.GetEventsByCityDate(city, dateTimeInterval);

            Category expected = events.Where(e => e.City == city && e.DateAndTime <= dateTimeInterval)
                                      .ToList()[0].Category;
            Category actual = returnedEvents[0].Category;

            Assert.Equal(expected, actual);

            _context.Event.RemoveRange(events);
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task GetEventsByDate_10_Days_From_Now_Pass()
        {
            List<Event> events = GetTestEvents();

            foreach (Event @event in events)
            {
                @event.Category.Id = 0;
                if (@event.EventAttendees != null)
                {
                    @event.EventAttendees.ForEach(ea => ea.Id = 0);
                }
            }

            _context.Event.AddRange(events);
            await _context.SaveChangesAsync();

            DateTime dateTimeInterval = DateTime.Today.AddDays(10);

            List<Event> returnedEvents = await _eventRepository.GetEventsByDate(dateTimeInterval);

            int expected = events.Where(e => e.DateAndTime <= dateTimeInterval)
                                        .ToList()[0].EventAttendees.Count;
            int actual = returnedEvents[0].EventAttendees.Count;

            Assert.Equal(expected, actual);

            _context.Event.RemoveRange(events);
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task GetUsersCreatedEvents_reklgnelkrngrg_Pass()
        {
            List<Event> events = GetTestEvents();

            foreach (Event @event in events)
            {
                @event.Category.Id = 0;
                if (@event.EventAttendees != null)
                {
                    @event.EventAttendees.ForEach(ea => ea.Id = 0);
                }
            }

            _context.Event.AddRange(events);
            await _context.SaveChangesAsync();

            string userId = "reklgnelkrngrg";

            List<Event> returnedEvents = await _eventRepository.GetUsersCreatedEvents(userId);

            string expected = events.Where(e => e.UserId == userId).ToList()[0].User.UserName;
            string actual = returnedEvents[0].User.UserName;

            Assert.Equal(expected, actual);

            _context.Event.RemoveRange(events);
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task GetEventsWhichUserWillAttend_kwjefnwkje_Pass()
        {
            List<Event> events = GetTestEvents();

            foreach (Event @event in events)
            {
                @event.Category.Id = 0;
                if (@event.EventAttendees != null)
                {
                    @event.EventAttendees.ForEach(ea => ea.Id = 0);
                }
            }

            _context.Event.AddRange(events);
            await _context.SaveChangesAsync();

            string userId = "kwjefnwkje";

            List<Event> returnedEvents = await _eventRepository.GetEventsWhichUserWillAttend(userId);

            string expected = _context.EventAttendee.Where(e => e.UserId == userId)
                                                .Select(e => e.Event)
                                                .ToList()[0].Name;
            string actual = returnedEvents[0].Name;

            Assert.Equal(expected, actual);

            _context.Event.RemoveRange(events);
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task Add_Event_Pass()
        {
            Event @event = GetTestEvent();

            await _eventRepository.Add(@event);

            Event returnedEvent = await _eventRepository.GetEvent(@event.Id);

            string expected = @event.Name;
            string actual = returnedEvent.Name;

            Assert.Equal(expected, actual);

            _context.Event.Remove(@event);
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task Update_Event_Pass()
        {
            Event @event = GetTestEvent();

            _context.Event.Add(@event);
            await _context.SaveChangesAsync();

            Event newEvent = @event;
            newEvent.Name = "Album listening session";

            await _eventRepository.Update(newEvent, @event);

            Event returnedEvent = await _eventRepository.GetEvent(@event.Id);

            string expected = newEvent.Name;
            string actual = returnedEvent.Name;

            Assert.Equal(expected, actual);

            _context.Event.Remove(@event);
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task Delete_Id1_Pass()
        {
            Event @event = GetTestEvent();

            _context.Event.Add(@event);
            await _context.SaveChangesAsync();

            await _eventRepository.Delete(@event.Id);

            Event returnedEvent = await _eventRepository.GetEvent(@event.Id);

            Assert.Null(returnedEvent);
        }

        private List<Event> GetTestEvents()
        {
            List<Event> events = new List<Event>()
            {
                new Event()
                {
                    Id = 1,
                    Name = "Lorem ipsum",
                    Category = new Category() { Id = 1, Name = "Music" },
                    City = "San Fransiscp",
                },
                new Event()
                {
                    Id = 2,
                    Name = "Concert",
                    Category = new Category() { Id = 2, Name = "Art" },
                    City = "New York",
                    Address = "720 9th Ave",
                    DateAndTime = DateTime.Today.AddDays(5),
                    ImageName = "image.jpg",
                    EventAttendees = new List<EventAttendee>()
                    {
                        new EventAttendee() {Id = 1, UserId = "kwjefnwkje", EventId = 2},
                        new EventAttendee() {Id = 2, UserId = "wjkfnwejkf", EventId = 2}
                    }
                },
                new Event()
                {
                    Id = 3,
                    Name = "Watch party",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    Category = new Category() { Id = 3, Name = "Film" },
                    City = "Vilnius",
                    Address = "Medziu g. 15",
                    DateAndTime = DateTime.Today.AddDays(10),
                    EventAttendees = new List<EventAttendee>()
                    {
                        new EventAttendee() {Id = 1, UserId = "kwjefnwkje", EventId = 3},
                    },
                    User = new User() {Id = "reklgnelkrngrg", UserName = "Joe Doe"}
                }
            };

            return events;
        }

        private Event GetTestEvent()
        {
            Event @event = new Event()
            {
                Id = 1,
                Name = "Lorem ipsum",
                Category = new Category() { Id = 1, Name = "Music" },
                City = "Vilnius",
            };

            return @event;
        }
    }
}
