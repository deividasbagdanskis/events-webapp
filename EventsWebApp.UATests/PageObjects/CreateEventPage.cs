using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace EventsWebApp.UATests.PageObjects
{
    public class CreateEventPage
    {
        private readonly IWebDriver _webDriver;
        private readonly string _createPageUrl = "https://localhost:44357/Events/Create";
        private readonly By _nameInput = By.Id("Name");
        private readonly By _descriptionInput = By.Id("Description");
        private readonly By _categoryDropdown = By.Id("CategoryId");
        private readonly By _cityInput = By.Id("City");
        private readonly By _addressInput = By.Id("Address");
        private readonly By _imageFileInput = By.Id("imageFile");
        private readonly By _dateInput = By.Name("date");
        private readonly By _timeInput = By.Name("time");
        private readonly By _createButton = By.XPath("/html/body/div/main/div[1]/div/form/div[9]/input");
        private readonly By _nameError = By.Id("Name-error");
        private readonly By _descriptionError = By.Id("Description-error");
        private readonly By _cityError = By.Id("City-error");
        private readonly By _addressError = By.Id("Address-error");

        public CreateEventPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public void CreateEvent(string name, string description, string category, string city, string address, 
            string imageFilePath, DateTime date, TimeSpan time)
        {
            _webDriver.Navigate().GoToUrl(_createPageUrl);

            _webDriver.FindElement(_nameInput).SendKeys(name);
            _webDriver.FindElement(_descriptionInput).SendKeys(description);

            IWebElement categoryDropdown = _webDriver.FindElement(_categoryDropdown);

            SelectElement categoryOptions = new SelectElement(categoryDropdown);
            categoryOptions.SelectByText(category);

            _webDriver.FindElement(_cityInput).SendKeys(city);
            _webDriver.FindElement(_addressInput).SendKeys(address);
            
            if (string.IsNullOrWhiteSpace(imageFilePath) == false)
            {
               _webDriver.FindElement(_imageFileInput).SendKeys(imageFilePath);
            }

            _webDriver.FindElement(_dateInput).SendKeys(date.Date.ToString("yyyy-MM-dd"));
            _webDriver.FindElement(_timeInput).SendKeys(time.ToString(@"hh\:mm"));

            _webDriver.FindElement(_createButton).Click();
        }

        public string GetCreatedEventName(string name)
        {
            By createdEventRecord = By.XPath($"//td[normalize-space() = '{name}']");

            string createdEventName = "";

            try
            {
                IWebElement element = _webDriver.FindElement(createdEventRecord);

                createdEventName = element.Text;
            }
            catch (NoSuchElementException)
            {

            }

            return createdEventName;
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
