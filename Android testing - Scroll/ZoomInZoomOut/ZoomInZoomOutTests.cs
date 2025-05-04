using Newtonsoft.Json.Bson;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Interactions;
using System.Drawing;

namespace ZoomInZoomOut

{
    public class ZoomInZoomOutTests
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
        public void ZoomInZoomOutTest()
        {
            IWebElement viewsButton = _driver.FindElement(MobileBy.AccessibilityId("Views"));

            viewsButton.Click();

            ScrollToText("WebView");

            IWebElement webViewButton = _driver.FindElement(MobileBy.AccessibilityId("WebView"));

            webViewButton.Click();

            PerformZoomIn(339, 1169, 128, 690, 701, 1896, 1021, 2717);

            PerformZoomIn(114, 978, 405, 1688, 690, 2150, 986, 2660);
        }

        private void ScrollToText(string text)
        {
            _driver.FindElement(MobileBy.AndroidUIAutomator
            ("new UiScrollable(new UiSelector().scrollable(true)).scrollIntoView(new UiSelector().text(\"" + text + "\"))"));
        }


        public void PerformZoomIn(int ffStartX, int ffStartY, int ffEndX, int ffEndY, int sfStartX, int sfStartY, int sfEndX, int sfEndY)
        {
            var finger = new PointerInputDevice(PointerKind.Touch);
            var finger2 = new PointerInputDevice(PointerKind.Touch);
            var zoomFinger1 = new ActionSequence(finger);
            var zoomFinger2 = new ActionSequence(finger2);

            zoomFinger1.AddAction(finger.CreatePointerMove(CoordinateOrigin.Viewport, ffStartX, ffStartY, TimeSpan.Zero));

            zoomFinger1.AddAction(finger.CreatePointerDown(MouseButton.Left));

            zoomFinger1.AddAction(finger.CreatePointerMove(CoordinateOrigin.Viewport, ffEndX, ffEndY, TimeSpan.FromSeconds(1)));

            zoomFinger1.AddAction(finger.CreatePointerUp(MouseButton.Left));

            _driver.PerformActions(new List<ActionSequence> { zoomFinger1 });

            //Actions for finger 2

            zoomFinger2.AddAction(finger2.CreatePointerMove(CoordinateOrigin.Viewport, sfStartX, sfStartY, TimeSpan.Zero));

            zoomFinger2.AddAction(finger2.CreatePointerDown(MouseButton.Left));

            zoomFinger2.AddAction(finger2.CreatePointerMove(CoordinateOrigin.Viewport, sfEndX, sfEndY, TimeSpan.FromSeconds(1)));

            zoomFinger2.AddAction(finger2.CreatePointerUp(MouseButton.Left));

            _driver.PerformActions(new List<ActionSequence> { zoomFinger1, zoomFinger2 });
        }
    }
}