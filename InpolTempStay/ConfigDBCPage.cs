using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InpolTempStay
{
    internal class ConfigDBCPage
    {
        IWebDriver driver;
        public ConfigDBCPage(IWebDriver driver)
        {
            this.driver = driver;
        }
        By bySubmitLogin = By.Name("autoSubmitForms");
        public void Login(string login, string pass)
        {
            driver.Navigate().GoToUrl("chrome-extension://ejagiilfhmflpcohicichiokfoofeljp/popup/popup.html");
            driver.FindElement(By.Name("username")).SendKeys(login);
            driver.FindElement(By.Name("password")).SendKeys(pass);
            driver.FindElement(By.TagName("button")).WaitForClickable(5).Click();

            driver.FindElement(bySubmitLogin).WaitForClickable(5).Click();

            var repeatInput = driver.FindElement(By.Name("repeatOnErrorTimes"));
            repeatInput.Clear();
            repeatInput.SendKeys("5");
            foreach(var elem in driver.FindElements(By.XPath("//*[@id=\"settings-form\"]/div/table[2]//td/input[2]")))
            {
                elem.Scroll().WaitForClickable(5).Click();
            }
            driver.Navigate().Refresh();
        }
    }
}
