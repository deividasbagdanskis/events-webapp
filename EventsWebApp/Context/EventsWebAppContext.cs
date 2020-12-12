using EventsWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EventsWebApp.Context
{
    public class EventsWebAppContext : DbContext
    {
        public EventsWebAppContext(DbContextOptions<EventsWebAppContext> options) : base(options)
        {
        }

        public DbSet<Event> Event { get; set; }
    }
}
