using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace EventsWebApp.UATests.PageObjects
{
    public class EventDiscoveryPage
    {
        private readonly IWebDriver _webDriver;
        private readonly string _eventDiscoveryPage = "https://localhost:44357/Events";
        private readonly By _cityInput = By.Id("city");
        private readonly By _categoryDropdown = By.Id("categoryId");
        private readonly By _dateDropdown = By.Id("date");
        private readonly By _filterButton = By.XPath("/html/body/div/main/form/button");
        private readonly By _eventName = By.XPath("/html/body/div/main/table/tbody/tr/td[1]/a");
        private readonly By _eventNameHeading = By.XPath("/html/body/div/main/div/h2");

        public EventDiscoveryPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public void FilterByCityCategoryDate(string city, string category, int days)
        {
            _webDriver.Navigate().GoToUrl(_eventDiscoveryPage);
            _webDriver.FindElement(_cityInput).SendKeys(city);

            IWebElement categoryDropdown = _webDriver.FindElement(_categoryDropdown);

            SelectElement categoryOptions = new SelectElement(categoryDropdown);
            categoryOptions.SelectByText(category);

            IWebElement dateDropdown = _webDriver.FindElement(_dateDropdown);

            SelectElement dateOptions = new SelectElement(dateDropdown);
            dateOptions.SelectByValue(days.ToString());

            _webDriver.FindElement(_filterButton).Click();
        }

        public string GetEventName()
        {
            string eventName;

            try
            {
                eventName = _webDriver.FindElement(_eventName).Text;
            }
            catch (NoSuchElementException)
            {
                eventName = "";
            }

            return eventName; 
        }

        public string GetEventNameFromEventDetailsPage()
        {
            _webDriver.Navigate().GoToUrl(_eventDiscoveryPage);

            _webDriver.FindElement(_eventName).Click();

            return _webDriver.FindElement(_eventNameHeading).Text;
        }
    }
}
