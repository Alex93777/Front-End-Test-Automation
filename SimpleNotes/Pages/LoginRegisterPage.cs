using OpenQA.Selenium;

namespace SimpleNotes.Pages
{
    public class LoginRegisterPage : BasePage
    {
        public LoginRegisterPage(IWebDriver driver) : base(driver)
        {
            
        }

        public string Url = BaseUrl + "/User/LoginRegister";

        public IWebElement LoginButton => driver.FindElement(By.XPath("//a[text()='Login']"));

        public IWebElement RegisterButton => driver.FindElement(By.XPath("//a[text()='Register' and @id='tab-register']"));

        public IWebElement EmailInput => driver.FindElement(By.XPath("//input[@name='LoginModel.Email']"));

        public IWebElement PasswordInput => driver.FindElement(By.XPath("//input[@name='LoginModel.Password']"));

        public IWebElement SigninBtn => driver.FindElement
            (By.XPath("//button[text()='Sign in' and @class='btn btn-primary btn-block mb-4']"));

        public void Login(string email, string password)
        {
            //EmailInput.SendKeys(email);
            //PasswordInput.SendKeys(password);
            //SigninBtn.Click();

            // Click the Login tab if needed
            if (!EmailInput.Displayed)
            {
                LoginButton.Click(); // Ensure the login tab is visible
                wait.Until(driver => EmailInput.Displayed && EmailInput.Enabled);
            }

            // Enter credentials
            EmailInput.Clear();
            EmailInput.SendKeys(email);

            PasswordInput.Clear();
            PasswordInput.SendKeys(password);

            SigninBtn.Click();
        }

        public void OpenPage()
        {
            driver.Navigate().GoToUrl(Url);
        }
    }
}
