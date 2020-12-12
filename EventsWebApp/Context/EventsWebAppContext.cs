using EventsWebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventsWebApp.Context
{
    public class EventsWebAppContext : IdentityDbContext
    {
        public EventsWebAppContext(DbContextOptions<EventsWebAppContext> options) : base(options)
        {
        }

        public DbSet<Event> Event { get; set; }
        public DbSet<Category> Category { get; set; }
    }
}
