using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V131.Network;

namespace POMExercise.Pages
{
    public class LoginPage : BasePage
    {
        private readonly By userNameField = By.XPath("//input[@id='user-name']");

        private readonly By passWordField = By.XPath("//input[@id='password']");

        private readonly By loginButton = By.XPath("//input[@id='login-button']");

        private readonly By errorMessage = By.XPath("//div[@class='error-message-container error']//h3");


        public LoginPage(IWebDriver driver) : base(driver)
        {

        }

        public void FillUserName(string username)
        {
            Type(userNameField, username);
        }

        public void FillPassword(string password)
        {
            Type(passWordField, password);
        }

        public void ClickLoginButton()
        {
            Click(loginButton);
        }

        public string GetErrorMessage()
        {
            return GetText(errorMessage);
        }

        public void LoginUser(string username, string password)
        {
            FillUserName(username);
            FillPassword(password);
            ClickLoginButton();
            
            //check if no error message is visible
        }
    }
}
