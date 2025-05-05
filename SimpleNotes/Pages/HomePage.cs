using OpenQA.Selenium;

namespace SimpleNotes.Pages
{
    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver) : base(driver) 
        {
            
        }

        public string Url = BaseUrl + "/Home/Main";

        public IWebElement LogoutButton => driver.FindElement(By.XPath("//span[text()='Logout']"));

        public void OpenPage()
        {
            driver.Navigate().GoToUrl(Url);
        }
    }
}
