using EventsWebApp.Controllers;
using EventsWebApp.Helpers;
using EventsWebApp.Models;
using EventsWebApp.Repositories;
using EventsWebApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace EventsWebApp.UnitTests.ControllerTests
{
    public class EventsControllerTests
    {
        private readonly EventsController _eventsController;

        public EventsControllerTests()
        {
            Mock<IEventRepository> eventRepository = new Mock<IEventRepository>();
            eventRepository.Setup(er => er.GetAllEvents()).ReturnsAsync(GetTestEvents());
            eventRepository.Setup(er => er.GetEventsByCityCategory(It.IsAny<string>(), It.IsAny<int>()))
                           .ReturnsAsync(GetTestEvents());
            eventRepository.Setup(er => er.GetEventsByCategory(It.IsAny<int>())).ReturnsAsync(GetTestEvents());
            eventRepository.Setup(er => er.GetEventsByCity(It.IsAny<string>())).ReturnsAsync(GetTestEvents());
            eventRepository.Setup(er => er.GetEventsByDate(It.IsAny<DateTime>())).ReturnsAsync(GetTestEvents());
            eventRepository.Setup(er => er.GetEventsByCityCategoryDate(It.IsAny<string>(), It.IsAny<int>(),
                It.IsAny<DateTime>())).ReturnsAsync(GetTestEvents());
            eventRepository.Setup(er => er.GetEventsByCityDate(It.IsAny<string>(), It.IsAny<DateTime>()))
                           .ReturnsAsync(GetTestEvents());
            eventRepository.Setup(er => er.GetEventsByCategoryDate(It.IsAny<int>(), It.IsAny<DateTime>()))
                           .ReturnsAsync(GetTestEvents());
            eventRepository.Setup(er => er.GetUsersCreatedEvents(It.IsAny<string>())).ReturnsAsync(GetTestEvents());
            eventRepository.Setup(er => er.GetEventsWhichUserWillAttend(It.IsAny<string>()))
                           .ReturnsAsync(GetTestEvents());
            eventRepository.Setup(er => er.GetEvent(It.IsAny<int>())).ReturnsAsync(GetTestEvents()[0]);

            Mock<IEventAttendeeRepository> eventAttendeeRepository = new Mock<IEventAttendeeRepository>();
            eventAttendeeRepository.Setup(er => er.GetEventAttendee(It.IsAny<string>(), It.IsAny<int>()))
                                   .ReturnsAsync(GetTestEventAttendee());

            Mock<ICategoryRepository> categoryRepository = new Mock<ICategoryRepository>();
            categoryRepository.Setup(cr => cr.GetAll()).ReturnsAsync(GetTestCategories());
            categoryRepository.Setup(cr => cr.Get(It.IsAny<int>())).ReturnsAsync(GetTestCategories()[0]);

            Mock<IHttpContextAccessor> httpContextAccessor = new Mock<IHttpContextAccessor>();
            httpContextAccessor.Setup(h => h.HttpContext.User.FindFirst(It.IsAny<string>()))
                               .Returns(new Claim("name", "Joe Doe"));
            
            Mock<IImageHelper> imageHelper = new Mock<IImageHelper>();

            _eventsController = new EventsController(eventRepository.Object,
                eventAttendeeRepository.Object, categoryRepository.Object, httpContextAccessor.Object,
                imageHelper.Object);
        }

        [Fact]
        public async Task Index_Pass()
        {
            var result = await _eventsController.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = (List<Event>)Assert.IsAssignableFrom<IEnumerable<Event>>(
                viewResult.ViewData.Model);

            Assert.Equal(3, model.Count);
        }

        [Fact]
        public async Task Index_City_Vilnius_Category_1_Pass()
        {
            var result = await _eventsController.Index("Vilnius", 1);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = (List<Event>)Assert.IsAssignableFrom<IEnumerable<Event>>(
                viewResult.ViewData.Model);

            Assert.Equal(3, model.Count);
        }

        [Fact]
        public async Task Index_Category_1_Pass()
        {
            var result = await _eventsController.Index(null, 1, 0);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = (List<Event>)Assert.IsAssignableFrom<IEnumerable<Event>>(
                viewResult.ViewData.Model);

            Assert.Equal(3, model.Count);
        }

        [Fact]
        public async Task Index_City_Vilnius_Pass()
        {
            var result = await _eventsController.Index("Vilnius");

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = (List<Event>)Assert.IsAssignableFrom<IEnumerable<Event>>(
                viewResult.ViewData.Model);

            Assert.Equal(3, model.Count);
        }

        [Fact]
        public async Task Index_Date_7_Pass()
        {
            var result = await _eventsController.Index(null, 0, 7);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = (List<Event>)Assert.IsAssignableFrom<IEnumerable<Event>>(
                viewResult.ViewData.Model);

            Assert.Equal(3, model.Count);
        }

        [Fact]
        public async Task Index_City_Vilnius_Category_1_Date_7_Pass()
        {
            var result = await _eventsController.Index("Vilnius", 1, 7);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = (List<Event>)Assert.IsAssignableFrom<IEnumerable<Event>>(
                viewResult.ViewData.Model);

            Assert.Equal(3, model.Count);
        }

        [Fact]
        public async Task Index_City_Vilnius_Date_7_Pass()
        {
            var result = await _eventsController.Index("Vilnius", 0, 7);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = (List<Event>)Assert.IsAssignableFrom<IEnumerable<Event>>(
                viewResult.ViewData.Model);

            Assert.Equal(3, model.Count);
        }

        [Fact]
        public async Task Index_Category_1_Date_7_Pass()
        {
            var result = await _eventsController.Index(null, 1, 7);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = (List<Event>)Assert.IsAssignableFrom<IEnumerable<Event>>(
                viewResult.ViewData.Model);

            Assert.Equal(3, model.Count);
        }

        [Fact]
        public async Task IndexUserEvents_Pass()
        {
            var result = await _eventsController.IndexUserEvents();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<UserEventsViewModel>(viewResult.ViewData.Model);

            Assert.Equal(3, model.UsersAttendEvents.Count);
            Assert.Equal(3, model.UsersCreatedEvents.Count);
        }

        [Fact]
        public async Task Details_Id_1_Pass()
        {
            int eventId = 1;
            var result = await _eventsController.Details(eventId);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Event>(viewResult.ViewData.Model);

            Assert.Equal(eventId, model.Id);
        }

        [Fact]
        public async Task Details_Id_Null_Pass()
        {
            var result = await _eventsController.Details(null);

            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task Create_Pass()
        {
            var result = await _eventsController.Create();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Create_Event_Date_Time_ImageFile_Pass()
        {
            Event @event = GetTestEvent();
            DateTime date = new DateTime(2021, 1, 9);
            TimeSpan time = new TimeSpan(17, 0, 0);
            IFormFile imageFile = GetTestImage();

            var result = await _eventsController.Create(@event, date, time, imageFile);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("IndexUserEvents", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Create_EventName_Null_Date_Time_ImageFile_Pass()
        {
            Event @event = GetTestEvent();
            @event.Name = null;

            DateTime date = new DateTime(2021, 1, 9);
            TimeSpan time = new TimeSpan(17, 0, 0);
            IFormFile imageFile = GetTestImage();

            _eventsController.ViewData.ModelState.AddModelError("error", "Name is required");

            var result = await _eventsController.Create(@event, date, time, imageFile);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Event>(viewResult.ViewData.Model);
            
            _eventsController.ViewData.ModelState.Remove("error");

            Assert.Equal(@event.Id, model.Id);
        }

        [Fact]
        public async Task Edit_Id1_Pass()
        {
            int eventId = 1;

            var result = await _eventsController.Edit(eventId);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Event>(viewResult.ViewData.Model);

            Assert.Equal(eventId, model.Id);
        }

        [Fact]
        public async Task Edit_Null_Pass()
        {
            var result = await _eventsController.Edit(null);

            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task Edit_Event_Date_Time_ImageFile_Pass()
        {
            Event @event = GetTestEvent();
            DateTime date = new DateTime(2021, 1, 9);
            TimeSpan time = new TimeSpan(17, 0, 0);
            IFormFile imageFile = GetTestImage();

            var result = await _eventsController.Edit(@event.Id , @event, date, time, imageFile);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("IndexUserEvents", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Edit_Event_Date_Time_ImageFile_Null_Pass()
        {
            Event @event = GetTestEvent();
            DateTime date = new DateTime(2021, 1, 9);
            TimeSpan time = new TimeSpan(17, 0, 0);
            IFormFile imageFile = null;

            var result = await _eventsController.Edit(@event.Id, @event, date, time, imageFile);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("IndexUserEvents", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Edit_EventName_Null_Date_Time_ImageFile_Pass()
        {
            Event @event = GetTestEvent();
            @event.Name = "";

            DateTime date = new DateTime(2021, 1, 9);
            TimeSpan time = new TimeSpan(17, 0, 0);
            IFormFile imageFile = GetTestImage();

            _eventsController.ViewData.ModelState.AddModelError("error", "Name is required");

            var result = await _eventsController.Edit(@event.Id ,@event, date, time, imageFile);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Event>(viewResult.ViewData.Model);

            _eventsController.ViewData.ModelState.Remove("error");

            Assert.Equal(@event.Id, model.Id);
        }

        [Fact]
        public async Task Delete_Id1_Pass()
        {
            int? eventId = 1;

            var result = await _eventsController.Delete(eventId);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Event>(viewResult.ViewData.Model);

            Assert.Equal(eventId, model.Id);
        }

        [Fact]
        public async Task Delete_Null_Pass()
        {
            var result = await _eventsController.Delete(null);

            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task DeleteEvent_Id1_Pass()
        {
            int eventId = 1;

            var result = await _eventsController.Delete(eventId);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("IndexUserEvents", redirectToActionResult.ActionName);
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
                    City = "Vilnius",
                    ImageName = "image.jpg"
                },
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

            return events;
        }

        private Event GetTestEvent()
        {
            Event @event = new Event()
            {
                Id = 1,
                Name = "Lorem ipsum",
                CategoryId = 1,
                Category = new Category() { Id = 1, Name = "Music" },
                Description = "Suspendisse felis odio, cursus sed porttitor eu, porta in neque. In eget justo " +
                "pretium, volutpat diam eget, tempor est.",
                City = "Vilnius",
                Address = "Medziu g. 15",
            };

            return @event;
        }

        private List<Category> GetTestCategories()
        {
            List<Category> categories = new List<Category>()
            {
                new Category() { Id = 1, Name = "Music" },
                new Category() { Id = 2, Name = "Arts" },
                new Category() { Id = 3, Name = "Film" },
                new Category() { Id = 4, Name = "Food" },
                new Category() { Id = 5, Name = "Networking" }
            };

            return categories;
        }

        private EventAttendee GetTestEventAttendee()
        {
            EventAttendee eventAttendee = new EventAttendee()
            {
                Id = 1,
                EventId = 1,
                Event = new Event()
                {
                    Id = 1,
                    Name = "Lorem ipsum",
                    Category = new Category() { Id = 1, Name = "Music" },
                    City = "Vilnius"
                },
                UserId = "jekfekjrfb",
                User = new User()
                {
                    Id = "jekfekjrfb",
                    UserName = "Joe Doe"
                }
            };

            return eventAttendee;
        }

        private IFormFile GetTestImage()
        {
            IFormFile imageFile = new FormFile(null, 0, 456, "image", "image.jpg");

            return imageFile;
        }
    }
}
