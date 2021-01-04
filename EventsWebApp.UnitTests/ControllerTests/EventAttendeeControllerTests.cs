using EventsWebApp.Controllers;
using EventsWebApp.Models;
using EventsWebApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace EventsWebApp.UnitTests.ControllerTests
{
    public class EventAttendeeControllerTests
    {
        private readonly EventAttendeeController _eventAttendeeController;

        public EventAttendeeControllerTests()
        {
            Mock<IEventAttendeeRepository> eventAttendeeRepository = new Mock<IEventAttendeeRepository>();
            eventAttendeeRepository.Setup(er => er.GetEventAttendee(It.IsAny<string>(), It.IsAny<int>()))
                                   .ReturnsAsync(GetTestEventAttendee());

            _eventAttendeeController = new EventAttendeeController(eventAttendeeRepository.Object);
        }

        [Fact]
        public async Task Create_EventId_1_UserId_eknfekf_Pass()
        {
            int eventId = 1;
            string userId = "eknfekf";

            var result = await _eventAttendeeController.Create(eventId, userId);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Details", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Create_EventId_0_UserId_Null_Pass()
        {
            int eventId = 0;
            string userId = null;

            var result = await _eventAttendeeController.Create(eventId, userId);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Details", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Delete_EventId_1_UserId_eknfekf_Pass()
        {
            int eventId = 1;
            string userId = "eknfekf";

            var result = await _eventAttendeeController.Delete(eventId, userId);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Details", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Delete_EventId_0_UserId_Null_Pass()
        {
            int eventId = 0;
            string userId = null;

            var result = await _eventAttendeeController.Delete(eventId, userId);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Details", redirectToActionResult.ActionName);
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
    }
}
