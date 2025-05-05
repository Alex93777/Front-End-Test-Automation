using OpenQA.Selenium;

namespace SimpleNotes.Pages
{
    public class AccessDeniedPage : BasePage
    {
        public AccessDeniedPage(IWebDriver driver) : base(driver) 
        {
            
        }

        public string Url =
            "http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com:1025/User/LoginRegister?ReturnUrl=%2FNote%2FNew";
        public IWebElement  AccessDeniedText => driver.FindElement(By.XPath
       ("//pre[text()='Access Denied']"));
    }
}
