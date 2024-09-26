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
using SimpleJson;
using System.Xml.Linq;
using InpolReservation.InpolReservationBot.pages;
using OpenQA.Selenium.Chrome;
using System.Threading;
using System.Diagnostics;

namespace InpolReservation.InpolReservationBot
{
    class InpolReservationBot {
        private CancellationTokenSource cts;
        private Action<string> log;

        public InpolReservationBot(Action<string> log) {
            this.log = log;
        }
        public void Stop() {
            cts.Cancel();
        }
        public async Task Start(InpolConfig config) {
            cts = new CancellationTokenSource();
            await Task.Factory.StartNew(() => {
                var options = new ChromeOptions();
                if (config.Headless)
                    options.AddArgument("headless");
                options.AddArgument("--lang=pl");

                var chromedriverservice = ChromeDriverService.CreateDefaultService();
                chromedriverservice.HideCommandPromptWindow = true;

                log("Starting browser");
                using (var driver = new ChromeDriver(chromedriverservice, options)) {
                    foreach (var login in config.Logins) {
                        while(true) {
                            if(cts.IsCancellationRequested)
                                return;
                            if (driver.IsBrowserClosed()) {
                                return;
                            }

                  

                            try {
                                driver.Manage().Cookies.DeleteAllCookies();
                                // (driver as IJavaScriptExecutor).ExecuteScript("sessionStorage.clear();");
                                // (driver as IJavaScriptExecutor).ExecuteScript("localStorage.clear();");

                                Stopwatch sw = new Stopwatch();

                                var page = new InpolLoginPage(driver, config);
                                log($"Signing in to {login.Username}");

                                if(cts.IsCancellationRequested)
                                    return;

                                var mainPage = page.Navigate().Login(login);
                                log($"Going to case");

                                if(cts.IsCancellationRequested)
                                    return;

                                CasePage casePage = null;

                                for(int i = 0; i < 3; i++)
                                {

                                    if(cts.IsCancellationRequested)
                                        return;
                                    try
                                    {

                                        casePage = mainPage.GoToCase().ExpandVisits().SelectQueue();
                                        casePage.SolveCaptcha();
                                        break;
                                    }
                                    catch(CaptchaNotSolvedException e)
                                    {
                                        log("Captcha not solved " + (i + 1).ToString());
                                        if (i == 2) {
                                            throw;
                                        }
                                    }

                                }


                                while(true)
                                {
                                    sw.Start();
                                    for(int i = 0; i < 3; i++)
                                    {

                                        if(cts.IsCancellationRequested)
                                            return;
                                        try
                                        {
                                            log($"Picking nth day");
                                            if (i > 0)
                                                casePage.GoToFirstPage();
                                            casePage.PickNthDay(config.NthDay).SolveCaptcha();
                                            // casePage.PickLatestDay().SolveCaptcha();
                                            break;
                                        }
                                        catch(CaptchaNotSolvedException e)
                                        {
                                            log("Captcha not solved " + (i + 1).ToString());
                                            if(i == 2)
                                            {
                                                throw;
                                            }
                                        }
                                    }


                                    if(cts.IsCancellationRequested)
                                        return;
                                    try
                                    {
                                        log($"Picking last hour");
                                        casePage.PickLastHour();
                                        cts.Token.WaitHandle.WaitOne(TimeSpan.FromSeconds(10));
                                        log($"Booked successfully");

                                        if(cts.IsCancellationRequested)
                                            return;
                                        break;
                                    }
                                    catch(Exception e)
                                    {
                                    }


                                    if(cts.IsCancellationRequested)
                                        return;

                                    log($"Chaning day to previous");
                                    // casePage.PickNotLatestDayAndLeave();
                                    casePage.GoToFirstPage().PickNthDayAndLeave(config.NthDay == 1 ? 2 : config.NthDay > 30 ? 29 : config.NthDay - 1 );

                                    if(cts.IsCancellationRequested)
                                        return;

                                    sw.Stop();
                                    if(sw.Elapsed.TotalMilliseconds < config.CheckIntervalMs)
                                    {
                                        var sleepTime = config.CheckIntervalMs - (int)sw.Elapsed.TotalMilliseconds;
                                        log($"Waiting {sleepTime} ms");
                                        Thread.Sleep(sleepTime);
                                    }
                                }
                 


                                if(cts.IsCancellationRequested)
                                    return;

                         

                                break;

                            }
                            catch (Exception e) {
                                if(cts.IsCancellationRequested)
                                    return;
                                log(e.Message.Replace("\n", "\t").Replace("\r", "\t"));
                                continue;
                            }


                        }

                    }
                }
            }, cts.Token);
        }
    }
}
