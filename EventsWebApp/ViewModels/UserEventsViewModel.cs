using EventsWebApp.Models;
using System.Collections.Generic;

namespace EventsWebApp.ViewModels
{
    public class UserEventsViewModel
    {
        public List<Event> UsersCreatedEvents { get; set; }
        public List<Event> UsersAttendEvents { get; set; }
    }
}
