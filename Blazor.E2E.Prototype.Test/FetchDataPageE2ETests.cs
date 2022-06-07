using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Blazor.E2E.Prototype.Test
{
    public class FetchDataPageE2ETests : IDisposable
    {
        private readonly IWebDriver driver;
        private readonly string appURL;

        public FetchDataPageE2ETests()
        {
            var options = new ChromeOptions();
            driver = new ChromeDriver(options);
            appURL = Environment.GetEnvironmentVariable("TestUrl");
            if (string.IsNullOrEmpty(appURL)) appURL = "https://localhost:7241";
            Console.WriteLine($"appURL is: {appURL}");
        }

        [Fact]
        public void WeatherForecast_ShouldHaveExpectedColumnCount()
        {
            // arrange
            const int expectedColumnCount = 4;
            const string columnSelector = ".table th";

            // act
            driver.Navigate().GoToUrl(appURL + "/fetchdata");
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector(".table")));

            var columnCount = driver.FindElements(By.CssSelector(columnSelector)).Count;

            // assert
            Assert.Equal(expectedColumnCount, columnCount);

            driver.Quit();
        }

        public void Dispose()
        {
            driver.Dispose();
        }
    }
}