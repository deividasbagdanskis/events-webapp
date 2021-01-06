using EventsWebApp.UATests.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using Xunit;

namespace EventsWebApp.UATests
{
    public class EventDiscoveryPageTests
    {
        private readonly FirefoxOptions _options;

        public EventDiscoveryPageTests()
        {
            _options = new FirefoxOptions
            {
                AcceptInsecureCertificates = true
            };
        }

        [Fact]
        public void FilterEvents_CityEmpty_AllCategory_AllTime_ReturnsEventName()
        {
            using IWebDriver webDriver = new FirefoxDriver(_options);

            LoginPage loginPage = new LoginPage(webDriver);
            loginPage.LoginUser("v.pavardenis@gmail.com", "password");

            EventDiscoveryPage eventDiscoveryPage = new EventDiscoveryPage(webDriver);

            string city = "";
            string category = "All";
            int days = 0;

            eventDiscoveryPage.FilterByCityCategoryDate(city, category, days);

            string expectedEventName = "Lorem ipsum dolor sit amet elit.";
            string actualEventName = eventDiscoveryPage.GetEventName();

            Assert.Equal(expectedEventName, actualEventName);
        }

        [Fact]
        public void FilterEvents_CityNewYork_AllCategory_AllTime_ReturnsEventName()
        {
            using IWebDriver webDriver = new FirefoxDriver(_options);

            LoginPage loginPage = new LoginPage(webDriver);
            loginPage.LoginUser("v.pavardenis@gmail.com", "password");

            EventDiscoveryPage eventDiscoveryPage = new EventDiscoveryPage(webDriver);

            string city = "New York";
            string category = "All";
            int days = 0;

            eventDiscoveryPage.FilterByCityCategoryDate(city, category, days);

            string expectedEventName = "Lorem ipsum dolor sit amet elit.";
            string actualEventName = eventDiscoveryPage.GetEventName();

            Assert.Equal(expectedEventName, actualEventName);
        }

        [Fact]
        public void FilterEvents_CityEmpty_CategoryMusic_AllTime_ReturnsEventName()
        {
            using IWebDriver webDriver = new FirefoxDriver(_options);

            LoginPage loginPage = new LoginPage(webDriver);
            loginPage.LoginUser("v.pavardenis@gmail.com", "password");

            EventDiscoveryPage eventDiscoveryPage = new EventDiscoveryPage(webDriver);

            string city = "";
            string category = "Music";
            int days = 0;

            eventDiscoveryPage.FilterByCityCategoryDate(city, category, days);

            string expectedEventName = "Lorem ipsum dolor sit amet elit.";
            string actualEventName = eventDiscoveryPage.GetEventName();

            Assert.Equal(expectedEventName, actualEventName);
        }

        [Fact]
        public void FilterEvents_CityEmpty_CategoryAll_Time7Days_ReturnsEventName()
        {
            using IWebDriver webDriver = new FirefoxDriver(_options);

            LoginPage loginPage = new LoginPage(webDriver);
            loginPage.LoginUser("v.pavardenis@gmail.com", "password");

            EventDiscoveryPage eventDiscoveryPage = new EventDiscoveryPage(webDriver);

            string city = "";
            string category = "All";
            int days = 7;

            eventDiscoveryPage.FilterByCityCategoryDate(city, category, days);

            string expectedEventName = "Lorem ipsum dolor sit amet elit.";
            string actualEventName = eventDiscoveryPage.GetEventName();

            Assert.Equal(expectedEventName, actualEventName);
        }

        [Fact]
        public void FilterEvents_CityNewYork_CategoryMusic_Time7Days_ReturnsEventName()
        {
            using IWebDriver webDriver = new FirefoxDriver(_options);

            LoginPage loginPage = new LoginPage(webDriver);
            loginPage.LoginUser("v.pavardenis@gmail.com", "password");

            EventDiscoveryPage eventDiscoveryPage = new EventDiscoveryPage(webDriver);

            string city = "New York";
            string category = "Music";
            int days = 7;

            eventDiscoveryPage.FilterByCityCategoryDate(city, category, days);

            string expectedEventName = "Lorem ipsum dolor sit amet elit.";
            string actualEventName = eventDiscoveryPage.GetEventName();

            Assert.Equal(expectedEventName, actualEventName);
        }

        [Fact]
        public void FilterEvents_CityVilnius_CategoryAll_AllTime_ReturnsEmptyEventName()
        {
            using IWebDriver webDriver = new FirefoxDriver(_options);

            LoginPage loginPage = new LoginPage(webDriver);
            loginPage.LoginUser("v.pavardenis@gmail.com", "password");

            EventDiscoveryPage eventDiscoveryPage = new EventDiscoveryPage(webDriver);

            string city = "Vilnius";
            string category = "All";
            int days = 0;

            eventDiscoveryPage.FilterByCityCategoryDate(city, category, days);

            string expectedEventName = "";
            string actualEventName = eventDiscoveryPage.GetEventName();

            Assert.Equal(expectedEventName, actualEventName);
        }

        [Fact]
        public void FilterEvents_CityEmpty_CategoryArts_AllTime_ReturnsEmptyEventName()
        {
            using IWebDriver webDriver = new FirefoxDriver(_options);

            LoginPage loginPage = new LoginPage(webDriver);
            loginPage.LoginUser("v.pavardenis@gmail.com", "password");

            EventDiscoveryPage eventDiscoveryPage = new EventDiscoveryPage(webDriver);

            string city = "";
            string category = "Arts";
            int days = 0;

            eventDiscoveryPage.FilterByCityCategoryDate(city, category, days);

            string expectedEventName = "";
            string actualEventName = eventDiscoveryPage.GetEventName();

            Assert.Equal(expectedEventName, actualEventName);
        }

        [Fact]
        public void FilterEvents_CityEmpty_CategoryEmpty_Time30Days_ReturnsEventName()
        {
            using IWebDriver webDriver = new FirefoxDriver(_options);

            LoginPage loginPage = new LoginPage(webDriver);
            loginPage.LoginUser("v.pavardenis@gmail.com", "password");

            EventDiscoveryPage eventDiscoveryPage = new EventDiscoveryPage(webDriver);

            string city = "";
            string category = "All";
            int days = 30;

            eventDiscoveryPage.FilterByCityCategoryDate(city, category, days);

            string expectedEventName = "Lorem ipsum dolor sit amet elit.";
            string actualEventName = eventDiscoveryPage.GetEventName();

            Assert.Equal(expectedEventName, actualEventName);
        }

        [Fact]
        public void FilterEvents_CityVilnius_CategoryArts_Time14Days_ReturnsEmptyEventName()
        {
            using IWebDriver webDriver = new FirefoxDriver(_options);

            LoginPage loginPage = new LoginPage(webDriver);
            loginPage.LoginUser("v.pavardenis@gmail.com", "password");

            EventDiscoveryPage eventDiscoveryPage = new EventDiscoveryPage(webDriver);

            string city = "Vilnius";
            string category = "Arts";
            int days = 14;

            eventDiscoveryPage.FilterByCityCategoryDate(city, category, days);

            string expectedEventName = "";
            string actualEventName = eventDiscoveryPage.GetEventName();

            Assert.Equal(expectedEventName, actualEventName);
        }

        [Fact]
        public void EventDetails_ReturnsEventName()
        {
            using IWebDriver webDriver = new FirefoxDriver(_options);

            LoginPage loginPage = new LoginPage(webDriver);
            loginPage.LoginUser("v.pavardenis@gmail.com", "password");

            EventDiscoveryPage eventDiscoveryPage = new EventDiscoveryPage(webDriver);

            string expectedEventName = "Lorem ipsum dolor sit amet elit.";
            string actualEventName = eventDiscoveryPage.GetEventNameFromEventDetailsPage();

            Assert.Equal(expectedEventName, actualEventName);
        }
    }
}
