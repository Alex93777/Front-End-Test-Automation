﻿using OpenQA.Selenium.Appium;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;

namespace AppiumDemoProject
{
    public class SummatorPage
    {
        private readonly AndroidDriver _driver;

        public SummatorPage(AndroidDriver driver)
        {
            _driver = driver;
        }

        public IWebElement Field1 => _driver.FindElement(MobileBy.Id
               ("com.example.androidappsummator:id/editText1"));

        public IWebElement Field2 => _driver.FindElement(MobileBy.Id
                ("com.example.androidappsummator:id/editText2"));

        public IWebElement CalcButton => _driver.FindElement(MobileBy.Id
                ("com.example.androidappsummator:id/buttonCalcSum"));

        public IWebElement ResultField => _driver.FindElement(MobileBy.Id
                ("com.example.androidappsummator:id/editTextSum"));

        public string Calculate(string number1, string number2)
        {
            Field1.Clear();
            Field2.Clear();

            Field1.SendKeys(number1);
            Field2.SendKeys(number2);
            CalcButton.Click();

            return ResultField.Text;
        }

        public void ClearFields()
        {
            Field1.Clear();
            Field2.Clear();
        }   
    }
}
