using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;

namespace ColorNoteAppTesting
{
    public class ColorNoteAppTest
    {
        private AndroidDriver _driver;

        private AppiumLocalService _appiumLocalService;   //за да можеш да си вдигнеш сървъра


        [OneTimeSetUp]
        public void Setup()
        {
            _appiumLocalService = new AppiumServiceBuilder()
                .WithIPAddress("127.0.0.1")
                .UsingPort(4723)
                .Build();

            _appiumLocalService.Start();

            var androidOptions = new AppiumOptions()
            {
                PlatformName = "Android",
                AutomationName = "UIAutomator2",
                DeviceName = "Pixel 9 Pro",
                App = @"D:\QA\Front-End Test Automation\Front-End Test Automation\apkForTesting\Notepad.apk",
                PlatformVersion = "16"
            };
            androidOptions.AddAdditionalAppiumOption("autoGrantPermissions", true);

            _driver = new AndroidDriver(_appiumLocalService, androidOptions);

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            try
            {
                var skipTutorialButton = _driver.FindElement(MobileBy.Id
                    ("com.socialnmobile.dictapps.notepad.color.note:id/btn_start_skip"));

                skipTutorialButton.Click();
            }
            catch (NoSuchElementException)
            {

            };

        }

        [OneTimeTearDown]
        public void Teardown()
        {
            _driver?.Quit();
            _driver?.Dispose();
            _appiumLocalService?.Dispose();
        }

        [Test, Order(1)]
        public void TestCreateNewNote()
        {
            IWebElement newNoteButton = _driver.FindElement(MobileBy.Id
                ("com.socialnmobile.dictapps.notepad.color.note:id/main_btn1"));
            newNoteButton.Click();

            IWebElement createNoteText = _driver.FindElement(MobileBy.AndroidUIAutomator
                ("new UiSelector().text(\"Text\")"));
            createNoteText.Click();

            IWebElement noteTextField = _driver.FindElement(MobileBy.Id
                ("com.socialnmobile.dictapps.notepad.color.note:id/edit_note"));
            noteTextField.SendKeys("Test_1");

            IWebElement backButton = _driver.FindElement(MobileBy.Id
                ("com.socialnmobile.dictapps.notepad.color.note:id/back_btn"));
            backButton.Click();
            backButton.Click();

            IWebElement createdNote = _driver.FindElement(MobileBy.Id
                ("com.socialnmobile.dictapps.notepad.color.note:id/title"));

            Assert.That(createdNote, Is.Not.Null, "Note was not created");

            Assert.That(createdNote.Text, Is.EqualTo("Test_1"));

        }

        [Test, Order(2)]
        public void TestUpdateNote()
        {
            IWebElement newNoteButton = _driver.FindElement(MobileBy.Id
                ("com.socialnmobile.dictapps.notepad.color.note:id/main_btn1"));
            newNoteButton.Click();

            IWebElement createNoteText = _driver.FindElement(MobileBy.AndroidUIAutomator
                ("new UiSelector().text(\"Text\")"));
            createNoteText.Click();

            IWebElement noteTextField = _driver.FindElement(MobileBy.Id
                ("com.socialnmobile.dictapps.notepad.color.note:id/edit_note"));
            noteTextField.SendKeys("Test_2");

            IWebElement backButton = _driver.FindElement(MobileBy.Id
                ("com.socialnmobile.dictapps.notepad.color.note:id/back_btn"));
            backButton.Click();
            backButton.Click();

            IWebElement note = _driver.FindElement(MobileBy.AndroidUIAutomator
                ("new UiSelector().text(\"Test_2\")"));
            note.Click();

            IWebElement editButton = _driver.FindElement(MobileBy.Id
                ("com.socialnmobile.dictapps.notepad.color.note:id/edit_btn"));
            editButton.Click();

            noteTextField = _driver.FindElement(MobileBy.Id
            ("com.socialnmobile.dictapps.notepad.color.note:id/edit_note"));
            noteTextField.Clear();
            noteTextField.Click();
            noteTextField.SendKeys("Edited");

            backButton = _driver.FindElement(MobileBy.AndroidUIAutomator
                ("new UiSelector().resourceId(\"com.socialnmobile.dictapps.notepad.color.note:id/back_btn\")"));

            backButton.Click();
            backButton.Click();

            IWebElement editedNote = _driver.FindElement(MobileBy.AndroidUIAutomator
                ("new UiSelector().text(\"Edited\")"));

            Assert.That(editedNote.Text, Is.EqualTo("Edited"));
        }

        [Test, Order(3)]
        public void TestDeleteNote()
        {
            IWebElement newNoteButton = _driver.FindElement(MobileBy.Id
                ("com.socialnmobile.dictapps.notepad.color.note:id/main_btn1"));
            newNoteButton.Click();

            IWebElement createNoteText = _driver.FindElement(MobileBy.AndroidUIAutomator
                ("new UiSelector().text(\"Text\")"));
            createNoteText.Click();

            IWebElement noteTextField = _driver.FindElement(MobileBy.Id
                ("com.socialnmobile.dictapps.notepad.color.note:id/edit_note"));

            noteTextField.SendKeys("NoteForDelete");

            IWebElement backButton = _driver.FindElement(MobileBy.Id
                ("com.socialnmobile.dictapps.notepad.color.note:id/back_btn"));
            backButton.Click();
            backButton.Click();

            IWebElement note = _driver.FindElement(MobileBy.AndroidUIAutomator
                ("new UiSelector().text(\"NoteForDelete\")"));

            note.Click();

            IWebElement menuButton = _driver.FindElement(MobileBy.Id
                ("com.socialnmobile.dictapps.notepad.color.note:id/menu_btn"));
            menuButton.Click();

            IWebElement deleteButton = _driver.FindElement(MobileBy.AndroidUIAutomator
                ("new UiSelector().text(\"Delete\")"));
            deleteButton.Click();

            IWebElement okButton = _driver.FindElement(MobileBy.Id("android:id/button1"));
            okButton.Click();

            var deletedNote = _driver.FindElements(MobileBy.XPath
                ("//android.widget.TextView[@text=\"NoteForDelete\"]"));

            Assert.That(deletedNote, Is.Empty, "Note was not deleted");
        }
    }
}