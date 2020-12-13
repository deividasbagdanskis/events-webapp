using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventsWebApp.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Required]
        public string City { get; set; }
        
        [Required]
        public string Address { get; set; }
        public string ImageName { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateAndTime { get; set; }
        public List<EventAttendee> EventAttendees { get; set; }
    }
}
