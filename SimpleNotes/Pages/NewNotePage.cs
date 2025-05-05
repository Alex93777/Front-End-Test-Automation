using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace SimpleNotes.Pages
{
    public class NewNotePage : BasePage
    {
        public NewNotePage(IWebDriver driver) : base(driver)
        {

        }

        public string Url = BaseUrl + "/Note/New";

        public ReadOnlyCollection<IWebElement> NotesCards => driver.FindElements(By.XPath
            ("//section[@class='p-4 d-flex justify-content-center text-center w-100']"));

        public IWebElement NoteBtn => driver.FindElement(By.XPath
           ("//span[text()='Notes']"));

        public IWebElement CreateNoteBtn => driver.FindElement(By.XPath
           ("//a[text()='Create Note']"));

        public IWebElement LastCreatedContainer => NotesCards.Last().FindElement(By.XPath
            (".//section[@class='p-4 d-flex justify-content-center text-center w-100']"));

        public IWebElement EditButtonLastNode => NotesCards.Last().FindElement(By.XPath
            (".//a[text()='Edit']"));

        public IWebElement TitleLastCreatedNode => NotesCards.Last().FindElement(By.XPath
            (".//h5[@class='card-title']"));

        public IWebElement SetToDoneBtn => NotesCards.Last().FindElement(By.XPath
            (".//a[@class='btn btn-primary']"));

        public IWebElement DeleteButtonLastNode => NotesCards.Last().FindElement(By.XPath
            (".//a[text()='Delete']"));

        public void OpenPage()
        {
            driver.Navigate().GoToUrl(Url);
        }
    }
}
