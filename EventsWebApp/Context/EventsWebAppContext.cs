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
        public DbSet<Category> Category { get; set; }
    }
}
