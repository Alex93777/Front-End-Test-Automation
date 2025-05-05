using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WebDriverWaitExercise
{
    public class WebDriverWaitTest
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/windows");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void HandleMultipleWindows()
        {
            driver.FindElement(By.LinkText("Click Here")).Click();

            ReadOnlyCollection<string> windowsHandles = driver.WindowHandles;

            Assert.That(windowsHandles.Count, Is.EqualTo(2), "The number of equal tabs should be 2");

            driver.SwitchTo().Window(windowsHandles[1]);

            string newWindowContent = driver.FindElement(By.TagName("h3")).Text;

            Assert.That(newWindowContent, Is.EqualTo("New Window"), "Did not find new window content!");

            string path = Path.Combine(Directory.GetCurrentDirectory(), "content.txt");

            if (File.Exists(path))
            {
                File.Delete(path);
            }
            File.AppendAllText(path, "current handle " + driver.CurrentWindowHandle);
            File.AppendAllText(path, "page content: " + driver.PageSource);

            driver.Close();

            driver.SwitchTo().Window(windowsHandles[0]);

            string originalWindowContent = driver.FindElement(By.TagName("h3")).Text;
            Assert.That(originalWindowContent, Is.EqualTo("Opening a new window"),
                "Did not find new window content!");

            File.AppendAllText(path, "current handle " + driver.CurrentWindowHandle);
            File.AppendAllText(path, "page content: " + driver.PageSource);
        }

        [Test]
        public void HandleNoSuchWindow()
        {
            driver.FindElement(By.LinkText("Click Here")).Click();

            ReadOnlyCollection<string> windowsHandles = driver.WindowHandles;

            Assert.That(windowsHandles.Count, Is.EqualTo(2), "The number of equal tabs should be 2");

            driver.SwitchTo().Window(windowsHandles[1]);

            string newWindowContent = driver.FindElement(By.TagName("h3")).Text;
            Assert.That(newWindowContent, Is.EqualTo("New Window"), "Did not find new window content!");

            driver.Close();

            try
            {
                driver.SwitchTo().Window(windowsHandles[1]);
            }
            catch(NoSuchWindowException ex)
            {
                Assert.Pass("Expected error");
            }
            catch(Exception ex)
            {
                Assert.Fail("Unexpected error " + ex.Message);
            }
        }
    }
}
