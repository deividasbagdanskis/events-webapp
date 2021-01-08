using System;
using System.Diagnostics.CodeAnalysis;
using EventsWebApp.Context;
using EventsWebApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(EventsWebApp.Areas.Identity.IdentityHostingStartup))]
namespace EventsWebApp.Areas.Identity
{
    [ExcludeFromCodeCoverage]
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContextPool<EventsWebAppContext>(options =>
                    options.UseMySql(context.Configuration.GetConnectionString("EventsWebAppContext"), 
                    new MySqlServerVersion(new Version(8, 0, 21))));

                services.AddIdentity<User, IdentityRole>(options =>
                {
                    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ ";
                    options.SignIn.RequireConfirmedAccount = true;
                    options.Password.RequireDigit = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.User.RequireUniqueEmail = true;

                }).AddEntityFrameworkStores<EventsWebAppContext>().AddDefaultTokenProviders(); ;

                services.AddAuthorization();
            });
        }
    }
}