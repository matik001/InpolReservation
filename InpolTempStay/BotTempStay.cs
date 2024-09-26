using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.Extensions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Xml.Linq;
using OpenQA.Selenium.Chrome;
using System.Threading;
using System.Diagnostics;
using InpolTempStay.pages;

namespace InpolTempStay
{
    class BotTempStay 
    {
        private Action<string> log;
        
        public BotTempStay(Action<string> log) {
            this.log = log;
        }
        public async Task Start(Config config, FormInfo formInfo, CancellationToken ct) 
        {
            await Task.Factory.StartNew(() => {
                var options = new ChromeOptions();
                if (config.Headless)
                    options.AddArgument("headless");
                options.AddArgument("--lang=pl");

                var chromedriverservice = ChromeDriverService.CreateDefaultService();
                chromedriverservice.HideCommandPromptWindow = true;

                log("Starting browser");
                options.AddExtension("dbc.crx");
                options.AddArgument("--disable-search-engine-choice-screen");

                try {
                    using(var driver = new ChromeDriver(chromedriverservice, options)) {
                        Task.Delay(2000).Wait();
                        driver.SwitchTo().Window(driver.WindowHandles[1]);
                        driver.Close();
                        driver.SwitchTo().Window(driver.WindowHandles[0]);

                        ConfigDBCPage configDBCPage = new ConfigDBCPage(driver);
                        configDBCPage.Login(config.DBCLogin, config.DBCPass);


                        log($"Started for {formInfo.ImieCudzoziemca} {formInfo.NazwiskoCudzoziemca}");

                        MainPage mainPage = new MainPage(driver, formInfo, ct);
                        mainPage.WaitForPageReady()
                                .FillForm();
                        log($"Filled for {formInfo.ImieCudzoziemca} {formInfo.NazwiskoCudzoziemca}");

                        if(ct.IsCancellationRequested)
                            return;
                    }
                }
                catch(Exception ex)
                {
                    log($"Error: {ex.Message}");
                }
            }, ct);
        }
    }
}
