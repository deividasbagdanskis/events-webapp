using EventsWebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace EventsWebApp.Context
{
    [ExcludeFromCodeCoverage]
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
        public DbSet<EventAttendee> EventAttendee { get; set; }
    }
}
