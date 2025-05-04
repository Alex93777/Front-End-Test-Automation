using OpenQA.Selenium;

namespace POMExercise.Pages
{
    public class CartPage : BasePage
    {
        protected readonly By cartItem = By.CssSelector(".cart_item_label");

        protected readonly By checkOutButton = By.Id("checkout");
        public CartPage(IWebDriver driver) : base(driver)
        {
            
        }

        public bool IsCarItemDisplayed()
        {
            return FindElement(cartItem).Displayed;
        }

        public void ClickCheckoutButton()
        {
            Click(checkOutButton);
        }
    }
}
