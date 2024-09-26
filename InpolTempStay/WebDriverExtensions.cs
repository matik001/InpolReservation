using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Support.Extensions;
using System.Drawing;
using System.Threading;
using OpenQA.Selenium.Interactions;
using System.Xml.Linq;

namespace InpolTempStay
{
    public static class WebDriverExtensions
    {
        public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            if(timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.TryFindElement(by));
            }
            return driver.FindElement(by);
        }

        public static By WaitForAny(this IWebDriver driver, List<By> byList, int timeoutInSeconds)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            try
            {
                return wait.Until(drv =>
                {
                    foreach(var by in byList)
                    {
                        var elem = drv.TryFindElement(by);
                        if(elem != null)
                            return by;
                    }
                    return null;
                });
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        public static IWebElement TryFindElement(this IWebDriver driver, By by)
        {
            try
            {
                return driver.FindElement(by);
            }
            catch(NoSuchElementException)
            {
                return null;
            }
        }
        public static IWebElement TryFindElement(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            try
            {
                if(timeoutInSeconds > 0)
                {
                    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                    return wait.Until(drv => drv.TryFindElement(by));
                }
                return driver.FindElement(by);
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        public static bool WaitForReload(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            try
            {

                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv =>
                {
                    if(drv.TryFindElement(by) == null)
                        return true;
                    else
                        return false;
                });
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public static IWebElement Scroll(this IWebElement element)
        {
            var driver = (element as WebElement).WrappedDriver;

            Actions actions = new Actions(driver);
            actions.MoveToElement(element);
            actions.Perform();
            return element;
        }
        public static void ScrollAndClick(this IWebElement element)
        {
            var driver = (element as WebElement).WrappedDriver;

            Actions actions = new Actions(driver);
            actions.MoveToElement(element);
            actions.Click();
            actions.Perform();
        }
        public static void WaitForClickable(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            //try
            //{

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
            //}
            //catch(Exception ex)
            //{
            //}
        }
        public static IWebElement WaitForClickable(this IWebElement element, int timeoutInSeconds)
        {
            //try
            //{
            var driver = (element as WebElement).WrappedDriver;

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
            return element;
            //}
            //catch(Exception ex)
            //{
            //}
        }
        public static void ClickJS(this IWebElement elem)
        {
            (elem as WebElement).WrappedDriver.ExecuteJavaScript("arguments[0].click();", elem);
        }
        public static void PickDate(this IWebElement elem, DateTime date)
        {
            elem.Click();
            var driver = (elem as WebElement).WrappedDriver;

            var month = driver.TryFindElement(By.CssSelector(".open .flatpickr-monthDropdown-months"), 5);
            var monthSelect = new SelectElement(month);
            monthSelect.SelectByValue((date.Month - 1).ToString());

            var year = driver.FindElement(By.CssSelector(".open .cur-year"));
            year.Clear();
            year.SendKeys(date.Year.ToString() + Keys.Return);
            Thread.Sleep(1000);
            var day = driver.FindElement(By.XPath($"//div[contains(@class, 'open')]//span[text()='{date.Day}']"));
            day.Click();
            //(elem as WebElement).WrappedDriver.ExecuteJavaScript("arguments[0].click();", elem);
        }
    }
}
