using EventsWebApp.Context;
using EventsWebApp.Models;
using EventsWebApp.Repositories;
using System.Threading.Tasks;
using Xunit;

namespace EventsWebApp.UnitTests.RepositoryTests
{
    public class EventAttendeeRepositoryTests
    {
        private readonly IEventAttendeeRepository _eventAttendeeRepository;
        private readonly EventsWebAppContext _context;

        public EventAttendeeRepositoryTests()
        {
            _context = new EventsWebAppContext(Utilities.Utilities.TestDbContextOptions());
            _eventAttendeeRepository = new EventAttendeeRepository(_context);
        }

        [Fact]
        public async Task GetEventAttendee_UserId_jekfekjrfb_EventId_1_Pass()
        {
            EventAttendee eventAttendee = GetTestEventAttendee();

            _context.EventAttendee.Add(eventAttendee);
            await _context.SaveChangesAsync();

            string userId = "jekfekjrfb";
            int eventId = 1;

            EventAttendee returnedEventAttendee = await _eventAttendeeRepository.GetEventAttendee(userId, 
                eventId);

            Assert.Equal(1, returnedEventAttendee.Id);
            Assert.Equal(userId, returnedEventAttendee.UserId);
            Assert.Equal(eventId, returnedEventAttendee.EventId);

            _context.Remove(eventAttendee);
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task Add_EventAttendee_Pass()
        {
            EventAttendee eventAttendee = GetTestEventAttendee();

            await _eventAttendeeRepository.Add(eventAttendee);

            EventAttendee returnedEventAttendee = await _eventAttendeeRepository.GetEventAttendee(eventAttendee.UserId,
                eventAttendee.EventId);

            Assert.Equal(eventAttendee.User, returnedEventAttendee.User);
            Assert.Equal(eventAttendee.Event, returnedEventAttendee.Event);
        }

        [Fact]
        public async Task Delete_EventAttendee_Pass()
        {
            EventAttendee eventAttendee = GetTestEventAttendee();

            _context.EventAttendee.Add(eventAttendee);
            await _context.SaveChangesAsync();

            await _eventAttendeeRepository.Delete(eventAttendee);

            Assert.Null(await _context.EventAttendee.FindAsync(eventAttendee.Id));
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
