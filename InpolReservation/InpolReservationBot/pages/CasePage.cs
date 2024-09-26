using DeathByCaptcha;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace InpolReservation.InpolReservationBot.pages
{
    internal class CasePage
    {
        private readonly IWebDriver _driver;
        private readonly InpolConfig _config;

        By BY_VISITS_BTN = By.XPath("//div[h3[text()='Umów się na wizytę w urzędzie']]/button");
        By BY_LOCALIZATION = By.Id("mat-select-0");
        By BY_QUEUE = By.Id("mat-select-2");
        By BY_OPTIONS_LOC = By.CssSelector("#cdk-overlay-0 mat-option");
        By BY_OPTIONS_QUE = By.CssSelector("#cdk-overlay-1 mat-option");
        By BY_HOURS = By.CssSelector(".tiles__link");
        By BY_HOURS_CONFIRM = By.XPath("//button[text()='Tak']");
        By BY_CLOSE_MODAL = By.XPath("//button[span[text()='Zamknij']]");

        

        By BY_MODAL_VERIFY_BTN = By.XPath("//button[@type='submit']");

        static By BY_CAPTCHA_IFRAME = By.XPath("//iframe[@title='reCAPTCHA']");

        static By BY_CALENDAR_OPTIONS =
            By.XPath("//mat-month-view//td[@role='gridcell' and not(contains(@class, 'disabled'))]");
        static By BY_CALENDAR_NEXT =
                    By.XPath("//button[@aria-label='Next month']");
        static By BY_CALENDAR_PREV =
                By.XPath("//button[@aria-label='Previous month']");

        public CasePage(IWebDriver driver, InpolConfig config)
        {
            _driver = driver;
            _config = config;
        }

        public CasePage ExpandVisits()
        {
            var expandBtn = _driver.FindElement(BY_VISITS_BTN);
            expandBtn.ClickJS();
            return this;
        }

        public CasePage SelectQueue()
        {
            var selectLoc = _driver.FindElement(BY_LOCALIZATION);
            var selectQue = _driver.FindElement(BY_QUEUE);

            selectLoc.ClickJS();
            var locOptions = _driver.FindElements(BY_OPTIONS_LOC);
                
            var location = locOptions.SingleOrDefault(a => a.Text.ToLower().Contains(_config.OfficeName.ToLower()));
            location.ClickJS();

            selectQue.ClickJS();
            var queOptions = _driver.FindElements(BY_OPTIONS_QUE);
            queOptions[1].ClickJS();

            return this;
        }


        public CasePage SolveCaptcha()
        {
            IWebElement captchaIframe = null;
            try
            {
                captchaIframe = _driver.FindElement(BY_CAPTCHA_IFRAME);
            }
            catch(System.Exception e)
            {
                return this;
            }
            var verifyBtn = _driver.FindElement(BY_MODAL_VERIFY_BTN);

            var src = captchaIframe.GetAttribute("src");
            var siteKeyRegex = new Regex("k=(.*?)&");
            var match = siteKeyRegex.Match(src);
            var siteKey = match.Groups[1].Value;

            string proxy = "";
            string proxyType = "";
            string pageurl = _driver.Url;

            string tokenParams = "{\"proxy\": \"" + proxy + "\"," +
                                 "\"proxytype\": \"" + proxyType + "\"," +
                                 "\"googlekey\": \"" + siteKey + "\"," +
                                 "\"pageurl\": \"" + pageurl + "\"}";
            try
            {
                Client client = (Client)new SocketClient("authtoken", _config.DBCKey);

                Captcha captcha = client.Decode(100,
                    new Hashtable()
                    {
                        {"type", 4},
                        {"token_params", tokenParams}
                    });

                if(null != captcha)
                {
                    _driver.ExecuteJavaScript(
                        $"document.getElementsByName('g-recaptcha-response')[0].innerHTML='{captcha.Text}'");
                    Thread.Sleep(300);
                    _driver.ActivateCaptcha(captcha.Text);
                    Thread.Sleep(300);
                    verifyBtn.ClickJS();

                    try
                    {
                        // _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
                        // var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
                        //
                        // var res = wait.Until(d => d.Url == "https://inpol.mazowieckie.pl/home");
                        // _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    }
                    catch(System.Exception ee)
                    {
                        client.Report(captcha);
                        throw new CaptchaNotSolvedException("Error while solving Captcha");
                    }
                }
                else
                {
                    throw new CaptchaNotSolvedException("Captcha not solved");
                }
            }
            catch(AccessDeniedException e)
            {
                throw;
            }

            return this;
        }

        public CasePage PickLatestDay()
        {
            var previousBtn  = _driver.FindElement(BY_CALENDAR_PREV);
            var nextBtn = _driver.FindElement(BY_CALENDAR_NEXT);
            while (true) {
                var options = _driver.FindElements(BY_CALENDAR_OPTIONS);
                if (options.Count == 0)
                    break;
                else
                    nextBtn.ClickJS();
            }
            previousBtn.ClickJS();
            var daysOptions = _driver.FindElements(BY_CALENDAR_OPTIONS);
            var last = daysOptions.Last();
            last.ClickJS();
            return this;
        }
        public CasePage PickNthDay(int n)
        {
            var previousBtn = _driver.FindElement(BY_CALENDAR_PREV);
            var nextBtn = _driver.FindElement(BY_CALENDAR_NEXT);
            while(true)
            {
                var options = _driver.FindElements(BY_CALENDAR_OPTIONS);
                if (options.Count == 0) {
                    previousBtn.ClickJS();
                    var daysOptions = _driver.FindElements(BY_CALENDAR_OPTIONS);
                    var last = daysOptions.Last();
                    last.ClickJS();
                    return this;
                }

                if (n <= options.Count) {
                    options[n-1].ClickJS();
                    return this;
                }

                n -= options.Count;
                nextBtn.ClickJS();
            }
        }

        public CasePage GoToFirstPage()
        {
            var previousBtn = _driver.FindElement(BY_CALENDAR_PREV);
            var nextBtn = _driver.FindElement(BY_CALENDAR_NEXT);
            while(true)
            {
                var options = _driver.FindElements(BY_CALENDAR_OPTIONS);
                if(options.Count == 0)
                {
                    nextBtn.ClickJS();
                    return this;
                }
                
                previousBtn.ClickJS();
            }

            return this;
        }
        public CasePage PickNthDayAndLeave(int n) {
            PickNthDay(n);
            try
            {
                var closeBtn = _driver.FindElement(BY_CLOSE_MODAL);
                closeBtn.ClickJS();
            }
            catch(System.Exception e)
            {
            }
            return this;
        }

        public CasePage PickNotLatestDayAndLeave()
        {
            var options = _driver.FindElements(BY_CALENDAR_OPTIONS).ToList();
            var option = options[options.Count - 2];
            option.ClickJS();

            try
            {
                var closeBtn = _driver.FindElement(BY_CLOSE_MODAL);
                closeBtn.ClickJS();
            }
            catch(System.Exception e)
            {
            }
            return this;
        }
        public CasePage PickLastHour()
        {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            var hourBtn = _driver.FindElement(BY_HOURS);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            hourBtn.ClickJS();

            var confirm = _driver.FindElement(BY_HOURS_CONFIRM);
            confirm.ClickJS();
            return SolveCaptcha();
        }
    }
}
