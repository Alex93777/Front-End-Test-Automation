using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POMExercise.Pages
{
    public class HiddenMenuPage : BasePage
    {
        protected readonly By hamburgerMenuButton = By.Id("react-burger-menu-btn");

        protected readonly By logoutButton = By.XPath("//a[text()='Logout']");


        public HiddenMenuPage(IWebDriver driver) : base(driver)
        {
            
        }

        public void ClickHamburgerMenuButton()
        {
            Click(hamburgerMenuButton);
        }

        public void ClickLogoutButton()
        {
            Click(logoutButton);
        }

        public bool IsMenuOpen()
        {
            return FindElement(logoutButton).Displayed;
        }
    }
}
