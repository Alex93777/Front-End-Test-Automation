using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace WebDriverWaitExercise
{
    public class ExplicitWait
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
        public void Teardown()
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
                //disable implicit time out set it up to 0
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);

                //Setup explicit wait and click on Buy Now Link
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(e => e.FindElement(By.LinkText("Buy Now"))).Click();

                string productName = driver.FindElement(By.XPath("//form[@name='cart_quantity']//a//strong"))
                    .Text;

                Assert.IsTrue(productName == "Microsoft Internet Keyboard PS/2");

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

            //add implicit wait
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);

            try
            {
                //Setup explicit wait and click on Buy Now Link
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                var noProductMessage = wait.Until(e => e.FindElement(By.XPath("//div[@class='contentText']//p")));

                //Store the message in variable
                string productName = noProductMessage.Text;

                //Verify text
                Assert.IsTrue(productName == "There is no product that matches the search criteria.");
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected Exception " + ex.Message);
            }
        }
    }
}
