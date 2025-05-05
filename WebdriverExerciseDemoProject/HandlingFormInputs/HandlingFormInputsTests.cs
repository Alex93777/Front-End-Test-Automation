using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace HandlingFormInputs
{
    public class HandlingFormInputsTests
    {
        WebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://practice.bpbonline.com/");
        }

        [TearDown]

        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void HandlingFormInputs()
        {
            //Click in my account button
            var myAccountButton = driver.FindElements(By.XPath("//span[@class='ui-button-text']"))[2];
            myAccountButton.Click();

            //Click continue button
            driver.FindElement(By.LinkText("Continue")).Click();

            //Click male radio button
            driver.FindElement(By.XPath("//input[@type='radio'][@value='m']")).Click();

            //Fill value in first name field
            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='firstname']")).SendKeys("Alex");

            //Fill value in last name field
            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='lastname']")).SendKeys("Cvetkov");

            //Fill in date field
            driver.FindElement(By.Id("dob")).SendKeys("07/01/2024");

            //Build random email address
            Random random = new Random();
            int randomNumber = random.Next(1000, 9999);
            string email = "alex" + randomNumber.ToString() + "@gmail.com";

            //Fill email field
            driver.FindElement(By.Name("email_address")).SendKeys(email);

            //Fill company name
            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='company']")).SendKeys("Example company");

            //Fill street address field
            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='street_address']"))
            .SendKeys("Boulevard Vitusha 15");

            //Fill suburb field
            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='suburb']")).SendKeys("Sofia");

            //Fill postal code field
            driver.FindElement(By.Name("postcode")).SendKeys("1000");

            //Fill city field
            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='city']")).SendKeys("Sofia");

            //Fill state/province field
            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='state']")).SendKeys("Sofia");

            //Select from country dropdown
            new SelectElement(driver.FindElement(By.Name("country"))).SelectByText("Bulgaria");

            //Fill telephone field
            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='telephone']")).SendKeys("0879981981");

            //Click newsletter checkbox
            driver.FindElement(By.XPath("//input[@name='newsletter']")).Click();

            //Fill password field
            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='password']")).SendKeys("secret_user");

            //Fill confirm password field
            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='confirmation']")).SendKeys("secret_user");

            //Click continue button
            driver.FindElements(By.XPath("//span[@class='ui-button-icon-primary ui-icon ui-icon-person']" +
                "//following-sibling::span"))[1].Click();

            //Assert message for success
            Assert.AreEqual(driver.FindElement(By.XPath("//div[@id='bodyContent']//h1")).Text, "Your Account Has Been Created!");

            //Click log off button
            driver.FindElement(By.LinkText("Log Off")).Click();

            //Click continue button
            driver.FindElement(By.LinkText("Continue")).Click();

            Console.WriteLine("User Created Successfully!");

        }
    }
}