using System;
using System.Collections.Generic;

namespace EventsWebApp.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string ImageName { get; set; }
        public DateTime DateAndTime { get; set; }
        public List<EventAttendee> EventAttendees { get; set; }
    }
}
