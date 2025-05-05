using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WebDriverWaitExercise
{
    public class ImplicitWait
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();

            driver.Navigate().GoToUrl("http://practice.bpbonline.com/");

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TearDown] 
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void SearchKeyboardTest()
        {
            //Fill in the search field text box
            driver.FindElement(By.Name("keywords")).SendKeys("keyboard");

            //Click on the search icon
            driver.FindElement(By.XPath("//input[@title=' Quick Find ']")).Click();

            try
            {
                //Click on Buy Now Link
                driver.FindElement(By.LinkText("Buy Now")).Click();

                //Verify text
                Assert.IsTrue(driver.PageSource.Contains("keyboard"), "The keyboard is not added to card");
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected Exception " + ex.Message);
            }
        }

        [Test]
        public void SearchJunkTest()
        {
            //Fill in the search field text box
            driver.FindElement(By.Name("keywords")).SendKeys("junk");

            //Click on the search icon
            driver.FindElement(By.XPath("//input[@title=' Quick Find ']")).Click();

            try
            {
                //Click on Buy Now Link
                driver.FindElement(By.LinkText("Buy Now")).Click();
            }
            catch (NoSuchElementException ex)
            {
                Assert.Pass("NoSuchElementException was thrown");
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected Exception" + ex.Message);
            }
        }
    }
}