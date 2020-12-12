using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace EventsWebApp.Models
{
    public class User : IdentityUser
    {
        public List<Event> Events { get; set; }
        public List<EventAttendee> EventAttendees { get; set; }
    }
}
