using IdeaCenter.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using System;


namespace IdeaCenter.Tests
{
    public class BaseTest
    {
        public IWebDriver driver;

        public LoginPage loginPage;

        public CreateIdeaPage createIdeaPage;

        public MyIdeasPage myIdeasPage;

        public IdeasReadPage ideasReadPage;

        public IdeasEditPage ideasEditPage;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            //var chromeOptions = new ChromeOptions();
            //set path to Brave browser
            //chromeOptions.BinaryLocation = @"C:\Program Files\BraveSoftware\Brave-Browser\Application\brave.exe";
            //chromeOptions.AddUserProfilePreference("profile.password_manager_enable", false);
            //chromeOptions.AddArgument("--disable-search-engine-choice-screen");

            //driver = new ChromeDriver(chromeOptions);
            //driver = new EdgeDriver();

            driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            loginPage = new LoginPage(driver);
            createIdeaPage = new CreateIdeaPage(driver);
            myIdeasPage = new MyIdeasPage(driver);
            ideasReadPage = new IdeasReadPage(driver);
            ideasEditPage = new IdeasEditPage(driver);

            loginPage.OpenPage();
            loginPage.Login("examPrep1_Demo@gmail.com", "123456");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        public string GenerateRandomString(int lenght)
        {
            const string chars = "kajsldabsdlhabkjasdbpvo";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, lenght).Select(s => s[random.Next(s.Length)]).ToArray()); 
        }
    }
}
