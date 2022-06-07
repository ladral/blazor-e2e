using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Blazor.E2E.Prototype.Test
{
    public class CounterPageE2ETests : IDisposable
    {
        private readonly IWebDriver driver;
        private readonly string appURL;

        public CounterPageE2ETests()
        {
            var options = new ChromeOptions();
            //options.AddArgument("--headless");
            driver = new ChromeDriver(options);
            appURL = Environment.GetEnvironmentVariable("TestUrl");
            if (string.IsNullOrEmpty(appURL)) appURL = "https://localhost:7241";
            Console.WriteLine($"appURL is: {appURL}");
        }

        [Fact]
        public void ShouldIncrementCount()
        {
            driver.Navigate().GoToUrl(appURL + "/counter");
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector(".btn")));
            Assert.Equal("Current count: 0", driver.FindElements(By.CssSelector("p"))[0].Text);
            driver.FindElement(By.CssSelector(".btn")).Click();
            Assert.Equal("Current count: 1", driver.FindElements(By.CssSelector("p"))[0].Text);
            driver.Quit();
        }

        public void Dispose()
        {
            driver.Dispose();
        }
    }
}