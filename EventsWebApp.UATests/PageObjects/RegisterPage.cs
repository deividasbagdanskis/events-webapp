using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EventsWebApp.UATests.PageObjects
{
    class RegisterPage
    {
        private readonly IWebDriver _webDriver;
        private readonly string _registerPageUrl = "https://localhost:44357/Identity/Account/Register";
        private readonly By _emailInput = By.Id("Input_Email");
        private readonly By _passwordInput = By.Id("Input_Password");
        private readonly By _confirmPasswordInput = By.Id("Input_ConfirmPassword");
        private readonly By _registerButton = By.XPath("/html/body/div/main/div/div/form/button");
        private readonly By _errorList = By.XPath("/html/body/div/main/div/div/form/div[1]/ul");
        private readonly By _eventsPageHeading = By.XPath("/html/body/div/main/h1");
        private readonly By _differentPasswordsErrorMessage = By.Id("Input_ConfirmPassword-error");

        public RegisterPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            //_webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
        }

        public void RegisterUser(string email, string password, string passwordConfirmation)
        {
            _webDriver.Navigate().GoToUrl(_registerPageUrl);

            _webDriver.FindElement(_emailInput).SendKeys(email);
            _webDriver.FindElement(_passwordInput).SendKeys(password);
            _webDriver.FindElement(_confirmPasswordInput).SendKeys(passwordConfirmation);

            _webDriver.FindElement(_registerButton).Click();
        }

        public bool CheckIfUserWasRegistered()
        {
            try
            {
                _webDriver.FindElement(_eventsPageHeading);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public List<string> GetErrorMessages()
        {
            IWebElement errorListElement = _webDriver.FindElement(_errorList);

            ReadOnlyCollection<IWebElement> errorListElements = errorListElement.FindElements(By.XPath(".//*"));

            List<string> errorMessages = new List<string>();

            foreach (IWebElement error in errorListElements)
            {
                errorMessages.Add(error.Text);
            }

            return errorMessages;
        }

        public string GetDifferentPasswordErrorMessage()
        {
            return _webDriver.FindElement(_differentPasswordsErrorMessage).Text;
        }
    }
}
