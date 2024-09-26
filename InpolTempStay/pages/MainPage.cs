using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.Extensions;
using System.Threading;

namespace InpolTempStay.pages
{
    internal class MainPage
    {
        private readonly IWebDriver _driver;
        private readonly FormInfo _config;
        private readonly CancellationToken _ct;

        public MainPage(IWebDriver driver, FormInfo config, CancellationToken ct) {
            _driver = driver;
            _config = config;
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
            get => By.XPath($"//*[@id='nf-field-75-wrap']/div/ul/li/input[contains(@value, '{_config.PobytNaPodstawie}')]");
        }
        private By ByDataWaznosciDokumentu = By.CssSelector($"#nf-field-85 + .ninja-forms-field");
        private By ByPelnomocnik = By.Id($"nf-field-86");
        private By ByRegulamin = By.Id($"nf-field-72");
        private By BySubmitBtn = By.Id($"nf-field-70");


        public MainPage WaitForPageReady()
        {
            while(true)
            {
                _ct.ThrowIfCancellationRequested();
                try
                {
                    //_driver.Navigate().GoToUrl("file:///C:/Users/mateu/Downloads/Compressed/Pobyt/Pobyt/Pobyt%20Czasowy.html");
                    _driver.Navigate().GoToUrl("https://pobyt-czasowy-zapis-na-zlozenie-wniosku.mazowieckie.pl");
                    var currentBy = _driver.WaitForAny(new List<By> { ByNotAvailiableFrame, ByKodWnioskuInpol, }, 60);
                    if(currentBy == null)
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
                            catch(Exception ex)
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
            _driver.FindElement(ByKodWnioskuInpol).SendKeys(_config.KodWnioskuInpol);
            _driver.FindElement(ByKodWnioskuBOS).SendKeys(_config.KodWnioskuMOS);
            _driver.FindElement(ByImie).SendKeys(_config.ImieCudzoziemca);
            _driver.FindElement(ByNazwisko).SendKeys(_config.NazwiskoCudzoziemca);
            _driver.FindElement(ByDataUrodzenia).PickDate(DateTime.Parse(_config.DataUrodzenia));
            _driver.FindElement(ByNrPaszportu).SendKeys(_config.NumerPaszportu);
            _driver.FindElement(ByObywatelstwo).SendKeys(_config.Obywatelstwo);
            _driver.FindElement(ByNrTelRP).SendKeys(_config.NumerTelefonuRP);
            _driver.FindElement(ByEmailPowiadomienie).SendKeys(_config.EmailPowiadomienie);
            _driver.FindElement(ByPobyt).ClickJS();
            _driver.FindElement(ByDataWaznosciDokumentu).PickDate(DateTime.Parse(_config.DataWaznosciDokumentu));
            _driver.FindElement(ByPelnomocnik).SendKeys(_config.DanePelnomocnika);
            _driver.FindElement(ByRegulamin).ClickJS();

            Task.Delay(10000, _ct).Wait();
            _ct.ThrowIfCancellationRequested();

            _driver.FindElement(BySubmitBtn).ClickJS();

            Task.Delay(10000, _ct).Wait();
            return this;
        }
    }
}
