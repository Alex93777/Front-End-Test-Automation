using OpenQA.Selenium;

namespace IdeaCenter.Pages
{
    public class CreateIdeaPage : BasePage
    {
        public CreateIdeaPage(IWebDriver driver) : base(driver)
        {
            
        }

        public string Url = BaseUrl + "/Ideas/Create";

        public IWebElement TitleInput => driver.FindElement(By.XPath
            ("//input[@name='Title']"));

        public IWebElement ImageInput => driver.FindElement(By.XPath
            ("//input[@name='Url']"));

        public IWebElement DescriptionInput => driver.FindElement(By.XPath
            ("//textarea[@name='Description']"));

        public IWebElement CreateBtn => driver.FindElement(By.XPath("//button[text()='Create']"));

        public IWebElement MainMessage => driver.FindElement(By.XPath
            ("//div[@class='text-danger validation-summary-errors']//li"));

        public IWebElement TitleErrorMessage => driver.FindElements(By.XPath
            ("//span[@class='text-danger field-validation-error']"))[0];

        public IWebElement DescriptionErrorMessage => driver.FindElements(By.XPath
            ("//span[@class='text-danger field-validation-error']"))[1];

        public void CreateIdea(string title, string imageUrl, string description)
        {
            TitleInput.SendKeys(title);
            ImageInput.SendKeys(imageUrl);
            DescriptionInput.SendKeys(description);
            CreateBtn.Click();
        }

        public void AssertErrorMessages()
        {
            Assert.True(MainMessage.Text.Equals("Unable to create new Idea!"),
                "Main message is not as expected");

            Assert.True(TitleErrorMessage.Text.Equals("The Title field is required."),
                "Title error message is not as expected");

            Assert.True(DescriptionErrorMessage.Text.Equals("The Description field is required."),
                "Description error message is not as expected");
        }

        public void OpenPage()
        {
            driver.Navigate().GoToUrl(Url);
        }
    }
}
