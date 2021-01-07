using EventsWebApp.UATests.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace EventsWebApp.UATests
{
    public class CreateEventPageTests
    {
        private readonly FirefoxOptions _options;

        public CreateEventPageTests()
        {
            _options = new FirefoxOptions
            {
                AcceptInsecureCertificates = true
            };
        }

        [Fact]
        public void CreateEvent_ValidInputs_ReturnsCreatedEventsName()
        {
            using IWebDriver webDriver = new FirefoxDriver(_options);

            LoginPage loginPage = new LoginPage(webDriver);
            loginPage.LoginUser("v.pavardenis@gmail.com", "password");

            CreateEventPage createEventPage = new CreateEventPage(webDriver);

            string name = "Test event";
            string description = "Proin at lorem gravida, lacinia dolor quis, sodales quam. Vestibulum in purus " +
                "est. Quisque turpis mi, hendrerit at aliquet vitae, porttitor commodo metus.";
            string category = "Arts";
            string city = "Vilnius";
            string address = "Medziu g. 15";

            FileInfo fileInfo = new FileInfo(@"..\..\..\event_image.png");

            string imageFilePath = fileInfo.FullName;
            DateTime date = DateTime.Today.AddDays(5);
            TimeSpan time = new TimeSpan(17, 0, 0);

            createEventPage.CreateEvent(name, description, category, city, address, imageFilePath, date, time);

            string expectedEventName = name;
            string actualEventName = createEventPage.GetCreatedEventName(name);

            Assert.Equal(expectedEventName, actualEventName);

            DeleteEventPage deleteEventPage = new DeleteEventPage(webDriver);
            deleteEventPage.DeleteEvent();
        }

        [Fact]
        public void CreateEvent_InvalidInputs_ReturnsErrorMessages()
        {
            using IWebDriver webDriver = new FirefoxDriver(_options);

            LoginPage loginPage = new LoginPage(webDriver);
            loginPage.LoginUser("v.pavardenis@gmail.com", "password");

            CreateEventPage createEventPage = new CreateEventPage(webDriver);

            string name = "";
            string description = "";
            string category = "Music";
            string city = "";
            string address = "";
            string imageFilePath = "";
            DateTime date = DateTime.Today.AddDays(5);
            TimeSpan time = new TimeSpan(17, 0, 0);

            createEventPage.CreateEvent(name, description, category, city, address, imageFilePath, date, time);

            List<string> expectedErrorMessages = new List<string>() 
            {
                "The Name field is required.",
                "The Description field is required.",
                "The City field is required.",
                "The Address field is required."
            };
            List<string> actualErrorMessages = createEventPage.GetErrorMessages();

            Assert.Equal(expectedErrorMessages, actualErrorMessages);
        }
    }
}
