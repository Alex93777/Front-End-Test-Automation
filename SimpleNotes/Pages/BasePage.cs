using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SimpleNotes.Pages
{
    public class BasePage
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;
        protected static readonly string BaseUrl =
            "https://d5wfqm7y6yb3q.cloudfront.net";

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public IWebElement JoinUsNowButton => driver.FindElement(By.XPath("//a[text()='Join us now!']"));

        public IWebElement TitleBasePag => driver.FindElement(By.XPath("//h1[text()='Simple Notes']"));

    }
}
