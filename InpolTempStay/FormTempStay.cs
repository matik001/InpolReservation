using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InpolTempStay
{
    public partial class FormTempStay : Form
    {
        static readonly string configPath = "config.json";
        static readonly string dataPath = "data.json";
        Config config;
        BotPool pool;
        bool isModifiedByCode = false;


        private async Task<string> CheckPermission()
        {
            try
            {
                var client = new System.Net.Http.HttpClient();
                var response = await client.GetStringAsync("https://files.matik.live/interpol.license.txt");

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
            var permissionError = await CheckPermission();
            if(permissionError != null)
            {
                MessageBox.Show(permissionError, "ERROR");
                Environment.Exit(0);
            }

            config = Config.Load(configPath);
            ConfigToView();
        }

        public FormTempStay()
        {
            InitializeComponent();
        }

        public bool _isRunning;

        public bool IsRunning
        {
            get => _isRunning;
            set
            {
                _isRunning = value;
                runBtn.Enabled = !_isRunning;
                stopBtn.Enabled = _isRunning;
                settingsGroup.Enabled = !_isRunning;
            }
        }

        private void ConfigToView()
        {
            isModifiedByCode = true;
            dbcLogin.Text = config.DBCLogin;
            dbcPass.Text = config.DBCPass;
            headlessCheckbox.Checked = config.Headless;
            numericUpDown1.Value = config.Workers;
            isModifiedByCode = false;
        }
        private void ViewToConfig()
        {
            config = new Config
            {
                DBCLogin = dbcLogin.Text,
                DBCPass = dbcPass.Text,
                Headless = headlessCheckbox.Checked,
                Workers = (int)numericUpDown1.Value
            };
            config.Save(configPath);
        }

        private void dbcLogin_TextChanged(object sender, EventArgs e)
        {
            if(!isModifiedByCode)
                ViewToConfig();
        }

        private void dbcPass_TextChanged(object sender, EventArgs e)
        {
            if(!isModifiedByCode)
                ViewToConfig();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if(!isModifiedByCode)
                ViewToConfig();
        }

        private void headlessCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if(!isModifiedByCode)
                ViewToConfig();
        }

        private int addedPeople;

        public int AddedPeople
        {
            get { return addedPeople; }
            set { 
                addedPeople = value;
                addedPeopleLabel.Text = addedPeople.ToString();
            }
        }

        private async void runBtn_Click(object sender, EventArgs e)
        {
            IsRunning = true;
            FormInfoList list = new FormInfoList();
            list.Load(dataPath);
            totalPeopleLabel.Text = list.FormsInfo.Count().ToString();
            AddedPeople = 0;
            pool = new BotPool(config, list, (log) =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    logs.Text += (DateTime.Now.ToShortTimeString() + " --- " + log + Environment.NewLine);
                });
            });
            pool.OnChangedFinishedCnt += (cnt) =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    AddedPeople = cnt;
                });
            };  
            await pool.Start();

            this.Invoke((MethodInvoker)delegate
            {
                IsRunning = false;
            });

        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            pool.Stop();
        }

        private void logs_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
