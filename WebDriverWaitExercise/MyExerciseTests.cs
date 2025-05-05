using Newtonsoft.Json.Bson;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Linq.Expressions;

namespace WebDriverWaitExercise
{
    public class MyExerciseTests
    {
        public IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/javascript_alerts");
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void checkjsAlert()
        {
            driver.FindElement(By.XPath("//button[@onclick='jsAlert()']")).Click();

            var alert = driver.SwitchTo().Alert();

            Assert.That(alert.Text, Is.EqualTo("I am a JS Alert"), "Alert is not as expected");

            alert.Accept();

            string result = driver.FindElement(By.Id("result")).Text;

            Assert.That(result, Is.EqualTo("You successfully clicked an alert"),
                "Alert message is not as expected");
        }

        [Test]
        public void checkjsConfirmAlert()
        {
            driver.FindElement(By.XPath("//button[@onclick='jsConfirm()']")).Click();

            var alert = driver.SwitchTo().Alert();

            Assert.That(alert.Text, Is.EqualTo("I am a JS Confirm"),
                "Alert message is not as expected");

            alert.Accept();

            string result = driver.FindElement(By.XPath("//p[@id='result']")).Text;

            Assert.That(result, Is.EqualTo("You clicked: Ok"),
                "Alert message is not as expected");
        }

        [Test]
        public void checkjsPromptAlert()
        {
            driver.FindElement(By.XPath("//button[@onclick='jsPrompt()']")).Click();

            var alert = driver.SwitchTo().Alert();

            string alertMessage = "test alert";
            alert.SendKeys(alertMessage);

            Assert.That(alert.Text, Is.EqualTo("I am a JS prompt"),
                "Alert message is not as expected");

            alert.Accept();

            var result = driver.FindElement(By.Id("result")).Text;

            Assert.That(result, Is.EqualTo("You entered: test alert"),
                "Alert message is not as expected");
        }

        [Test]
        public void searchProductKeyboard_ShoulAddToCard()
        {
            driver.Navigate().GoToUrl("http://practice.bpbonline.com/");

            var field = driver.FindElement(By.XPath("//input[@type='text']"));

            field.SendKeys("keyboard");
            field.SendKeys(Keys.Enter);

            try
            {
                driver.FindElement(By.LinkText("Buy Now")).Click();
                Assert.That(driver.PageSource.Contains("keyboard"),
                    "The product keyboard was not found in the cart page");
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected exception " + ex.Message);
            }
        }

        [Test]
        public void searchProductJunk_ShouldThrowNoSuchElementException()
        {
            driver.Navigate().GoToUrl("http://practice.bpbonline.com/");

            //write junk in input field
            var inputField =  driver.FindElement(By.XPath("//input[@type='text']"));
            inputField.SendKeys("junk");

            //click input field
            var searchElement = driver.FindElement(By.XPath("//input[@type='image']"));
            searchElement.Click();

            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                var buyNowButton = wait.Until(e => e.FindElement(By.LinkText("Buy Now")));
                buyNowButton.Click();

                Assert.Fail("The Buy Now button was found for non-existing product");
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Pass("Expected WebDriverTimeoutException was thrown");
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected expression: " + ex.Message);
            }
        }

        [Test]
        public void TestFrameByIndex()
        {
            driver.Navigate().GoToUrl("https://codepen.io/pervillalva/full/abPoNLd");

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            driver.SwitchTo().Frame("result");

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".dropbtn")));

            driver.FindElement(By.CssSelector(".dropbtn")).Click();

            driver.SwitchTo().DefaultContent();
        }
    }
}
