using OpenQA.Selenium;

namespace IdeaCenter.Pages
{
    public class IdeasEditPage : BasePage
    {
        public IdeasEditPage(IWebDriver driver) :base(driver)
        {
            
        }
        public IWebElement TitleInput => driver.FindElement(By.XPath
           ("//input[@name='Title']"));

        public IWebElement ImageInput => driver.FindElement(By.XPath
            ("//input[@name='Url']"));

        public IWebElement DescriptionInput => driver.FindElement(By.XPath
            ("//textarea[@name='Description']"));

        public IWebElement EditBtn => driver.FindElement(By.XPath("//button[@class='btn btn-primary btn-lg']"));
    }
}
