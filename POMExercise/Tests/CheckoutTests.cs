namespace POMExercise.Tests
{
    public class CheckoutTests : BaseTest
    {
        [SetUp]
        public void Setup()
        {
            Login("standard_user", "secret_sauce");
            inventoryPage.AddToCartByIndex(1);
            inventoryPage.ClickCartLink();
            cartPage.ClickCheckoutButton();
        }

        [Test]
        public void TestCheckoutPageLoaded()
        {
            Assert.True(checkOutPage.IsPageLoaded(), "Checkout page not loaded");
        }

        [Test]
        public void TestContinueToNextStep()
        {
            checkOutPage.FillFirstName("Alex");
            checkOutPage.FillLastName("Tsvetkov");
            checkOutPage.FillPostalCode("5800");
            checkOutPage.ClickContinueButton();

            Assert.That(driver.Url.Contains("checkout-step-two.html"), Is.True,
                "Not navigated to the correct checkout page");
        }

        [Test]
        public void TestCompleteOrder()
        {
            checkOutPage.FillFirstName("Alex");
            checkOutPage.FillLastName("Tsvetkov");
            checkOutPage.FillPostalCode("5800");
            checkOutPage.ClickContinueButton();
            checkOutPage.ClickFinishButton();

            Assert.True(checkOutPage.IsCheckoutComplete(), "Checkout was not completed");
        }
    }
}
