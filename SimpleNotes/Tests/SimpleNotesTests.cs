using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SimpleNotes.Pages;
using System;

namespace SimpleNotes.Tests
{
    public class SimpleNotesTests : BaseTest
    {

        public string lastCreatedNoteTitle;

        public string lastCreatedNoteDescription;

        [Test, Order(1)]
        public void AddNoteWithInvalidDataTest()
        {
            noteCreatePage.OpenPage();
            noteCreatePage.CreateNote("", "", "", "", "");
            noteCreatePage.AssertErrorMessages();

            Assert.IsTrue(driver.Url.Contains("/Note/Create"), "Should remain on Create Note page.");

        }

        [Test, Order(2)]
        public void AddRandomNoteTest()
        {
            lastCreatedNoteTitle = "Node" + GenerateRandomString(5);
            lastCreatedNoteDescription = "Description" + GenerateRandomString(30);

            noteCreatePage.OpenPage();
            noteCreatePage.CreateNote(lastCreatedNoteTitle, lastCreatedNoteDescription,
                "09/04/2025 10:49", "18/04/2025 10:49", "New");

            Assert.That(driver.Url, Is.EqualTo(newNotePage.Url), "Url is not correct");

            Assert.That(noteCreatePage.SuccessMessage.Text.Trim(),
                Is.EqualTo("Note created successfully!"), "Note is not created");
        }

        [Test, Order(3)]
        public void EditLastAddedNoteTest() 
        {
            newNotePage.OpenPage();
            newNotePage.EditButtonLastNode.Click();

            string updatedTitle = "Changed Title: " + lastCreatedNoteTitle;

            noteEditPage.TitleInput.Clear();
            noteEditPage.TitleInput.SendKeys(updatedTitle);
            noteEditPage.EditBtn.Click();

            newNotePage.OpenPage();

            Assert.That(newNotePage.TitleLastCreatedNode.Text.Trim(), Is.EqualTo(updatedTitle),
             "Title do not match");
        }

        [Test, Order(4)]
        public void MoveEditedNoteToDoneTest()
        {
            newNotePage.OpenPage();
            newNotePage.SetToDoneBtn.Click();

            Assert.That(noteCreatePage.SuccessMessage.Text.Trim(), Is.EqualTo("Note status changed successfully!"),
                "Note status does not changed");
        }

        [Test, Order(5)]
        public void DeleteEditedNoteTest()
        {
            driver.Navigate().GoToUrl("https://d5wfqm7y6yb3q.cloudfront.net/Note/Done");
            var doneNotes = driver.FindElements(By.XPath("//section[@class='p-4 d-flex justify-content-center text-center w-100']"));
            doneNotes.Last().FindElement(By.XPath(".//a[text()='Delete']")).Click();

            deleteNotePage.YesBtn.Click();

            Assert.That(noteCreatePage.DeleteNoteMessage.Text.Trim(), Is.EqualTo("Note deleted successfully!"),
                "Note was not deleted properly");
        }

        [Test, Order(6)]
        public void LogoutTest()
        {
            homePage.OpenPage();
            homePage.LogoutButton.Click();

            Assert.That(basePage.TitleBasePag.Text.Trim(), Is.EqualTo("SIMPLE NOTES"),
                "Not correct redirect to Base page");

            newNotePage.OpenPage();

            Assert.That(accessDeniedPage.AccessDeniedText.Text.Trim(), Is.EqualTo("Access Denied"));
        }
    }
}
