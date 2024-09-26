using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.Extensions;

namespace InpolReservation.InpolReservationBot.pages
{
    internal class MainPage
    {
        private readonly IWebDriver _driver;
        private readonly InpolConfig _config;

        public MainPage(IWebDriver driver, InpolConfig config) {
            _driver = driver;
            _config = config;
        }

        public CasePage GoToCase() 
        {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _driver.Navigate().GoToUrl("https://inpol.mazowieckie.pl/home");
            By BY_CASE_LINK = By.XPath($"//div[div/div/p[contains(text(), '{_config.CaseName}')]]//a");
            var link = _driver.FindElement(BY_CASE_LINK);
            _driver.ExecuteJavaScript("arguments[0].click();", link);
            return new CasePage(_driver, _config);
        }
    }
}
