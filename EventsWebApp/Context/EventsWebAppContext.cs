using EventsWebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
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

            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = "a946a04a-1b06-44f6-b72b-388f7e5c5773",
                UserName = "V Pavardenis",
                NormalizedUserName = "V PAVARDENIS",
                Email = "v.pavardenis@gmail.com",
                NormalizedEmail = "V.PAVARDENIS@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEKGAVOKhL9/tlVFm93GZyaAOzTQ3ToRCmchisKzYrSX3bT5JgLrPpfDrDT7Aoe7YrQ==",
                SecurityStamp = "2LMI6XECPFVDBOHSLKZUVCXUK7V7547S",
                ConcurrencyStamp = "c463ddb3-ba0d-4816-8f0d-edeaf3eb6df1",
                LockoutEnabled = true
            });

            modelBuilder.Entity<Category>().HasData( new []
            {
                new Category() { Id = 1, Name = "Music" },
                new Category() { Id = 2, Name = "Arts" },
                new Category() { Id = 3, Name = "Film" },
                new Category() { Id = 4, Name = "Food" },
                new Category() { Id = 5, Name = "Fitness" },
                new Category() { Id = 6, Name = "Networking" },
                new Category() { Id = 7, Name = "Nightlife" },
                new Category() { Id = 8, Name = "Other" },
            });

            modelBuilder.Entity<Event>().HasData(new Event()
            {
                Id = 1,
                Name = "Lorem ipsum dolor sit amet elit.",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed suscipit fermentum " +
                "tortor, non pharetra erat. Donec at felis purus. Vivamus nisi lorem, congue eu nisl quis, " +
                "tincidunt accumsan est. Nam id nulla ex. Cras pretium ante quis sagittis vestibulum. In eu " +
                "vehicula massa. Duis dapibus consequat erat a eleifend.Nullam rutrum finibus magna vel viverra. " +
                "Sed quis leo laoreet,elementum nunc bibendum, aliquam nulla.Integer eu nunc arcu.Vivamus dapibus " +
                "sem leo.In ut turpis eu risus sollicitudin pellentesque eu sit amet lorem.Morbi suscipit sem sit " +
                "amet vestibulum placerat. Phasellus sed ultricies odio, at facilisis est.Integer enim ex, " +
                "malesuada vitae blandit et, tempus nec nulla.",
                City = "New York",
                Address = "720 9th Ave",
                ImageName = "ff6cda08-29ce-4436-9298-bdd467d1210f_highres_491103347.png",
                CategoryId = 1,
                DateAndTime = DateTime.Today.AddDays(6) + new TimeSpan(17, 0, 0),
                UserId = "a946a04a-1b06-44f6-b72b-388f7e5c5773"
            });
        }

        public DbSet<Event> Event { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<EventAttendee> EventAttendee { get; set; }
    }
}
