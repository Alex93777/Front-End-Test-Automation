using OpenQA.Selenium;
using System.Net.Security;

namespace POMExercise.Pages
{
    public class InventoryPage : BasePage
    {
        protected readonly By cartLink = By.CssSelector(".shopping_cart_link");

        protected readonly By productsPageTitle = By.CssSelector(".title");

        protected readonly By productItems = By.CssSelector(".inventory_item");
        public InventoryPage(IWebDriver driver) : base(driver)
        {
            
        }

        public void AddToCartByIndex(int itemIndex)
        {
            var itemByIndexButton = By.XPath
                ($"//div[@class='inventory_list']//div[@class='inventory_item'][{itemIndex}]//button");

            Click(itemByIndexButton);
        }

        public void AddToCartByName(string name)
        {
            var itemByNameButton = By.XPath
                ($"//div[text()='{name}']/ancestor::div[@class='inventory_item_description']//button");

            Click(itemByNameButton);
        }

        public void ClickCartLink()
        {
            Click(cartLink);
        }

        public bool IsInventoryPageHasItemsDisplayed()
        {
            return FindElements(productItems).Any();
        }

        public bool IsInventoryPageLoaded()
        {
            return GetText(productsPageTitle) == "Products" &&
                driver.Url.Contains("inventory.html");
        } 
    }
}
