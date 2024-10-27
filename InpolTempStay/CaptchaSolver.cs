using DeathByCaptcha;
using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InpolTempStay
{
    internal class CaptchaSolver
    {
        string login;
        string pass;

        public CaptchaSolver(string login, string pass)
        {
            this.login = login;
            this.pass = pass;
        }

        public string SolveCaptcha(string url, string sitekey)
        {
            string proxy = "";
            string proxyType = "";
            string pageurl = url;

            string tokenParams = "{\"proxy\": \"" + proxy + "\"," +
                                 "\"proxytype\": \"" + proxyType + "\"," +
                                 "\"googlekey\": \"" + sitekey + "\"," +
                                 "\"pageurl\": \"" + pageurl + "\"}";
            try
            {
                Client client = (Client)new SocketClient(login, pass);

                Captcha captcha = client.Decode(100,
                    new Hashtable()
                    {
                        {"type", 4},
                        {"token_params", tokenParams}
                    });
                return captcha.Text;
            }
            catch(AccessDeniedException e)
            {
                throw;
            }

        }
    }
}
