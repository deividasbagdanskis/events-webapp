using EventsWebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventsWebApp.Context
{
    public class EventsWebAppContext : IdentityDbContext<User>
    {
        public EventsWebAppContext(DbContextOptions<EventsWebAppContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Event> Event { get; set; }
        public DbSet<Category> Category { get; set; }
    }
}
