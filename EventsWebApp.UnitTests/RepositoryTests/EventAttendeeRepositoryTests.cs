using EventsWebApp.Models;
using EventsWebApp.Repositories;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace EventsWebApp.UnitTests.RepositoryTests
{
    public class EventAttendeeRepositoryTests
    {
        private readonly Mock<IEventAttendeeRepository> _eventAttendeeRepository;
        private readonly EventAttendee _eventAttendee;

        public EventAttendeeRepositoryTests()
        {
            _eventAttendeeRepository = new Mock<IEventAttendeeRepository>();
            _eventAttendee = new EventAttendee()
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
        }

        [Fact]
        public async Task GetEventAttendee_UserId_jekfekjrfb_EventId_1_Pass()
        {
            _eventAttendeeRepository.Setup(er => er.GetEventAttendee(It.IsAny<string>(), It.IsAny<int>()))
                                    .ReturnsAsync(_eventAttendee);

            string userId = "jekfekjrfb";
            int eventId = 1;

            EventAttendee returnedEventAttendee = await _eventAttendeeRepository.Object.GetEventAttendee(userId, 
                eventId);

            Assert.Equal(userId, returnedEventAttendee.UserId);
        }

        [Fact]
        public async Task Add_EventAttendee_Pass()
        {
            await _eventAttendeeRepository.Object.Add(_eventAttendee);

            _eventAttendeeRepository.Verify(er => er.Add(_eventAttendee), Times.Once);
        }

        [Fact]
        public async Task Delete_EventAttendee_Pass()
        {
            await _eventAttendeeRepository.Object.Delete(_eventAttendee);

            _eventAttendeeRepository.Verify(er => er.Delete(_eventAttendee), Times.Once);
        }
    }
}
