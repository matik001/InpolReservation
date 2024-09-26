using DeathByCaptcha;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InpolReservation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private async Task<string> CheckPermission()
        {
            try
            {
                var client = new System.Net.Http.HttpClient();
                var response = await client.GetStringAsync("http://files.zaora.me/interpol.license.txt");

                if(response.StartsWith("ERROR\n"))
                    return response.Substring("ERROR\n".Length);
                return null;
            }
            catch(System.Exception e)
            {
            }
            return null;
        }
        private async void Form1_Load(object sender, EventArgs e)
        {
            IsRunning = false;
            var permissionError = await CheckPermission();
            if(permissionError != null)
            {
                MessageBox.Show(permissionError, "ERROR");
                Environment.Exit(0);
            }
        }
        /// VIEW
        public bool _isRunning;

        public bool IsRunning
        {
            get => _isRunning;
            set
            {
                _isRunning = value;
                runBtn.Enabled = !_isRunning;
                stopBtn.Enabled = _isRunning;
                headlessCheckbox.Enabled = !_isRunning;
            }
        }
        /// Events
        InpolReservationBot.InpolReservationBot bot;
        private async void button1_Click(object sender, EventArgs e)
        {
            var config = InpolConfig.Load("config.json");
            config.Headless = headlessCheckbox.Checked;
            IsRunning = true;
            bot = new InpolReservationBot.InpolReservationBot((log) =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    textBox1.Text += (DateTime.Now.ToShortTimeString() + " --- " + log + Environment.NewLine);
                });
            });
            await bot.Start(config);

            this.Invoke((MethodInvoker)delegate
            {
                IsRunning = false;
            });
        }


        private void stopBtn_Click(object sender, EventArgs e)
        {
            bot.Stop();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void headlessCheckbox_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
