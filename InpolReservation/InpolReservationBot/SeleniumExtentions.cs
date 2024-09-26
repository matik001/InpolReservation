using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;

namespace InpolReservation.InpolReservationBot
{
    public static class SeleniumExtensions
    {
        public static void ClickJS(this IWebElement elem) {
            (elem as WebElement).WrappedDriver.ExecuteJavaScript("arguments[0].click();", elem);
        }
        public static bool IsBrowserClosed(this IWebDriver driver)
        {
            bool isClosed = false;
            try
            {
                var test = driver.Title;
            }
            catch(Exception e)
            {
                isClosed = true;
            }

            return isClosed;
        }

        public static void ActivateCaptcha(this IWebDriver driver, string solution) {
            driver.ExecuteJavaScript("const findName = (obj, name)=>{\r\n\tif(obj == null || typeof obj !== 'object')\r\n\t\treturn  null;\r\n\tconst keys = Object.keys(obj);\r\n\tfor(const key of keys){\r\n\t\tif(key === name)\r\n\t\t\treturn obj[key];\r\n\t\tconst res = findName(obj[key], name);\r\n\t\tif(res)\r\n\t\t\treturn res;\r\n\t}\t\r\n}\r\n\r\n\r\nconst captchaClient = window.___grecaptcha_cfg.clients[Object.keys(window.___grecaptcha_cfg.clients).length-1];\r\nfindName(captchaClient, \"callback\")('"+solution+"');");
        }
    }
}
