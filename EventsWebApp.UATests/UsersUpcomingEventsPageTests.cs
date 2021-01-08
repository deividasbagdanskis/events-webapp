using EventsWebApp.UATests.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Xunit;

namespace EventsWebApp.UATests
{
    public class UsersUpcomingEventsPageTests
    {
        private readonly FirefoxOptions _options;

        public UsersUpcomingEventsPageTests()
        {
            _options = new FirefoxOptions
            {
                AcceptInsecureCertificates = true
            };
        }

        [Fact]
        public void MarkEventAsGoing_UsersUpcomingEventName()
        {
            using IWebDriver webDriver = new FirefoxDriver(_options);
            
            LoginPage loginPage = new LoginPage(webDriver);
            loginPage.LoginUser("v.pavardenis@gmail.com", "password");

            UsersUpcomingEventsPage usersUpcomingEventsPage = new UsersUpcomingEventsPage(webDriver);
            usersUpcomingEventsPage.SelectEventAsGoing();

            string expectedEventName = "Lorem ipsum dolor sit amet elit.";
            string actualEventName = usersUpcomingEventsPage.GetUpcomingEventName();

            Assert.Equal(expectedEventName, actualEventName);

            usersUpcomingEventsPage.DeselectEventAsGoing();
        }

        [Fact]
        public void MarkEventAsNotGoing_EmptyUsersUpcomingEventName()
        {
            using IWebDriver webDriver = new FirefoxDriver(_options);

            LoginPage loginPage = new LoginPage(webDriver);
            loginPage.LoginUser("v.pavardenis@gmail.com", "password");

            UsersUpcomingEventsPage usersUpcomingEventsPage = new UsersUpcomingEventsPage(webDriver);
            usersUpcomingEventsPage.SelectEventAsGoing();
            usersUpcomingEventsPage.DeselectEventAsGoing();

            string expectedEventName = "";
            string actualEventName = usersUpcomingEventsPage.GetUpcomingEventName();

            Assert.Equal(expectedEventName, actualEventName);
        }
    }
}
