using EventsWebApp.Models;
using EventsWebApp.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EventsWebApp.UnitTests
{
    public class EventRepositoryTests
    {
        private readonly Mock<IEventRepository> _eventRepository;
        private readonly Event _event;
        private readonly List<Event> _events;

        public EventRepositoryTests()
        {
            _eventRepository = new Mock<IEventRepository>();
            _event = new Event()
            {
                Id = 1,
                Name = "Lorem ipsum",
                Category = new Category() { Id = 1, Name = "Music" },
                City = "Vilnius",
            };
            _events = new List<Event>()
            {
                _event,
                new Event()
                {
                    Id = 2,
                    Name = "Concert",
                    Category = new Category() { Id = 1, Name = "Music" },
                    City = "New York",
                    Address = "720 9th Ave",
                    DateAndTime = new DateTime(2021, 1, 7, 17, 0, 0),
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
                    Category = new Category() { Id = 2, Name = "Film" },
                    City = "Vilnius",
                    Address = "Medziu g. 15",
                    DateAndTime = new DateTime(2021, 1, 10, 16, 0, 0),
                    EventAttendees = new List<EventAttendee>()
                    {
                        new EventAttendee() {Id = 1, UserId = "kwjefnwkje", EventId = 3},
                    },
                    User = new User() {Id = "reklgnelkrngrg", UserName = "Joe Doe"}
                }
            };
        }

        [Fact]
        public async Task GetEvent_Id1_Pass()
        {
            _eventRepository.Setup(e => e.GetEvent(It.IsAny<int>())).ReturnsAsync(_event);

            int eventId = 1;

            Event returnedEvent = await _eventRepository.Object.GetEvent(eventId);

            Assert.Equal(_event.Name, returnedEvent.Name);
        }

        [Fact]
        public async Task GetAllEvents_Pass()
        {
            _eventRepository.Setup(e => e.GetAllEvents()).ReturnsAsync(_events);

            List<Event> returnedEvents = await _eventRepository.Object.GetAllEvents();

            Assert.Equal(_events.Count, returnedEvents.Count);
        }

        [Fact]
        public async Task GetEventsByCategory_Id2_Pass()
        {
            _eventRepository.Setup(e => e.GetEventsByCategory(It.IsAny<int>())).ReturnsAsync(_events);

            int categoryId = 2;

            List<Event> returnedEvents = await _eventRepository.Object.GetEventsByCategory(categoryId);

            Assert.NotNull(returnedEvents.Where(re => re.Category.Id == categoryId));
        }

        [Fact]
        public async Task GetEventsByCategoryDate_Id2_2021_1_7_Pass()
        {
            _eventRepository.Setup(e => e.GetEventsByCategoryDate(It.IsAny<int>(), It.IsAny<DateTime>()))
                            .ReturnsAsync(_events);

            int categoryId = 2;
            DateTime dateTime = new DateTime(2021, 1, 7, 17, 0, 0);

            List<Event> returnedEvents = await _eventRepository.Object.GetEventsByCategoryDate(categoryId, dateTime);

            Assert.NotNull(returnedEvents.Where(re => re.Category.Id == categoryId && re.DateAndTime == dateTime));
        }

        [Fact]
        public async Task GetEventsByCity_Vilnius_Pass()
        {
            _eventRepository.Setup(e => e.GetEventsByCity(It.IsAny<string>())).ReturnsAsync(_events);

            string city = "Vilnius";

            List<Event> returnedEvents = await _eventRepository.Object.GetEventsByCity(city);

            Assert.NotNull(returnedEvents.Where(re => re.City == city));
        }

        [Fact]
        public async Task GetEventsByCityCategory_Vilnius_Id1_Pass()
        {
            _eventRepository.Setup(e => e.GetEventsByCityCategory(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(_events);

            string city = "Vilnius";
            int categoryId = 1;

            List<Event> returnedEvents = await _eventRepository.Object.GetEventsByCityCategory(city, categoryId);

            Assert.NotNull(returnedEvents.Where(re => re.City == city && re.Category.Id == categoryId));
        }

        [Fact]
        public async Task GetEventsByCityCategoryDate_NewYork_Id1_2021_01_07_Pass()
        {
            _eventRepository.Setup(e => e.GetEventsByCityCategoryDate(It.IsAny<string>(), It.IsAny<int>(), 
                                    It.IsAny<DateTime>())).ReturnsAsync(_events);

            string city = "New York";
            int categoryId = 1;
            DateTime dateTime = new DateTime(2021, 1, 7, 17, 0, 0);

            List<Event> returnedEvents = await _eventRepository.Object.GetEventsByCityCategoryDate(city, categoryId, 
                dateTime);

            Assert.NotNull(returnedEvents.Where(re => re.City == city && re.Category.Id == categoryId 
            && re.DateAndTime == dateTime));
        }

        [Fact]
        public async Task GetEventsByCityDate_NewYork_2021_01_07_Pass()
        {
            _eventRepository.Setup(e => e.GetEventsByCityDate(It.IsAny<string>(), It.IsAny<DateTime>()))
                            .ReturnsAsync(_events);

            string city = "New York";
            DateTime dateTime = new DateTime(2021, 1, 7, 17, 0, 0);

            List<Event> returnedEvents = await _eventRepository.Object.GetEventsByCityDate(city, dateTime);

            Assert.NotNull(returnedEvents.Where(re => re.City == city && re.DateAndTime == dateTime));
        }

        [Fact]
        public async Task GetEventsByDate_2021_01_07_Pass()
        {
            _eventRepository.Setup(e => e.GetEventsByDate(It.IsAny<DateTime>()))
                            .ReturnsAsync(_events);

            DateTime dateTime = new DateTime(2021, 1, 7, 17, 0, 0);

            List<Event> returnedEvents = await _eventRepository.Object.GetEventsByDate(dateTime);

            Assert.NotNull(returnedEvents.Where(re => re.DateAndTime == dateTime));
        }

        [Fact]
        public async Task GetUsersCreatedEvents_reklgnelkrngrg_Pass()
        {
            _eventRepository.Setup(e => e.GetUsersCreatedEvents(It.IsAny<string>()))
                            .ReturnsAsync(_events);

            string userId = "reklgnelkrngrg";

            List<Event> returnedEvents = await _eventRepository.Object.GetUsersCreatedEvents(userId);

            Assert.NotNull(returnedEvents.Where(re => re.User.Id == userId));
        }

        [Fact]
        public async Task GetEventsWhichUserWillAttend_kwjefnwkje_Pass()
        {
            _eventRepository.Setup(e => e.GetEventsWhichUserWillAttend(It.IsAny<string>()))
                            .ReturnsAsync(_events);

            string userId = "kwjefnwkje";

            List<Event> returnedEvents = await _eventRepository.Object.GetEventsWhichUserWillAttend(userId);

            Assert.NotNull(returnedEvents.Where(re => re.User.Id == userId));
        }

        [Fact]
        public async Task Add_Event_Pass()
        {
            await _eventRepository.Object.Add(_event);

            _eventRepository.Verify(er => er.Add(_event), Times.Once);
        }

        [Fact]
        public async Task Update_Event_Pass()
        {
            Event newEvent = _event;
            newEvent.Name = "Album listening session";
            await _eventRepository.Object.Update(newEvent, _event);

            _eventRepository.Verify(er => er.Update(newEvent, _event), Times.Once);
        }

        [Fact]
        public async Task Delete_Id1_Pass()
        {
            int eventId = 1;

            await _eventRepository.Object.Delete(eventId);

            _eventRepository.Verify(er => er.Delete(eventId), Times.Once);
        }
    }
}
