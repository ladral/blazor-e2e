using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Blazor.E2E.Prototype.Test
{
    public class IndexPageE2ETests : IDisposable
    {
        private readonly IWebDriver driver;
        private readonly string appURL;

        public IndexPageE2ETests()
        {
            var options = new ChromeOptions();
            //options.AddArgument("--headless");
            driver = new ChromeDriver(options);
            appURL = Environment.GetEnvironmentVariable("TestUrl");
            if (string.IsNullOrEmpty(appURL)) appURL = "https://localhost:7241";
            Console.WriteLine($"appURL is: {appURL}");
        }

        [Fact]
        public void ShouldDisplayHelloWorld()
        {
            // act
            driver.Navigate().GoToUrl(appURL + "/");
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("h1")));

            //assert
            Assert.Equal("Hello, world!", driver.FindElements(By.CssSelector("h1"))[0].Text);
            driver.Quit();
        }

        public void Dispose()
        {
            driver.Dispose();
        }
    }
}