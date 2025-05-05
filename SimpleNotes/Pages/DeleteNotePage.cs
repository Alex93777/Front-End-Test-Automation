using OpenQA.Selenium;

namespace SimpleNotes.Pages
{
    public class DeleteNotePage : BasePage
    {
        public DeleteNotePage(IWebDriver driver) : base(driver)
        {

        }

        public IWebElement YesBtn => driver.FindElement(By.XPath
          ("//button[text()='Yes']"));

        public IWebElement NoBtn => driver.FindElement(By.XPath
          ("//a[text()='No']"));

       
    }
}
