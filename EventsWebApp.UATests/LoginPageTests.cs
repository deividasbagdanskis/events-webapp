using EventsWebApp.UATests.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.Collections.Generic;
using Xunit;

namespace EventsWebApp.UATests
{
    public class LoginPageTests
    {
        private readonly FirefoxOptions _options;

        public LoginPageTests()
        {
            _options = new FirefoxOptions
            {
                AcceptInsecureCertificates = true
            };
        }

        [Fact]
        public void LoginUser_ValidInputs_Success()
        {
            using IWebDriver webDriver = new FirefoxDriver(_options);

            LoginPage loginPage = new LoginPage(webDriver);

            string email = "v.pavardenis@gmail.com";
            string password = "password";

            loginPage.LoginUser(email, password);

            bool userWasLoggedIn = loginPage.CheckIfUserWasLoggedIn();

            Assert.True(userWasLoggedIn);
        }

        [Fact]
        public void LoginUser_ValidEmail_InvalidPassword_ReturnsErrorMessage()
        {
            using IWebDriver webDriver = new FirefoxDriver(_options);

            LoginPage loginPage = new LoginPage(webDriver);

            string email = "v.pavardenis@gmail.com";
            string password = "flwekfnewoing";

            loginPage.LoginUser(email, password);

            List<string> errors = loginPage.GetErrorMessages();
            string expectedErrorMessage = "Invalid login attempt.";

            Assert.Contains(expectedErrorMessage, errors);
        }

        [Fact]
        public void LoginUser_EmptyInputs_ReturnsErrorMessage()
        {
            using IWebDriver webDriver = new FirefoxDriver(_options);

            LoginPage loginPage = new LoginPage(webDriver);

            string email = "";
            string password = "";

            loginPage.LoginUser(email, password);

            List<string> errors = loginPage.GetErrorMessages();

            List<string> expectedErrorMessages = new List<string>()
            {
                "The Email field is required.",
                "The Password field is required."
            };

            Assert.Equal(expectedErrorMessages, errors);
        }

        [Fact]
        public void LoginUser_InvalidEmail_InvalidPassword_ReturnsErrorMessage()
        {
            using IWebDriver webDriver = new FirefoxDriver(_options);

            LoginPage loginPage = new LoginPage(webDriver);

            string email = "joe.doe@gmail.com";
            string password = "flwekfnewoing";

            loginPage.LoginUser(email, password);

            List<string> errors = loginPage.GetErrorMessages();
            string expectedErrorMessage = "Invalid login attempt.";

            Assert.Contains(expectedErrorMessage, errors);
        }
    }
}
