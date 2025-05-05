using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using SimpleNotes.Pages;

namespace SimpleNotes.Tests
{
    public class BaseTest
    {
        public IWebDriver driver;

        public BasePage basePage;

        public LoginRegisterPage loginRegisterPage;

        public NewNotePage newNotePage;

        public NoteCreatePage noteCreatePage;

        public NoteEditPage noteEditPage;

        public DeleteNotePage deleteNotePage;

        public HomePage homePage;

        public AccessDeniedPage accessDeniedPage;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            basePage = new BasePage(driver);
            loginRegisterPage = new LoginRegisterPage(driver);
            newNotePage = new NewNotePage(driver);
            noteCreatePage = new NoteCreatePage(driver);
            noteEditPage = new NoteEditPage(driver);
            deleteNotePage = new DeleteNotePage(driver);
            homePage = new HomePage(driver);
            accessDeniedPage = new AccessDeniedPage(driver);

            loginRegisterPage.OpenPage();
            loginRegisterPage.Login("alex@gmail.com", "123456");
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
