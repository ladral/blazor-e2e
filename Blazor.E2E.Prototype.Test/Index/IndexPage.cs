using OpenQA.Selenium;

namespace Blazor.E2E.Prototype.Test.Index;

public class IndexPage
{
    private readonly IWebDriver _driver;
    private readonly string _uri;
    
    public IndexPage(IWebDriver driver, string baseUri)
    {
        _driver = driver;
        _uri = baseUri + "/";
    }
    
    public static By HeadingSelector => By.CssSelector("h1");
    public string Heading => _driver.FindElement(HeadingSelector).Text;
    
    public void Navigate() => _driver.Navigate()
        .GoToUrl(_uri);
}