using OpenQA.Selenium;
namespace IdeaCenter.Tests;

using IdeaCenter.Pages;
using System.Collections.ObjectModel;

public class IdeaCenterTests : BaseTest
{
    public string lastCreatedIdeaTitle;

    public string lastCreatedIdeaDescription;


    [Test, Order(1)]
    public void CreateIdeaWithInvalidDataTest()
    {
        createIdeaPage.OpenPage();
        createIdeaPage.CreateIdea("", "", "");
        createIdeaPage.AssertErrorMessages();
    }

    [Test, Order(2)]
    public void CreateIdeaWithValidDataTest()
    {
        lastCreatedIdeaTitle = "Idea" + GenerateRandomString(5);
        lastCreatedIdeaDescription = "Description" + GenerateRandomString(5);

        createIdeaPage.OpenPage();
        createIdeaPage.CreateIdea(lastCreatedIdeaTitle, "", lastCreatedIdeaDescription);

        Assert.That(driver.Url, Is.EqualTo(myIdeasPage.Url), "Url is not correct");

        Assert.That(myIdeasPage.DescriptionLastIdea.Text.Trim(), Is.EqualTo(lastCreatedIdeaDescription),
            "Descriptions not match");
    }

    [Test, Order(3)]
    public void ViewLastCreatedIdeaTest()
    {
        myIdeasPage.OpenPage();

        myIdeasPage.ViewButtonLastIdea.Click();

        Assert.That(ideasReadPage.IdeaTitle.Text.Trim(), Is.EqualTo(lastCreatedIdeaTitle),
            "Title do not match");

        Assert.That(ideasReadPage.IdeaDescription.Text.Trim(), Is.EqualTo(lastCreatedIdeaDescription),
            "Description do not match");
    }

    [Test, Order(4)]
    public void EditIdeaTitleTest()
    {
        myIdeasPage.OpenPage();

        myIdeasPage.EditButtonLastIdea.Click();

        string updatedTitle = "Changed Title: " + lastCreatedIdeaTitle;

        ideasEditPage.TitleInput.Clear();
        ideasEditPage.TitleInput.SendKeys(updatedTitle);
        ideasEditPage.EditBtn.Click();

        Assert.That(driver.Url, Is.EqualTo(myIdeasPage.Url),
            "Not correct redirect");

        myIdeasPage.ViewButtonLastIdea.Click();

        Assert.That(ideasReadPage.IdeaTitle.Text.Trim(), Is.EqualTo(updatedTitle),
            "Title do not match");
    }

    [Test, Order(5)]
    public void EditIdeaDescriptionTest()
    {
        myIdeasPage.OpenPage();

        myIdeasPage.EditButtonLastIdea.Click();

        string updatedDescription = "Changed Title: " + lastCreatedIdeaDescription;

        ideasEditPage.DescriptionInput.Clear();
        ideasEditPage.DescriptionInput.SendKeys(updatedDescription);
        ideasEditPage.EditBtn.Click();

        Assert.That(driver.Url, Is.EqualTo(myIdeasPage.Url),
            "Not correct redirect");

        myIdeasPage.ViewButtonLastIdea.Click();

        Assert.That(ideasReadPage.IdeaDescription.Text.Trim(), Is.EqualTo(updatedDescription),
            "Description do not match");
    }

    [Test, Order(6)]
    public void DeleteLastIdeaTest()
    {
        myIdeasPage.OpenPage();

        myIdeasPage.DeleteButtonLastIdea.Click();

        bool isIdeaDeleted = myIdeasPage.IdeasCards.All
            (card => card.Text.Contains(lastCreatedIdeaDescription));

        Assert.IsFalse(isIdeaDeleted, "The idea was not deleted");
    }
}
