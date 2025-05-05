using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Chrome;

namespace WebDriverWaitExercise
{
    public class IFrames
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();

            driver.Navigate().GoToUrl("https://codepen.io/pervillalva/full/abPoNLd");
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void IFramesDropdownTesting()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            driver.SwitchTo().Frame("result"); 

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".dropbtn")));

            driver.FindElement(By.CssSelector(".dropbtn")).Click();

            driver.SwitchTo().DefaultContent();
        }

        [Test]
        public void IFramesDropdownTesting2()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.Id("result")));

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".dropbtn")));

            driver.SwitchTo().DefaultContent();
        }

        [Test]
        public void IFramesDropdownTesting3()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            var frame = wait.Until(ExpectedConditions.ElementIsVisible(By.TagName("iframe")));

            driver.SwitchTo().Frame(frame);

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".dropbtn")));

            driver.SwitchTo().DefaultContent();
        }
    }
}
