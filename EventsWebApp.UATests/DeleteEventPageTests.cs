using EventsWebApp.UATests.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.IO;
using Xunit;

namespace EventsWebApp.UATests
{
    public class DeleteEventPageTests
    {
        private readonly FirefoxOptions _options;

        public DeleteEventPageTests()
        {
            _options = new FirefoxOptions
            {
                AcceptInsecureCertificates = true
            };
        }

        [Fact]
        public void DeleteEvent_DeletesEvent()
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

            DeleteEventPage deleteEventPage = new DeleteEventPage(webDriver);
            deleteEventPage.DeleteEvent();

            bool eventWasDeleted = deleteEventPage.CheckIfEventWasDeleted(name);

            Assert.True(eventWasDeleted);
        }
    }
}
