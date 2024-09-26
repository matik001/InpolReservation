using DeathByCaptcha;
using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.Extensions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.CodeDom.Compiler;
using System.Xml.Linq;
using OpenQA.Selenium.Support.UI;
using SimpleJson;

namespace InpolReservation.InpolReservationBot.pages
{
    internal class InpolLoginPage
    {
        private static By BY_USERNAME = By.Id("mat-input-0");
        private static By BY_PASSWORD = By.Id("mat-input-1");
        private static By BY_CAPTCHA_IFRAME = By.XPath("//iframe[@title='reCAPTCHA']");
        private static By BY_SUBMIT_BTN = By.XPath("//button[contains(@class, 'btn--submit')]");

        private IWebDriver _driver;
        private readonly InpolConfig _config;


        public InpolLoginPage(IWebDriver driver, InpolConfig config)
        {
            _driver = driver;
            _config = config;
        }

        private void SolveCaptcha()
        {
            var captchaIframe = _driver.FindElement(BY_CAPTCHA_IFRAME);
            var submitBtn = _driver.FindElement(BY_SUBMIT_BTN);

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
                    submitBtn.ClickJS();

                    try {

                        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
                        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
                        var res = wait.Until(d => d.Url == "https://inpol.mazowieckie.pl/home");
                        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    }
                    catch (System.Exception ee) {
                        client.Report(captcha);
                        throw new CaptchaNotSolvedException("Error while solving Captcha");
                    }
                }
                else {
                    
                    throw new CaptchaNotSolvedException("Captcha not solved");
                }
            }
            catch(AccessDeniedException e)
            {
                throw;
            }
        }

        public InpolLoginPage Navigate()
        {
            _driver.Navigate().GoToUrl("https://inpol.mazowieckie.pl/login");
            return this;
        }
        public MainPage Login(LoginInfo loginInfo)
        {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            var usernameInput = _driver.FindElement(BY_USERNAME);
            var passInput = _driver.FindElement(BY_PASSWORD);
            usernameInput.SendKeys(loginInfo.Username);
            passInput.SendKeys(loginInfo.Pass);
            SolveCaptcha();

            return new MainPage(_driver, _config);
        }
    }
}
