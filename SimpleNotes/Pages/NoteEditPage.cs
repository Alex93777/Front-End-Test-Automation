using OpenQA.Selenium;

namespace SimpleNotes.Pages
{
    public class NoteEditPage : BasePage
    {
        public NoteEditPage(IWebDriver driver) : base(driver)
        {
            
        }

        public IWebElement TitleInput => driver.FindElement(By.XPath
           ("//input[@name='Title']"));

        public IWebElement DescriptionInput => driver.FindElement(By.XPath
           ("//textarea[@name='Description']"));

        public IWebElement SelectStatusInput => driver.FindElement(By.XPath
           ("//select[@class='form-select active']"));

        public IWebElement EditBtn => driver.FindElement(By.XPath
            ("//button[text()='Edit']"));
    }
}
