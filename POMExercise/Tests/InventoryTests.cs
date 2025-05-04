namespace POMExercise.Tests
{
    public class InventoryTests : BaseTest
    {
        [SetUp]
        public void Setup()
        {
            Login("standard_user", "secret_sauce");
        }

        [Test]
        public void TestInventoryDisplayed()
        {
            Assert.That(inventoryPage.IsInventoryPageHasItemsDisplayed(), Is.True,
                "Inventory page has not items displayed");
        }

        [Test]
        public void AddToCartByIndex()
        {
            inventoryPage.AddToCartByIndex(1);
            inventoryPage.ClickCartLink();
            Assert.That(cartPage.IsCarItemDisplayed(), Is.True,
                "Cart item was not added in the cart");
        }

        [Test]
        public void AddToCartByName()
        {
            inventoryPage.AddToCartByName("Sauce Labs Bike Light");
            inventoryPage.ClickCartLink();
            Assert.That(cartPage.IsCarItemDisplayed(), Is.True,
                "Cart item was not added in the cart");
        }

        [Test]
        public void TestPageTitle()
        {
            Assert.That(inventoryPage.IsInventoryPageLoaded(), Is.True,
                "Inventory page not loaded correctly");
        }
    }
}
