using EventsWebApp.UATests.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.Collections.Generic;
using Xunit;

namespace EventsWebApp.UATests
{
    public class RegisterPageTests
    {
        private readonly FirefoxOptions _options;

        public RegisterPageTests()
        {
            _options = new FirefoxOptions
            {
                AcceptInsecureCertificates = true
            };
        }

        [Fact]
        public void RegisterUser_ValidInputs_Success()
        {
            using IWebDriver webDriver = new FirefoxDriver(_options);

            RegisterPage registerPage = new RegisterPage(webDriver);

            string email = "lorem.ipsum@gmail.com";
            string password = "password";
            string passwordConfirmation = "password";

            registerPage.RegisterUser(email, password, passwordConfirmation);

            bool userWasRegistered = registerPage.CheckIfUserWasRegistered();

            Assert.True(userWasRegistered);
        }

        [Fact]
        public void RegisterUser_ValidEmail_Different_Passwords_ReturnsErrorMessage()
        {
            using IWebDriver webDriver = new FirefoxDriver(_options);

            RegisterPage registerPage = new RegisterPage(webDriver);

            string email = "joe.doe@gmail.com";
            string password = "password";
            string passwordConfirmation = "password123";

            registerPage.RegisterUser(email, password, passwordConfirmation);

            string expectedErrorMessage = "The password and confirmation password do not match.";
            string actualErrorMessage = registerPage.GetDifferentPasswordErrorMessage();

            Assert.Contains(expectedErrorMessage, actualErrorMessage);
        }

        [Fact]
        public void RegisterUser_Taken_Email_Valid_Passwords_ReturnsErrorMessage()
        {
            using IWebDriver webDriver = new FirefoxDriver(_options);

            RegisterPage registerPage = new RegisterPage(webDriver);

            string email = "v.pavardenis@gmail.com";
            string password = "password";
            string passwordConfirmation = "password";

            registerPage.RegisterUser(email, password, passwordConfirmation);

            List<string> errors = registerPage.GetErrorMessages();

            string expectedErrorMessage = $"Email '{email}' is already taken.";

            Assert.Contains(expectedErrorMessage, errors);
        }

        [Fact]
        public void RegisterUser_Empty_Inputs_ReturnsErrorMessages()
        {
            using IWebDriver webDriver = new FirefoxDriver(_options);

            RegisterPage registerPage = new RegisterPage(webDriver);

            string email = "";
            string password = "";
            string passwordConfirmation = "";

            registerPage.RegisterUser(email, password, passwordConfirmation);

            List<string> errors = registerPage.GetErrorMessages();

            List<string> expectedErrorMessages = new List<string>()
            {
                "The Email field is required.",
                "The Password field is required."
            };

            Assert.Equal(expectedErrorMessages, errors);
        }
    }
}
