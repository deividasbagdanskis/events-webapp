using OpenQA.Selenium;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EventsWebApp.UATests.PageObjects
{
    class LoginPage
    {
        private readonly IWebDriver _webDriver;
        private readonly string _loginPageUrl = "https://localhost:44357/Identity/Account/Login";
        private readonly By _emailInput = By.Id("Input_Email");
        private readonly By _passwordInput = By.Id("Input_Password");
        private readonly By _loginButton = By.XPath("/html/body/div/main/div/div/section/form/div[5]/button");
        private readonly By _errorList = By.XPath("/html/body/div/main/div/div/section/form/div[1]/ul");
        private readonly By _eventsPageHeading = By.XPath("/html/body/div/main/h1");

        public LoginPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }
        
        public void LoginUser(string email, string password)
        {
            _webDriver.Navigate().GoToUrl(_loginPageUrl);

            _webDriver.FindElement(_emailInput).SendKeys(email);
            _webDriver.FindElement(_passwordInput).SendKeys(password);

            _webDriver.FindElement(_loginButton).Click();
        }

        public bool CheckIfUserWasLoggedIn()
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
    }
}
