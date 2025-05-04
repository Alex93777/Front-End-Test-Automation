using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium;

namespace AppiumDemoProject
{
    public class SummatorAppPOMTests
    {
        private AndroidDriver _driver;

        private AppiumLocalService _appiumLocalService;

        private SummatorPage _summatorPage;

        [OneTimeSetUp]
        public void Setup()
        {
            _appiumLocalService = new AppiumServiceBuilder()
                .WithIPAddress("127.0.0.1")
                .UsingPort(4723)
                .Build();
            _appiumLocalService.Start();

            var androidOptions = new AppiumOptions
            {
                PlatformName = "Android",
                AutomationName = "UIAutomator2",
                DeviceName = "Pixel 9 Pro",
                App = @"C:\\Users\\PC\\Downloads\\com.example.androidappsummator.apk",
                PlatformVersion = "16"
            };

            _driver = new AndroidDriver(_appiumLocalService, androidOptions);

            _summatorPage = new SummatorPage(_driver);
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            _driver?.Quit();
            _driver?.Dispose();
            _appiumLocalService?.Dispose();
        }

        [Test]
        public void TestWithValidData()
        {
            var result = _summatorPage.Calculate("1", "2");

            Assert.That(result, Is.EqualTo("3"));
        }

        [Test]
        public void TestWithValidData_ClickOnlyCalcBtn()
        {
            _summatorPage.ClearFields();

            _summatorPage.CalcButton.Click();

            Assert.That(_summatorPage.ResultField.Text, Is.EqualTo("error"));
        }

        [Test]
        public void TestWithValidData_FillOnlyFirstField()
        {
            _summatorPage.ClearFields();
            _summatorPage.Field1.SendKeys("1");

            _summatorPage.CalcButton.Click();

            Assert.That(_summatorPage.ResultField.Text, Is.EqualTo("error"));
        }

        [Test]
        public void TestWithValidData_FillOnlySecondField()
        {
            _summatorPage.ClearFields();
            _summatorPage.Field2.SendKeys("2");

            _summatorPage.CalcButton.Click();

            Assert.That(_summatorPage.ResultField.Text, Is.EqualTo("error"));
        }

        [Test]
        [TestCase("10", "10", "20")]
        [TestCase("100", "100", "200")]
        [TestCase("1000", "1000", "2000")]
        [TestCase("0", "1000", "1000")]
        [TestCase("2.5", "2.5", "5.0")]
        [TestCase("10.9", "10.1", "21.0")]
        public void TestWithValidData_Parametrized(string input1, string input2, string expectedResult)
        {
            var result = _summatorPage.Calculate(input1, input2);

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}

