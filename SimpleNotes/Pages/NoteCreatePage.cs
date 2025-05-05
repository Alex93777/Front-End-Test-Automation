using OpenQA.Selenium;
using System.Net.Mail;

namespace SimpleNotes.Pages
{
    public class NoteCreatePage : BasePage
    {
        public NoteCreatePage(IWebDriver driver) : base(driver)
        {

        }

        public string Url = BaseUrl + "/Note/Create";

        public IWebElement TitleInput => driver.FindElement(By.XPath
            ("//input[@name='Title']"));

        public IWebElement DescriptionInput => driver.FindElement(By.XPath
            ("//textarea[@name='Description']"));

        public IWebElement StartDateInput => driver.FindElement(By.XPath
            ("//input[@name='StartDate']"));

        public IWebElement EndDateInput => driver.FindElement(By.XPath
            ("//input[@name='EndDate']"));

        public IWebElement SelectStatusInput => driver.FindElement(By.XPath
            ("//select[@name='Status']"));

        public IWebElement CreateBtn => driver.FindElement(By.XPath
            ("//button[text()='Create']"));

        public IWebElement CancelBtn => driver.FindElement(By.XPath
            ("//a[text()='Cancel']"));

        public IWebElement ErrorMessage => driver.FindElement(By.XPath
            ("//div[@class='toast-message']"));

        public IWebElement SuccessMessage => driver.FindElement(By.XPath
            ("//div[@class='toast-message']"));

        public IWebElement DeleteNoteMessage => driver.FindElement(By.XPath
            ("//div[@class='toast-message']"));


        public void CreateNote(string title, string description, string startDate,
            string endDate, string selectStatus)
        {
            TitleInput.SendKeys(title);
            DescriptionInput.SendKeys(description);
            StartDateInput.SendKeys(startDate);
            EndDateInput.SendKeys(endDate);
            SelectStatusInput.SendKeys(selectStatus);
            CreateBtn.Click();
        }

        public void AssertErrorMessages()
        {
            Assert.True(ErrorMessage.Text.Equals("The Title field is required. The Description field is required."),
                "Error message is not as expected");  
        }

        public void SuccessMessages()
        {
            Assert.True(SuccessMessage.Text.Equals("Note Created Successfully!"),
                "Success message is not as expected");
        }


        public void OpenPage()
        {
            driver.Navigate().GoToUrl(Url);
        }
    }
}
