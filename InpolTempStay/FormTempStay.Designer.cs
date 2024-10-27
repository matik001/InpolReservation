namespace InpolTempStay
{
    partial class FormTempStay
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.headlessCheckbox = new System.Windows.Forms.CheckBox();
            this.stopBtn = new System.Windows.Forms.Button();
            this.logs = new System.Windows.Forms.TextBox();
            this.runBtn = new System.Windows.Forms.Button();
            this.settingsGroup = new System.Windows.Forms.GroupBox();
            this.dbcPass = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dbcLogin = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.addedPeopleLabel = new System.Windows.Forms.Label();
            this.totalPeopleLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBoxSolveCaptchaOnForm = new System.Windows.Forms.CheckBox();
            this.settingsGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // headlessCheckbox
            // 
            this.headlessCheckbox.AutoSize = true;
            this.headlessCheckbox.Location = new System.Drawing.Point(20, 130);
            this.headlessCheckbox.Name = "headlessCheckbox";
            this.headlessCheckbox.Size = new System.Drawing.Size(102, 20);
            this.headlessCheckbox.TabIndex = 8;
            this.headlessCheckbox.Text = "Background";
            this.headlessCheckbox.UseVisualStyleBackColor = true;
            this.headlessCheckbox.CheckedChanged += new System.EventHandler(this.headlessCheckbox_CheckedChanged);
            // 
            // stopBtn
            // 
            this.stopBtn.Location = new System.Drawing.Point(208, 108);
            this.stopBtn.Margin = new System.Windows.Forms.Padding(5);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(131, 42);
            this.stopBtn.TabIndex = 7;
            this.stopBtn.Text = "Stop";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // logs
            // 
            this.logs.Location = new System.Drawing.Point(18, 35);
            this.logs.Multiline = true;
            this.logs.Name = "logs";
            this.logs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logs.Size = new System.Drawing.Size(345, 361);
            this.logs.TabIndex = 6;
            this.logs.TextChanged += new System.EventHandler(this.logs_TextChanged);
            // 
            // runBtn
            // 
            this.runBtn.Location = new System.Drawing.Point(67, 108);
            this.runBtn.Margin = new System.Windows.Forms.Padding(5);
            this.runBtn.Name = "runBtn";
            this.runBtn.Size = new System.Drawing.Size(131, 42);
            this.runBtn.TabIndex = 5;
            this.runBtn.Text = "Run";
            this.runBtn.UseVisualStyleBackColor = true;
            this.runBtn.Click += new System.EventHandler(this.runBtn_Click);
            // 
            // settingsGroup
            // 
            this.settingsGroup.Controls.Add(this.checkBoxSolveCaptchaOnForm);
            this.settingsGroup.Controls.Add(this.dbcPass);
            this.settingsGroup.Controls.Add(this.label3);
            this.settingsGroup.Controls.Add(this.label2);
            this.settingsGroup.Controls.Add(this.dbcLogin);
            this.settingsGroup.Controls.Add(this.label1);
            this.settingsGroup.Controls.Add(this.numericUpDown1);
            this.settingsGroup.Controls.Add(this.headlessCheckbox);
            this.settingsGroup.Location = new System.Drawing.Point(12, 12);
            this.settingsGroup.Name = "settingsGroup";
            this.settingsGroup.Size = new System.Drawing.Size(409, 213);
            this.settingsGroup.TabIndex = 9;
            this.settingsGroup.TabStop = false;
            this.settingsGroup.Text = "Settings";
            // 
            // dbcPass
            // 
            this.dbcPass.Location = new System.Drawing.Point(130, 66);
            this.dbcPass.Name = "dbcPass";
            this.dbcPass.Size = new System.Drawing.Size(247, 22);
            this.dbcPass.TabIndex = 14;
            this.dbcPass.TextChanged += new System.EventHandler(this.dbcPass_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 16);
            this.label3.TabIndex = 13;
            this.label3.Text = "DBC password";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 16);
            this.label2.TabIndex = 12;
            this.label2.Text = "DBC login";
            // 
            // dbcLogin
            // 
            this.dbcLogin.Location = new System.Drawing.Point(130, 35);
            this.dbcLogin.Name = "dbcLogin";
            this.dbcLogin.Size = new System.Drawing.Size(247, 22);
            this.dbcLogin.TabIndex = 11;
            this.dbcLogin.TextChanged += new System.EventHandler(this.dbcLogin_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "Workers";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(130, 98);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(247, 22);
            this.numericUpDown1.TabIndex = 9;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.addedPeopleLabel);
            this.groupBox2.Controls.Add(this.totalPeopleLabel);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.runBtn);
            this.groupBox2.Controls.Add(this.stopBtn);
            this.groupBox2.Location = new System.Drawing.Point(12, 243);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(409, 185);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Execution";
            // 
            // addedPeopleLabel
            // 
            this.addedPeopleLabel.AutoSize = true;
            this.addedPeopleLabel.Location = new System.Drawing.Point(167, 66);
            this.addedPeopleLabel.Name = "addedPeopleLabel";
            this.addedPeopleLabel.Size = new System.Drawing.Size(14, 16);
            this.addedPeopleLabel.TabIndex = 18;
            this.addedPeopleLabel.Text = "0";
            // 
            // totalPeopleLabel
            // 
            this.totalPeopleLabel.AutoSize = true;
            this.totalPeopleLabel.Location = new System.Drawing.Point(157, 32);
            this.totalPeopleLabel.Name = "totalPeopleLabel";
            this.totalPeopleLabel.Size = new System.Drawing.Size(14, 16);
            this.totalPeopleLabel.TabIndex = 17;
            this.totalPeopleLabel.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(64, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 16);
            this.label5.TabIndex = 16;
            this.label5.Text = "Added people:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(64, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 16);
            this.label4.TabIndex = 15;
            this.label4.Text = "Total people:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.logs);
            this.groupBox3.Location = new System.Drawing.Point(439, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(383, 416);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Logs";
            // 
            // checkBoxSolveCaptchaOnForm
            // 
            this.checkBoxSolveCaptchaOnForm.AutoSize = true;
            this.checkBoxSolveCaptchaOnForm.Location = new System.Drawing.Point(20, 156);
            this.checkBoxSolveCaptchaOnForm.Name = "checkBoxSolveCaptchaOnForm";
            this.checkBoxSolveCaptchaOnForm.Size = new System.Drawing.Size(205, 20);
            this.checkBoxSolveCaptchaOnForm.TabIndex = 15;
            this.checkBoxSolveCaptchaOnForm.Text = "Should solve captcha on form";
            this.checkBoxSolveCaptchaOnForm.UseVisualStyleBackColor = true;
            this.checkBoxSolveCaptchaOnForm.CheckedChanged += new System.EventHandler(this.checkBoxSolveCaptchaOnForm_CheckedChanged);
            // 
            // FormTempStay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 450);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.settingsGroup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormTempStay";
            this.Text = "Bot wniosek";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.settingsGroup.ResumeLayout(false);
            this.settingsGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox headlessCheckbox;
        private System.Windows.Forms.Button stopBtn;
        private System.Windows.Forms.TextBox logs;
        private System.Windows.Forms.Button runBtn;
        private System.Windows.Forms.GroupBox settingsGroup;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.TextBox dbcPass;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox dbcLogin;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label addedPeopleLabel;
        private System.Windows.Forms.Label totalPeopleLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBoxSolveCaptchaOnForm;
    }
}

