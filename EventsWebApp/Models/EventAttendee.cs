namespace EventsWebApp.Models
{
    public class EventAttendee
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
