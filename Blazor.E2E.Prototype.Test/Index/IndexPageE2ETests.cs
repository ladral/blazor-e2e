using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Blazor.E2E.Prototype.Test.Index
{
    public class IndexPageE2ETests : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly IndexPage _page;
        private readonly WebDriverWait _wait;

        public IndexPageE2ETests()
        {
            var baseUri = Environment.GetEnvironmentVariable("TEST_URL");
            var options = new ChromeOptions();
            
            if (string.IsNullOrEmpty(baseUri))
            {
                baseUri = "https://localhost:7241";
            }
            else
            {
                options.AddArgument("--headless");
            }
            
            _driver = new ChromeDriver(options);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            _page = new IndexPage(_driver, baseUri);
        }

        [Fact]
        public void NavigateToIndexPage_ShouldDisplayExpectedHeading()
        {
            // arrange
            const string expectedHeading = "Hello, world!";
            
            // act
            _page.Navigate();
            _wait.Until(ExpectedConditions.ElementExists(IndexPage.HeadingSelector));

            //assert
            Assert.Equal(expectedHeading, _page.Heading);
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}