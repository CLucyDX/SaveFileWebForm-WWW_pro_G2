namespace Web_Servers
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            stop_button = new Button();
            start_button = new Button();
            IP_status = new RichTextBox();
            ip_box = new ComboBox();
            label2 = new Label();
            domain_text = new TextBox();
            create_button = new Button();
            label3 = new Label();
            label4 = new Label();
            panel1 = new Panel();
            ip_create = new TextBox();
            txtSoundPath = new TextBox();
            btnSelect = new Button();
            btnUpSound = new Button();
            label5 = new Label();
            panel2 = new Panel();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(12, 21);
            label1.Name = "label1";
            label1.Size = new Size(256, 27);
            label1.TabIndex = 1;
            label1.Text = "IP Address Administrate";
            label1.Click += label1_Click;
            // 
            // stop_button
            // 
            stop_button.BackColor = SystemColors.GradientInactiveCaption;
            stop_button.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            stop_button.Location = new Point(323, 143);
            stop_button.Name = "stop_button";
            stop_button.Size = new Size(136, 47);
            stop_button.TabIndex = 2;
            stop_button.Text = "Stop Server";
            stop_button.UseVisualStyleBackColor = false;
            stop_button.Click += stop_button_Click;
            // 
            // start_button
            // 
            start_button.BackColor = SystemColors.GradientInactiveCaption;
            start_button.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            start_button.Location = new Point(12, 143);
            start_button.Name = "start_button";
            start_button.Size = new Size(136, 47);
            start_button.TabIndex = 3;
            start_button.Text = "Start Server";
            start_button.UseVisualStyleBackColor = false;
            start_button.Click += start_button_Click;
            // 
            // IP_status
            // 
            IP_status.Location = new Point(12, 240);
            IP_status.Name = "IP_status";
            IP_status.Size = new Size(1400, 429);
            IP_status.TabIndex = 4;
            IP_status.Text = "";
            // 
            // ip_box
            // 
            ip_box.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ip_box.FormattingEnabled = true;
            ip_box.Items.AddRange(new object[] { "127.1.0.0", "127.1.0.1", "127.1.0.2", "127.1.0.3", "127.1.0.4", "127.1.0.5", "127.1.0.6", "127.1.0.7", "127.1.0.8", "127.1.0.9" });
            ip_box.Location = new Point(12, 74);
            ip_box.Name = "ip_box";
            ip_box.Size = new Size(447, 35);
            ip_box.TabIndex = 7;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(195, 3);
            label2.Name = "label2";
            label2.Size = new Size(241, 27);
            label2.TabIndex = 8;
            label2.Text = "Creat a new IP address";
            // 
            // domain_text
            // 
            domain_text.Location = new Point(167, 105);
            domain_text.Multiline = true;
            domain_text.Name = "domain_text";
            domain_text.Size = new Size(359, 33);
            domain_text.TabIndex = 11;
            // 
            // create_button
            // 
            create_button.BackColor = SystemColors.GradientInactiveCaption;
            create_button.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            create_button.Location = new Point(209, 157);
            create_button.Name = "create_button";
            create_button.Size = new Size(136, 47);
            create_button.TabIndex = 12;
            create_button.Text = "Create";
            create_button.UseVisualStyleBackColor = false;
            create_button.Click += create_button_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft YaHei UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(30, 52);
            label3.Name = "label3";
            label3.Size = new Size(101, 24);
            label3.TabIndex = 13;
            label3.Text = "IP Address";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft YaHei UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(30, 105);
            label4.Name = "label4";
            label4.Size = new Size(131, 24);
            label4.TabIndex = 14;
            label4.Text = "Domain name";
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(ip_create);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(create_button);
            panel1.Controls.Add(domain_text);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(484, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(569, 220);
            panel1.TabIndex = 15;
            // 
            // ip_create
            // 
            ip_create.Location = new Point(167, 52);
            ip_create.Multiline = true;
            ip_create.Name = "ip_create";
            ip_create.Size = new Size(359, 33);
            ip_create.TabIndex = 15;
            // 
            // txtSoundPath
            // 
            txtSoundPath.Location = new Point(34, 69);
            txtSoundPath.Multiline = true;
            txtSoundPath.Name = "txtSoundPath";
            txtSoundPath.Size = new Size(274, 48);
            txtSoundPath.TabIndex = 16;
            // 
            // btnSelect
            // 
            btnSelect.BackColor = SystemColors.GradientActiveCaption;
            btnSelect.Location = new Point(34, 154);
            btnSelect.Name = "btnSelect";
            btnSelect.Size = new Size(101, 34);
            btnSelect.TabIndex = 17;
            btnSelect.Text = "Select File";
            btnSelect.UseVisualStyleBackColor = false;
            btnSelect.Click += btnSelect_Click;
            // 
            // btnUpSound
            // 
            btnUpSound.BackColor = SystemColors.GradientActiveCaption;
            btnUpSound.Location = new Point(203, 154);
            btnUpSound.Name = "btnUpSound";
            btnUpSound.Size = new Size(105, 34);
            btnUpSound.TabIndex = 18;
            btnUpSound.Text = "UpLoading";
            btnUpSound.UseVisualStyleBackColor = false;
            btnUpSound.Click += btnUpSound_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(55, 5);
            label5.Name = "label5";
            label5.Size = new Size(234, 27);
            label5.TabIndex = 16;
            label5.Text = "Post file to IP Address";
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(label5);
            panel2.Controls.Add(btnUpSound);
            panel2.Controls.Add(btnSelect);
            panel2.Controls.Add(txtSoundPath);
            panel2.Location = new Point(1059, 12);
            panel2.Name = "panel2";
            panel2.Size = new Size(353, 220);
            panel2.TabIndex = 20;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1424, 681);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(ip_box);
            Controls.Add(IP_status);
            Controls.Add(start_button);
            Controls.Add(stop_button);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Servers Administrate";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Button stop_button;
        private Button start_button;
        private RichTextBox IP_status;
        private ComboBox ip_box;
        private Label label2;
        private TextBox domain_text;
        private Button create_button;
        private Label label3;
        private Label label4;
        private Panel panel1;
        private TextBox ip_create;
        private TextBox txtSoundPath;
        private Button btnSelect;
        private Button btnUpSound;
        private Label label5;
        private Panel panel2;
    }
}