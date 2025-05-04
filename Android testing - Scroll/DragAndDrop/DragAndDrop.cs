using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Interactions;

namespace DragAndDrop
{
    public class Tests
    {
        private AndroidDriver _driver;

        private AppiumLocalService _appiumLocalService;

        [OneTimeSetUp]
        public void Setup()
        {
            _appiumLocalService = new AppiumServiceBuilder()
                .WithIPAddress("127.0.0.1")
                .UsingPort(4723)
                .Build();

            _appiumLocalService.Start();

            var androidOptions = new AppiumOptions()
            {
                PlatformName = "Android",
                AutomationName = "UIAutomator2",
                DeviceName = "Pixel 9 Pro",
                PlatformVersion = "16",
                App = @"D:\QA\Front-End Test Automation\Front-End Test Automation\apkForTesting\ApiDemos-debug.apk"
            };

            _driver = new AndroidDriver(_appiumLocalService, androidOptions);

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _driver?.Quit();
            _driver?.Dispose();
            _appiumLocalService?.Dispose();
        }

        [Test]
        public void DragAndDropTest()
        {
            IWebElement viewsButton = _driver.FindElement(MobileBy.AccessibilityId("Views"));

            viewsButton.Click();

            IWebElement dragAndDropButton = _driver.FindElement(MobileBy.AccessibilityId("Drag and Drop"));

            dragAndDropButton.Click();

            AppiumElement firstDot = _driver.FindElement(By.Id("drag_dot_1"));

            AppiumElement secondDot = _driver.FindElement(By.Id("drag_dot_2"));

            var scriptArgs = new Dictionary<string, object>
            {
                {"elementId", firstDot.Id},
                { "endX", secondDot.Location.X + secondDot.Size.Width/2},
                { "endY", secondDot.Location.Y + secondDot.Size.Height/2},
                { "speed", 2500}
            };

            _driver.ExecuteScript("mobile: dragGesture", scriptArgs);

            var dropMessage = _driver.FindElement(By.Id("drag_result_text"));

            Assert.That(dropMessage.Text, Is.EqualTo("Dropped!"), "The element was not dropped!");
        }
    }
}