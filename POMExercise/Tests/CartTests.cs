namespace POMExercise.Tests
{
    public class CartTests : BaseTest
    {
        [SetUp]
        public void Setup()
        {
            Login("standard_user", "secret_sauce");
            inventoryPage.AddToCartByIndex(1);
            inventoryPage.ClickCartLink();
        }

        [Test]
        public void TestCartItemDisplayed()
        {
            Assert.True(cartPage.IsCarItemDisplayed(), "There were no products in the cart");
        }

        [Test]
        public void TestClickCheckout()
        {
            cartPage.ClickCheckoutButton();

            Assert.True(checkOutPage.IsPageLoaded(), "Not navigated to the checkout page");
        }
    }
}
