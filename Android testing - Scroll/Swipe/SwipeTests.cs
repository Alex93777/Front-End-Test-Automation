using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Interactions;

namespace Swipe
{
    public class SwipeTests
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
        public void SwipeTest()
        {
            IWebElement viewsButton = _driver.FindElement(MobileBy.AccessibilityId("Views"));

            viewsButton.Click();

            IWebElement galleryButton = _driver.FindElement(MobileBy.AccessibilityId("Gallery"));

            galleryButton.Click();

            IWebElement photosButton = _driver.FindElement(MobileBy.AccessibilityId("1. Photos"));

            photosButton.Click();

            var firstImage = _driver.FindElements(MobileBy.ClassName("android.widget.ImageView"))[0];

            var actions = new Actions(_driver);
            var swipe = actions.ClickAndHold(firstImage)
                .MoveByOffset(-200, 0)
                .Release()
                .Build();
            swipe.Perform();

            var thirdImage = _driver.FindElements(MobileBy.ClassName("android.widget.ImageView"))[2];

            Assert.That(thirdImage, Is.Not.Null, "Third image is not visible");
        }
    }
}