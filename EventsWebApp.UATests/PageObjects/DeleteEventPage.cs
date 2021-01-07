using OpenQA.Selenium;

namespace EventsWebApp.UATests.PageObjects
{
    public class DeleteEventPage
    {
        private readonly IWebDriver _webDriver;
        private readonly string _usersEventsPageUrl = "https://localhost:44357/Events/IndexUserEvents";
        private readonly By _createdEventNameLink = By.XPath("/html/body/div/main/table[2]/tbody/tr/td[1]/a");
        private readonly By _deleteEventPageLink = By.XPath("/html/body/div/main/div/div[4]/a[2]");
        private readonly By _deleteEventButton = By.XPath("/html/body/div/main/div/div[4]/form/input[1]");

        public DeleteEventPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public void DeleteEvent()
        {
            _webDriver.Navigate().GoToUrl(_usersEventsPageUrl);

            _webDriver.FindElement(_createdEventNameLink).Click();
            _webDriver.FindElement(_deleteEventPageLink).Click();
            _webDriver.FindElement(_deleteEventButton).Click();
        }

        public bool CheckIfEventWasDeleted(string name)
        {
            bool wasDeleted = false;

            By deletedEventRecord = By.XPath($"//td[normalize-space() = '{name}']");

            try
            {
                IWebElement element = _webDriver.FindElement(deletedEventRecord);
            }
            catch (NoSuchElementException)
            {
                wasDeleted = true;
            }

            return wasDeleted;
        }
    }
}
