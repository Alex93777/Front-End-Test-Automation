using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace IdeaCenter.Pages
{
    public class MyIdeasPage : BasePage
    {
        public MyIdeasPage(IWebDriver driver) : base(driver)
        {

        }

        public string Url = BaseUrl + "/Ideas/MyIdeas";

        public void OpenPage()
        {
            driver.Navigate().GoToUrl(Url);
        }

        public ReadOnlyCollection<IWebElement> IdeasCards => driver.FindElements(By.XPath
            ("//div[@class='card mb-4 box-shadow']"));

        public IWebElement ViewButtonLastIdea => IdeasCards.Last().FindElement(By.XPath
            (".//a[contains(@href,'Ideas/Read')]"));

        public IWebElement EditButtonLastIdea => IdeasCards.Last().FindElement(By.XPath
            (".//a[contains(@href,'/Ideas/Edit')]"));

        public IWebElement DeleteButtonLastIdea => IdeasCards.Last().FindElement(By.XPath
            (".//a[contains(@href,'/Ideas/Delete')]"));

        public IWebElement DescriptionLastIdea => IdeasCards.Last().FindElement(By.XPath
            (".//p[@class='card-text']"));
    }
}
