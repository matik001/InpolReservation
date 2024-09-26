namespace InpolReservation
{
    partial class Form1
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
            this.runBtn = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.stopBtn = new System.Windows.Forms.Button();
            this.headlessCheckbox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // runBtn
            // 
            this.runBtn.Location = new System.Drawing.Point(194, 16);
            this.runBtn.Margin = new System.Windows.Forms.Padding(5);
            this.runBtn.Name = "runBtn";
            this.runBtn.Size = new System.Drawing.Size(131, 42);
            this.runBtn.TabIndex = 0;
            this.runBtn.Text = "Run";
            this.runBtn.UseVisualStyleBackColor = true;
            this.runBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 77);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(466, 522);
            this.textBox1.TabIndex = 2;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // stopBtn
            // 
            this.stopBtn.Location = new System.Drawing.Point(335, 16);
            this.stopBtn.Margin = new System.Windows.Forms.Padding(5);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(131, 42);
            this.stopBtn.TabIndex = 3;
            this.stopBtn.Text = "Stop";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // headlessCheckbox
            // 
            this.headlessCheckbox.AutoSize = true;
            this.headlessCheckbox.Location = new System.Drawing.Point(21, 22);
            this.headlessCheckbox.Name = "headlessCheckbox";
            this.headlessCheckbox.Size = new System.Drawing.Size(164, 33);
            this.headlessCheckbox.TabIndex = 4;
            this.headlessCheckbox.Text = "Background";
            this.headlessCheckbox.UseVisualStyleBackColor = true;
            this.headlessCheckbox.CheckedChanged += new System.EventHandler(this.headlessCheckbox_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 611);
            this.Controls.Add(this.headlessCheckbox);
            this.Controls.Add(this.stopBtn);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.runBtn);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "Form1";
            this.Text = "InpolBot";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button runBtn;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button stopBtn;
        private System.Windows.Forms.CheckBox headlessCheckbox;
    }
}

