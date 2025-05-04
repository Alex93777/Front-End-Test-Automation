using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Interactions;
using OpenQA.Selenium.Interactions;
using PointerInputDevice = OpenQA.Selenium.Interactions.PointerInputDevice;
using System.Drawing;

namespace SeekBar
{
    public class SeekBarTests
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
        public void SeekBarTest()
        {
            IWebElement viewsButton = _driver.FindElement(MobileBy.AccessibilityId("Views"));

            viewsButton.Click();

            ScrollToText("Seek Bar");

            AppiumElement seekBarButton = _driver.FindElement(MobileBy.AccessibilityId("Seek Bar"));

            seekBarButton.Click();

            MoveSeekbarWithInspectorCoordinates(641, 351 ,1232, 348);

            var resultElement = _driver.FindElement(By.Id("progress"));

            Assert.That(resultElement.Text, Is.EqualTo("100 from touch=true"));
        }

        public void MoveSeekbarWithInspectorCoordinates(int startX, int startY, int endX, int endY)
        {
            var finger = new PointerInputDevice(PointerKind.Touch);
            var start = new Point(startX, startY);
            var end = new Point(endX, endY);
            var swipe = new ActionSequence(finger);

            swipe.AddAction(finger.CreatePointerMove(CoordinateOrigin.Viewport, startX, startY, TimeSpan.Zero));

            swipe.AddAction(finger.CreatePointerDown(MouseButton.Left));

            swipe.AddAction(finger.CreatePointerMove(CoordinateOrigin.Viewport, endX, endY, TimeSpan.FromSeconds(1)));

            swipe.AddAction(finger.CreatePointerUp(MouseButton.Left));

            _driver.PerformActions(new List<ActionSequence> { swipe });

        }
        private void ScrollToText(string text)
        {
            _driver.FindElement(MobileBy.AndroidUIAutomator
            ("new UiScrollable(new UiSelector().scrollable(true)).scrollIntoView(new UiSelector().text(\"" + text + "\"))"));
        }
    }
}