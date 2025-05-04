using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;

namespace AppiumTesting
{
    public class Tests
    {
        private AndroidDriver driver;
        private AppiumLocalService service;

        [SetUp]
        public void Setup()
        {
            service = new AppiumServiceBuilder()
                .WithIPAddress("127.0.0.1")
                .UsingPort(4723)
                .Build();

            AppiumOptions options = new AppiumOptions();
            options.App = @"C:\Users\PC\Downloads\com.example.androidappsummator.apk";
            options.PlatformName = "Android";
            options.DeviceName = "Pixel 9 Pro API 16";
            options.AutomationName = "UIAutomator2";

            driver = new AndroidDriver(service, options);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Dispose();
            service.Dispose();
        }

        [Test]
        public void TestValidSummation()
        {
            var firstInput = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
            firstInput.Clear();
            firstInput.SendKeys("2");

            var secondInput = driver.FindElement(MobileBy.XPath
                ("//android.widget.EditText[@resource-id=\"com.example.androidappsummator:id/editText2\"]"));
            secondInput.Clear();
            secondInput.SendKeys("7");

            var calcButton = driver.FindElement(MobileBy.ClassName("android.widget.Button"));
            calcButton.Click();

            var result = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum"));

            Assert.That(result.Text, Is.EqualTo("9"), "summation is incorrect");
        }

        [Test]
        public void TestInvalidSummation()
        {
            var firstInput = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
            firstInput.Clear();
            firstInput.SendKeys("2");

            var secondInput = driver.FindElement(MobileBy.XPath
                ("//android.widget.EditText[@resource-id=\"com.example.androidappsummator:id/editText2\"]"));
            secondInput.Clear();
            secondInput.SendKeys(".");

            var calcButton = driver.FindElement(MobileBy.ClassName("android.widget.Button"));
            calcButton.Click();

            var result = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum"));

            Assert.That(result.Text, Is.EqualTo("error"), "summation is incorrect");
        }
    }
}