using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace EventsWebApp.Models
{
    [ExcludeFromCodeCoverage]
    public class User : IdentityUser
    {
        public List<Event> Events { get; set; }
        public List<EventAttendee> EventAttendees { get; set; }
    }
}
