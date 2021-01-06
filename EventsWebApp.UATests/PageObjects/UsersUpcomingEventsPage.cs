using OpenQA.Selenium;

namespace EventsWebApp.UATests.PageObjects
{
    public class UsersUpcomingEventsPage
    {
        private readonly IWebDriver _webDriver;
        private readonly string _eventDiscoveryPage = "https://localhost:44357/Events";
        private readonly By _eventLink = By.XPath("/html/body/div/main/table/tbody/tr/td[1]/a");
        private readonly By _goingButton = By.XPath("/html/body/div/main/div/div[3]/form/button");
        private readonly By _myEventsLink = By.XPath("/html/body/header/nav/div/div/ul/li[3]/a");
        private readonly By _upcomingEventName = By.XPath("/html/body/div/main/table[1]/tbody/tr/td[1]/a");

        public UsersUpcomingEventsPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public void SelectEventAsGoing()
        {
            _webDriver.Navigate().GoToUrl(_eventDiscoveryPage);
            _webDriver.FindElement(_eventLink).Click();
            _webDriver.FindElement(_goingButton).Click();
        }

        public void DeselectEventAsGoing()
        {
            _webDriver.FindElement(_myEventsLink).Click();
            _webDriver.FindElement(_upcomingEventName).Click();
            _webDriver.FindElement(_goingButton).Click();
        }

        public string GetUpcomingEventName()
        {
            _webDriver.FindElement(_myEventsLink).Click();

            string upcomingEventName;

            try
            {
                upcomingEventName = _webDriver.FindElement(_upcomingEventName).Text;
            }
            catch (NoSuchElementException)
            {
                upcomingEventName = "";
            }

            return upcomingEventName;
        }
    }
}
