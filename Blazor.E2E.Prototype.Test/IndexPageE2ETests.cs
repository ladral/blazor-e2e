using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Blazor.E2E.Prototype.Test
{
    public class IndexPageE2ETests : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly string? _appUrl;

        public IndexPageE2ETests()
        {
            var options = new ChromeOptions();
            //options.AddArgument("--headless");
            _driver = new ChromeDriver(options);
            _appUrl = Environment.GetEnvironmentVariable("TestUrl");
            if (string.IsNullOrEmpty(_appUrl)) _appUrl = "https://localhost:7241";
            Console.WriteLine($"appURL is: {_appUrl}");
        }

        [Fact]
        public void ShouldDisplayHelloWorld()
        {
            // arrange
            const string expectedTitle = "Hello, world!";
            
            // act
            _driver.Navigate().GoToUrl(_appUrl + "/");
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("h1")));

            //assert
            Assert.Equal(expectedTitle, _driver.FindElements(By.CssSelector("h1"))[0].Text);
            _driver.Quit();
        }

        public void Dispose()
        {
            _driver.Dispose();
        }
    }
}