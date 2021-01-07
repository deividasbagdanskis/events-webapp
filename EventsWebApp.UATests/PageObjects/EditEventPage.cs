using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace EventsWebApp.UATests.PageObjects
{
    class EditEventPage
    {
        private readonly IWebDriver _webDriver;
        private readonly string _myEventsUrl = "https://localhost:44357/Events/IndexUserEvents";
        private readonly By _eventNameLink = By.XPath("/html/body/div/main/table[2]/tbody/tr/td[1]/a");
        private readonly By _editEventLink = By.XPath("/html/body/div/main/div/div[4]/a[1]");
        private readonly By _nameInput = By.Id("Name");
        private readonly By _descriptionInput = By.Id("Description");
        private readonly By _cityInput = By.Id("City");
        private readonly By _addressInput = By.Id("Address");
        private readonly By _imageFileInput = By.Id("imageFile");
        private readonly By _dateInput = By.Name("date");
        private readonly By _timeInput = By.Name("time");
        private readonly By _saveButton = By.XPath("/html/body/div/main/div[1]/div/form/div[9]/input");
        private readonly By _nameError = By.Id("Name-error");
        private readonly By _descriptionError = By.Id("Description-error");
        private readonly By _cityError = By.Id("City-error");
        private readonly By _addressError = By.Id("Address-error");

        public EditEventPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public void EditEvent(string name, string description, string city, string address, string imageFilePath, 
            DateTime date, TimeSpan time)
        {
            _webDriver.Navigate().GoToUrl(_myEventsUrl);
            _webDriver.FindElement(_eventNameLink).Click();
            _webDriver.FindElement(_editEventLink).Click();

            _webDriver.FindElement(_nameInput).Clear();
            _webDriver.FindElement(_nameInput).SendKeys(name);
            _webDriver.FindElement(_descriptionInput).Clear();
            _webDriver.FindElement(_descriptionInput).SendKeys(description);

            _webDriver.FindElement(_cityInput).Clear();
            _webDriver.FindElement(_cityInput).SendKeys(city);
            _webDriver.FindElement(_addressInput).Clear();
            _webDriver.FindElement(_addressInput).SendKeys(address);

            if (string.IsNullOrWhiteSpace(imageFilePath) == false)
            {
                _webDriver.FindElement(_imageFileInput).SendKeys(imageFilePath);
            }

            _webDriver.FindElement(_dateInput).SendKeys(date.Date.ToString("yyyy-MM-dd"));
            _webDriver.FindElement(_timeInput).SendKeys(time.ToString(@"hh\:mm"));

            _webDriver.FindElement(_saveButton).Click();
        }

        public string GetUpdatedEventName(string name)
        {
            By updatedEventRecord = By.XPath($"//td[normalize-space() = '{name}']");

            string updatedEventName = "";

            try
            {
                IWebElement element = _webDriver.FindElement(updatedEventRecord);

                updatedEventName = element.Text;
            }
            catch (NoSuchElementException)
            {

            }

            return updatedEventName;
        }

        public List<string> GetErrorMessages()
        {
            string nameError = _webDriver.FindElement(_nameError).Text;
            string descriptionError = _webDriver.FindElement(_descriptionError).Text;
            string cityError = _webDriver.FindElement(_cityError).Text;
            string addressError = _webDriver.FindElement(_addressError).Text;

            List<string> errorMessages = new List<string>()
            {
                nameError,
                descriptionError,
                cityError,
                addressError
            };

            return errorMessages;
        }
    }
}
