using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;

namespace Test_appium_my_exercise
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
        public void Test1()
        {
            var firstInput = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
            firstInput.Clear();
            firstInput.SendKeys("5");

            var secondInput = driver.FindElement(MobileBy.XPath
                ("//android.widget.EditText[@resource-id=\"com.example.androidappsummator:id/editText2\"]"));
            secondInput.Clear();
            secondInput.SendKeys("6");

            var calculateButton = driver.FindElement(MobileBy.XPath
                ("//android.widget.Button[@resource-id=\"com.example.androidappsummator:id/buttonCalcSum\"]"));
            calculateButton.Click();

            var result = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum"));

            Assert.That(result.Text, Is.EqualTo("11"), "summation is incorrect");
        }
    }
}