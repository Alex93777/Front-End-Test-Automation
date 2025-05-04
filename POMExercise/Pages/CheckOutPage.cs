using Newtonsoft.Json.Bson;
using OpenQA.Selenium;

namespace POMExercise.Pages
{
    public class CheckOutPage : BasePage
    {
        protected readonly By firstNameInput = By.XPath("//input[@id='first-name']");

        protected readonly By lastNameInput = By.XPath("//div[@class='form_group']//input[@id='last-name']");

        protected readonly By postalCodeInput = By.Id("postal-code");

        protected readonly By continueButton = By.XPath("//input[@id='continue']");

        protected readonly By finishButton = By.CssSelector("#finish");

        protected readonly By completeHeader = By.ClassName("complete-header");

        public CheckOutPage(IWebDriver driver) : base(driver)
        {

        }

        public void FillFirstName(string firstname)
        {
            Type(firstNameInput, firstname);
        }

        public void FillLastName(string lastName) 
        {  
            Type(lastNameInput, lastName); 
        }

        public void FillPostalCode(string postalCode)
        {
            Type(postalCodeInput, postalCode);
        }

        public void ClickContinueButton()
        {
            Click(continueButton);
        }

        public void ClickFinishButton()
        {
            Click(finishButton);
        }

        public bool IsPageLoaded()
        {
            return driver.Url.Contains("checkout-step-one.html") ||
                   driver.Url.Contains("checkout-step-two.html");
        }

        public bool IsCheckoutComplete()
        {
            return GetText(completeHeader) == "Thank you for your order!";
        }
    }
}
