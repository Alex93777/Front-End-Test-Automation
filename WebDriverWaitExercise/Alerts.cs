using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace WebDriverWaitExercise
{
    public class Alerts
    {
        public class ExplicitWait
        {
            private IWebDriver driver;

            [SetUp]
            public void Setup()
            {
                driver = new ChromeDriver();

                driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/javascript_alerts");

                //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            }

            [TearDown]
            public void Teardown()
            {
                driver.Quit();
                driver.Dispose();
            }

            [Test]
            public void HandleMultipleWindows()
            {
                driver.FindElement(By.XPath("//button[@onclick='jsConfirm()']")).Click();

                IAlert alert = driver.SwitchTo().Alert();

                Assert.That(alert.Text, Is.EqualTo("I am a JS Confirm"), "Alert did not open");

                alert.Dismiss();

                string result = driver.FindElement(By.Id("result")).Text;

                Assert.That(result, Is.EqualTo("You clicked: Cancel"),
                    "Result message is not as expected");
            }

            [Test]
            public void HandleJsPromptButtonAlert()
            {
                driver.FindElement(By.XPath("//button[@onclick='jsPrompt()']")).Click();

                IAlert alert = driver.SwitchTo().Alert();

                Assert.That(alert.Text, Is.EqualTo("I am a JS prompt"), "Alert did not open");

                string inputText = "populated";
                alert.SendKeys(inputText);

                alert.Accept();

                string result = driver.FindElement(By.Id("result")).Text;

                Assert.That(result, Is.EqualTo("You entered: populated"),
                    "Result message is not as expected");
            }

        }
    }
}
