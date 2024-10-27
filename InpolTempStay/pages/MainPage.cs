using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.Extensions;
using System.Threading;
using System.Collections;
using System.Text.RegularExpressions;
using DeathByCaptcha;

namespace InpolTempStay.pages
{
    internal class MainPage
    {
        private readonly IWebDriver _driver;
        private readonly Config _config;
        private readonly FormInfo _formInfo;
        private readonly CancellationToken _ct;

        public MainPage(IWebDriver driver, Config config, FormInfo formInfo, CancellationToken ct) {
            _driver = driver;
            _config = config;
            _formInfo = formInfo;
            _ct = ct;
        }

        private By ByNotAvailiableFrame = By.Id($"main-iframe");


        private By ByKodWnioskuInpol = By.Id($"nf-field-80");
        private By ByKodWnioskuBOS = By.Id($"nf-field-81");
        private By ByImie = By.Id($"nf-field-66");
        private By ByNazwisko = By.Id($"nf-field-67");
        private By ByDataUrodzenia = By.CssSelector($"#nf-field-82 + .ninja-forms-field");
        private By ByNrPaszportu = By.Id($"nf-field-83");
        private By ByObywatelstwo = By.Id($"nf-field-84");
        private By ByNrTelRP = By.Id($"nf-field-92");
        private By ByEmailPowiadomienie = By.Id($"nf-field-69");
        private By ByPobyt
        {
            get => By.XPath($"//*[@id='nf-field-75-wrap']/div/ul/li/input[contains(@value, '{_formInfo.PobytNaPodstawie}')]");
        }
        private By ByDataWaznosciDokumentu = By.CssSelector($"#nf-field-85 + .ninja-forms-field");
        private By ByPelnomocnik = By.Id($"nf-field-86");
        private By ByRegulamin = By.Id($"nf-field-72");
        private By BySubmitBtn = By.Id($"nf-field-70");

        private Predicate<IWebDriver> ErorrPred = (driver) =>
        {
            bool isError = (bool)(driver as IJavaScriptExecutor).ExecuteScript("window.location.href.includes('chrome-error')");
            if(isError)
                return true;
            return false;
        };
        public MainPage WaitForPageReady()
        {
            while(true)
            {
                _ct.ThrowIfCancellationRequested();
                try
                {
                    //_driver.Navigate().GoToUrl("file:///C:/Users/mateu/Downloads/Compressed/Pobyt/Pobyt/Pobyt%20Czasowy.html");
                    //_driver.Navigate().GoToUrl("http://safjsdlfjsda.eu");
                    _driver.Navigate().GoToUrl("https://pobyt-czasowy-zapis-na-zlozenie-wniosku.mazowieckie.pl");
                    var currentBy = _driver.WaitForAny(new List<By> { ByNotAvailiableFrame, ByKodWnioskuInpol, },
                                                    new List<Predicate<IWebDriver>>{ErorrPred}, 30);
                    if(currentBy == null)
                        continue;
                    else if(currentBy == ErorrPred)
                        continue;
                    else if(currentBy == ByNotAvailiableFrame)
                    {
                        var iframe = _driver.FindElement(ByNotAvailiableFrame);
                        _driver.SwitchTo().Frame(iframe);
                        var firstdiv = _driver.FindElement(By.XPath("/html/body/div"));
                        if(_driver.TryFindElement(By.ClassName("h-captcha")) != null)
                        {
                            try
                            {
                                if(_driver.WaitForReload(By.ClassName("h-captcha"), 90))
                                {
                                    if(_driver.TryFindElement(ByKodWnioskuInpol) != null)
                                    {
                                        break;
                                    }
                                }
                                continue;
                            }
                            catch(System.Exception ex)
                            {
                                continue;
                            }
                        }
                        else if(firstdiv.Text.Contains("Przyjmowanie zgłoszeń na złożenie wniosku o pobyt czasowy w sesji"))
                            continue;
                        else
                            continue;
                        //_driver.SwitchTo().ParentFrame();
                    }
                    else
                    {
                        return this;
                    }
                }
                catch(NoSuchElementException)
                {
                    Task.Delay(1000, _ct).Wait();
                    _ct.ThrowIfCancellationRequested();
                }
            }
            return this;
        }
        public MainPage FillForm() 
        {
            _driver.FindElement(ByKodWnioskuInpol).SendKeys(_formInfo.KodWnioskuInpol);
            _driver.FindElement(ByKodWnioskuBOS).SendKeys(_formInfo.KodWnioskuMOS);
            _driver.FindElement(ByImie).SendKeys(_formInfo.ImieCudzoziemca);
            _driver.FindElement(ByNazwisko).SendKeys(_formInfo.NazwiskoCudzoziemca);
            _driver.FindElement(ByDataUrodzenia).PickDate(DateTime.Parse(_formInfo.DataUrodzenia));
            _driver.FindElement(ByNrPaszportu).SendKeys(_formInfo.NumerPaszportu);
            _driver.FindElement(ByObywatelstwo).SendKeys(_formInfo.Obywatelstwo);
            _driver.FindElement(ByNrTelRP).SendKeys(_formInfo.NumerTelefonuRP);
            _driver.FindElement(ByEmailPowiadomienie).SendKeys(_formInfo.EmailPowiadomienie);
            _driver.FindElement(ByPobyt).ClickJS();
            _driver.FindElement(ByDataWaznosciDokumentu).PickDate(DateTime.Parse(_formInfo.DataWaznosciDokumentu));
            _driver.FindElement(ByPelnomocnik).SendKeys(_formInfo.DanePelnomocnika);
            _driver.FindElement(ByRegulamin).ClickJS();
            if(!_config.SolveFormCaptcha)
                return this;
            SolveCaptcha();
            _ct.ThrowIfCancellationRequested();
            _driver.FindElement(BySubmitBtn).ClickJS();
            return this;
        }

        private void SolveCaptcha()
        {
            var captchaElem = _driver.FindElement(By.ClassName("g-recaptcha"));
            var sitekey = captchaElem.GetAttribute("data-sitekey");
            var callback = captchaElem.GetAttribute("data-callback");



            string proxy = "";
            string proxyType = "";
            string pageurl = _driver.Url;

            string tokenParams = "{\"proxy\": \"" + proxy + "\"," +
                                 "\"proxytype\": \"" + proxyType + "\"," +
                                 "\"googlekey\": \"" + sitekey + "\"," +
                                 "\"pageurl\": \"" + pageurl + "\"}";
            try
            {
                Client client = (Client)new SocketClient(_config.DBCLogin, _config.DBCPass);

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
                    _driver.ExecuteJavaScript($"{callback}('{captcha.Text}')");
                    //_driver.ActivateCaptcha(captcha.Text);
                    Thread.Sleep(1000);
                }
                else
                {
                    throw new System.Exception("Captcha not solved");
                }
            }
            catch(AccessDeniedException e)
            {
                throw;
            }

        }
    }
}
