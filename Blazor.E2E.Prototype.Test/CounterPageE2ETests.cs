using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Blazor.E2E.Prototype.Test
{
    public class CounterPageE2ETests : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly string? _appUrl;

        public CounterPageE2ETests()
        {
            var options = new ChromeOptions();
            //options.AddArgument("--headless");
            _driver = new ChromeDriver(options);
            _appUrl = Environment.GetEnvironmentVariable("TestUrl");
            if (string.IsNullOrEmpty(_appUrl)) _appUrl = "https://localhost:7241";
            Console.WriteLine($"appURL is: {_appUrl}");
        }

        [Fact]
        public void ShouldIncrementCount()
        {
            // arrange
            const int initialCount = 0;
            const int expectedCount = 1;
            
            _driver.Navigate().GoToUrl(_appUrl + "/counter");
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector(".btn")));
            
            // act
            Assert.Equal($"Current count: {initialCount}", _driver.FindElements(By.CssSelector("p"))[0].Text);
            _driver.FindElement(By.CssSelector(".btn")).Click();
            
            // assert
            Assert.Equal($"Current count: {expectedCount}", _driver.FindElements(By.CssSelector("p"))[0].Text);
            
            _driver.Quit();
        }

        public void Dispose()
        {
            _driver.Dispose();
        }
    }
}