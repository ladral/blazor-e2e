using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Blazor.E2E.Prototype.Test.Index
{
    public class IndexPageE2ETests : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly string? _baseUri;
        private readonly IndexPage _page;

        public IndexPageE2ETests()
        {
            _baseUri = Environment.GetEnvironmentVariable("TestUrl");
            var options = new ChromeOptions();
            
            if (string.IsNullOrEmpty(_baseUri))
            {
                _baseUri = "https://localhost:7241";
            }
            else
            {
                options.AddArgument("--headless");
            }
            
            _driver = new ChromeDriver(options);
            _page = new IndexPage(_driver, _baseUri);
            _page.Navigate();
        }

        [Fact]
        public void ShouldDisplayHelloWorld()
        {
            // arrange
            const string expectedTitle = "Hello, world!";
            
            // act
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