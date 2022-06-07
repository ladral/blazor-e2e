using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Blazor.E2E.Prototype.Test
{
    public class FetchDataPageE2ETests : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly string? _appUrl;

        public FetchDataPageE2ETests()
        {
            var options = new ChromeOptions();
            _driver = new ChromeDriver(options);
            _appUrl = Environment.GetEnvironmentVariable("TestUrl");
            if (string.IsNullOrEmpty(_appUrl)) _appUrl = "https://localhost:7241";
            Console.WriteLine($"appURL is: {_appUrl}");
        }

        [Fact]
        public void WeatherForecast_ShouldHaveExpectedColumnCount()
        {
            // arrange
            const string columnSelector = ".table th";
            const int expectedColumnCount = 4;

            // act
            _driver.Navigate().GoToUrl(_appUrl + "/fetchdata");
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector(".table")));

            var columnCount = _driver.FindElements(By.CssSelector(columnSelector)).Count;

            // assert
            Assert.Equal(expectedColumnCount, columnCount);

            _driver.Quit();
        }

        public void Dispose()
        {
            _driver.Dispose();
        }
    }
}