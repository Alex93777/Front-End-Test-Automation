using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using POMExercise.Pages;

namespace POMExercise.Tests
{
    public class BaseTest
    {
        protected IWebDriver driver;

        protected LoginPage loginPage;

        protected InventoryPage inventoryPage;

        protected CartPage cartPage;

        protected CheckOutPage checkOutPage;

        protected HiddenMenuPage hiddenMenuPage;

        [SetUp]
        public void Setup()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);

            driver = new ChromeDriver(chromeOptions);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            loginPage = new LoginPage(driver);
            inventoryPage = new InventoryPage(driver);
            cartPage = new CartPage(driver);
            checkOutPage = new CheckOutPage(driver);
            hiddenMenuPage = new HiddenMenuPage(driver);
        }

        [TearDown]
        public void Teardown()
        {
            if(driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
        }

        protected void Login(string userName, string password)
        {
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            var loginPage = new LoginPage(driver);
            loginPage.LoginUser(userName, password);
        }
    }
}
