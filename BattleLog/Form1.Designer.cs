namespace BattleLog
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
            if (disposing && (components != null))
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
            this.StartWorkerBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.GuidBox = new System.Windows.Forms.TextBox();
            this.LogTB = new System.Windows.Forms.RichTextBox();
            this.sleepCount = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.StopWorkerBtn = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.JoinLimitCnt = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.requestCount = new System.Windows.Forms.NumericUpDown();
            this.PCount = new System.Windows.Forms.Label();
            this.ServerBox = new System.Windows.Forms.GroupBox();
            this.SrvUpdateLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ServerUpdate = new System.Windows.Forms.NumericUpDown();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Sip = new System.Windows.Forms.Label();
            this.SName = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.PlayerBox = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TickCounter = new System.Windows.Forms.NumericUpDown();
            this.ThreadsLabel = new System.Windows.Forms.Label();
            this.ThreadsCount = new System.Windows.Forms.NumericUpDown();
            this.AccsLabel = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.ProxyCB = new System.Windows.Forms.CheckBox();
            this.ProxyTB = new System.Windows.Forms.TextBox();
            this.ProxyLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.sleepCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.JoinLimitCnt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.requestCount)).BeginInit();
            this.ServerBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ServerUpdate)).BeginInit();
            this.panel1.SuspendLayout();
            this.PlayerBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TickCounter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThreadsCount)).BeginInit();
            this.SuspendLayout();
            // 
            // StartWorkerBtn
            // 
            this.StartWorkerBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StartWorkerBtn.Location = new System.Drawing.Point(722, 19);
            this.StartWorkerBtn.Name = "StartWorkerBtn";
            this.StartWorkerBtn.Size = new System.Drawing.Size(113, 35);
            this.StartWorkerBtn.TabIndex = 1;
            this.StartWorkerBtn.Text = "Start worker";
            this.StartWorkerBtn.UseVisualStyleBackColor = true;
            this.StartWorkerBtn.Click += new System.EventHandler(this.StartWorkerBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Guid:";
            // 
            // GuidBox
            // 
            this.GuidBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GuidBox.Location = new System.Drawing.Point(44, 17);
            this.GuidBox.Name = "GuidBox";
            this.GuidBox.Size = new System.Drawing.Size(308, 20);
            this.GuidBox.TabIndex = 3;
            this.GuidBox.TextChanged += new System.EventHandler(this.GuidBox_TextChanged);
            // 
            // LogTB
            // 
            this.LogTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LogTB.Location = new System.Drawing.Point(4, 173);
            this.LogTB.Name = "LogTB";
            this.LogTB.ReadOnly = true;
            this.LogTB.Size = new System.Drawing.Size(833, 164);
            this.LogTB.TabIndex = 7;
            this.LogTB.Text = "";
            this.LogTB.TextChanged += new System.EventHandler(this.LogTB_TextChanged);
            // 
            // sleepCount
            // 
            this.sleepCount.Location = new System.Drawing.Point(211, 118);
            this.sleepCount.Maximum = new decimal(new int[] {
            1241513983,
            370409800,
            542101,
            0});
            this.sleepCount.Name = "sleepCount";
            this.sleepCount.Size = new System.Drawing.Size(71, 20);
            this.sleepCount.TabIndex = 11;
            this.sleepCount.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.sleepCount.ValueChanged += new System.EventHandler(this.Sleep_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 120);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(150, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Sleep between requests (sec):";
            // 
            // StopWorkerBtn
            // 
            this.StopWorkerBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StopWorkerBtn.Enabled = false;
            this.StopWorkerBtn.Location = new System.Drawing.Point(722, 60);
            this.StopWorkerBtn.Name = "StopWorkerBtn";
            this.StopWorkerBtn.Size = new System.Drawing.Size(113, 35);
            this.StopWorkerBtn.TabIndex = 14;
            this.StopWorkerBtn.Text = "Stop worker";
            this.StopWorkerBtn.UseVisualStyleBackColor = true;
            this.StopWorkerBtn.Click += new System.EventHandler(this.StopWorkerBtn_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(133, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Max requests per account:";
            // 
            // JoinLimitCnt
            // 
            this.JoinLimitCnt.Location = new System.Drawing.Point(68, 118);
            this.JoinLimitCnt.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.JoinLimitCnt.Name = "JoinLimitCnt";
            this.JoinLimitCnt.Size = new System.Drawing.Size(57, 20);
            this.JoinLimitCnt.TabIndex = 17;
            this.JoinLimitCnt.ValueChanged += new System.EventHandler(this.JoinLimit_Changed);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 120);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "Join Limit:";
            // 
            // requestCount
            // 
            this.requestCount.Location = new System.Drawing.Point(211, 43);
            this.requestCount.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.requestCount.Name = "requestCount";
            this.requestCount.Size = new System.Drawing.Size(71, 20);
            this.requestCount.TabIndex = 19;
            this.requestCount.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.requestCount.ValueChanged += new System.EventHandler(this.Requests_ValueChanged);
            // 
            // PCount
            // 
            this.PCount.AutoSize = true;
            this.PCount.Location = new System.Drawing.Point(3, 26);
            this.PCount.Name = "PCount";
            this.PCount.Size = new System.Drawing.Size(64, 13);
            this.PCount.TabIndex = 21;
            this.PCount.Text = "Players: 0/0";
            // 
            // ServerBox
            // 
            this.ServerBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ServerBox.Controls.Add(this.ProxyCB);
            this.ServerBox.Controls.Add(this.ProxyTB);
            this.ServerBox.Controls.Add(this.ProxyLabel);
            this.ServerBox.Controls.Add(this.SrvUpdateLabel);
            this.ServerBox.Controls.Add(this.label3);
            this.ServerBox.Controls.Add(this.ServerUpdate);
            this.ServerBox.Controls.Add(this.panel1);
            this.ServerBox.Controls.Add(this.checkBox1);
            this.ServerBox.Controls.Add(this.GuidBox);
            this.ServerBox.Controls.Add(this.label9);
            this.ServerBox.Controls.Add(this.label1);
            this.ServerBox.Controls.Add(this.JoinLimitCnt);
            this.ServerBox.Enabled = false;
            this.ServerBox.Location = new System.Drawing.Point(302, 12);
            this.ServerBox.Name = "ServerBox";
            this.ServerBox.Size = new System.Drawing.Size(414, 155);
            this.ServerBox.TabIndex = 22;
            this.ServerBox.TabStop = false;
            this.ServerBox.Text = "Server";
            // 
            // SrvUpdateLabel
            // 
            this.SrvUpdateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SrvUpdateLabel.AutoSize = true;
            this.SrvUpdateLabel.Location = new System.Drawing.Point(300, 45);
            this.SrvUpdateLabel.Name = "SrvUpdateLabel";
            this.SrvUpdateLabel.Size = new System.Drawing.Size(108, 13);
            this.SrvUpdateLabel.TabIndex = 21;
            this.SrvUpdateLabel.Text = "Updated at: 00:00:00";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Update every (sec):";
            // 
            // ServerUpdate
            // 
            this.ServerUpdate.Location = new System.Drawing.Point(115, 41);
            this.ServerUpdate.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.ServerUpdate.Name = "ServerUpdate";
            this.ServerUpdate.Size = new System.Drawing.Size(57, 20);
            this.ServerUpdate.TabIndex = 19;
            this.ServerUpdate.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.ServerUpdate.ValueChanged += new System.EventHandler(this.ServerUpdate_Changed);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.Sip);
            this.panel1.Controls.Add(this.SName);
            this.panel1.Controls.Add(this.PCount);
            this.panel1.Location = new System.Drawing.Point(9, 67);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(399, 41);
            this.panel1.TabIndex = 5;
            // 
            // Sip
            // 
            this.Sip.AutoSize = true;
            this.Sip.Location = new System.Drawing.Point(3, 13);
            this.Sip.Name = "Sip";
            this.Sip.Size = new System.Drawing.Size(20, 13);
            this.Sip.TabIndex = 23;
            this.Sip.Text = "IP:";
            // 
            // SName
            // 
            this.SName.AutoSize = true;
            this.SName.Location = new System.Drawing.Point(3, 0);
            this.SName.Name = "SName";
            this.SName.Size = new System.Drawing.Size(38, 13);
            this.SName.TabIndex = 22;
            this.SName.Text = "Name:";
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(359, 20);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(50, 17);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "Lock";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.Lock_Ckecked);
            // 
            // PlayerBox
            // 
            this.PlayerBox.Controls.Add(this.label2);
            this.PlayerBox.Controls.Add(this.TickCounter);
            this.PlayerBox.Controls.Add(this.ThreadsLabel);
            this.PlayerBox.Controls.Add(this.ThreadsCount);
            this.PlayerBox.Controls.Add(this.AccsLabel);
            this.PlayerBox.Controls.Add(this.button3);
            this.PlayerBox.Controls.Add(this.label8);
            this.PlayerBox.Controls.Add(this.requestCount);
            this.PlayerBox.Controls.Add(this.label6);
            this.PlayerBox.Controls.Add(this.sleepCount);
            this.PlayerBox.Location = new System.Drawing.Point(4, 12);
            this.PlayerBox.Name = "PlayerBox";
            this.PlayerBox.Size = new System.Drawing.Size(294, 155);
            this.PlayerBox.TabIndex = 23;
            this.PlayerBox.TabStop = false;
            this.PlayerBox.Text = "Player";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "Tick time (sec):";
            // 
            // TickCounter
            // 
            this.TickCounter.Location = new System.Drawing.Point(211, 93);
            this.TickCounter.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.TickCounter.Name = "TickCounter";
            this.TickCounter.Size = new System.Drawing.Size(71, 20);
            this.TickCounter.TabIndex = 28;
            this.TickCounter.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.TickCounter.ValueChanged += new System.EventHandler(this.TimeTick_Changed);
            // 
            // ThreadsLabel
            // 
            this.ThreadsLabel.AutoSize = true;
            this.ThreadsLabel.Location = new System.Drawing.Point(6, 69);
            this.ThreadsLabel.Name = "ThreadsLabel";
            this.ThreadsLabel.Size = new System.Drawing.Size(115, 13);
            this.ThreadsLabel.TabIndex = 25;
            this.ThreadsLabel.Text = "Max accounts per tick:";
            // 
            // ThreadsCount
            // 
            this.ThreadsCount.Location = new System.Drawing.Point(211, 67);
            this.ThreadsCount.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
            this.ThreadsCount.Name = "ThreadsCount";
            this.ThreadsCount.Size = new System.Drawing.Size(71, 20);
            this.ThreadsCount.TabIndex = 26;
            this.ThreadsCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ThreadsCount.ValueChanged += new System.EventHandler(this.ThreadCount_Changed);
            // 
            // AccsLabel
            // 
            this.AccsLabel.AutoSize = true;
            this.AccsLabel.Location = new System.Drawing.Point(188, 19);
            this.AccsLabel.Name = "AccsLabel";
            this.AccsLabel.Size = new System.Drawing.Size(94, 13);
            this.AccsLabel.TabIndex = 24;
            this.AccsLabel.Text = "Accounts count: 0";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(6, 19);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(96, 23);
            this.button3.TabIndex = 0;
            this.button3.Text = "Load Accounts";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.LoadAccs_Click);
            // 
            // ProxyCB
            // 
            this.ProxyCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ProxyCB.AutoSize = true;
            this.ProxyCB.Location = new System.Drawing.Point(368, 120);
            this.ProxyCB.Name = "ProxyCB";
            this.ProxyCB.Size = new System.Drawing.Size(40, 17);
            this.ProxyCB.TabIndex = 24;
            this.ProxyCB.Text = "On";
            this.ProxyCB.UseVisualStyleBackColor = true;
            // 
            // ProxyTB
            // 
            this.ProxyTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProxyTB.Location = new System.Drawing.Point(173, 118);
            this.ProxyTB.Name = "ProxyTB";
            this.ProxyTB.Size = new System.Drawing.Size(189, 20);
            this.ProxyTB.TabIndex = 23;
            this.ProxyTB.TextChanged += new System.EventHandler(this.ProxyTB_TextChanged);
            // 
            // ProxyLabel
            // 
            this.ProxyLabel.AutoSize = true;
            this.ProxyLabel.Location = new System.Drawing.Point(131, 121);
            this.ProxyLabel.Name = "ProxyLabel";
            this.ProxyLabel.Size = new System.Drawing.Size(36, 13);
            this.ProxyLabel.TabIndex = 22;
            this.ProxyLabel.Text = "Proxy:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(841, 343);
            this.Controls.Add(this.PlayerBox);
            this.Controls.Add(this.ServerBox);
            this.Controls.Add(this.StopWorkerBtn);
            this.Controls.Add(this.LogTB);
            this.Controls.Add(this.StartWorkerBtn);
            this.MinimumSize = new System.Drawing.Size(730, 39);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FakePlayers";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sleepCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.JoinLimitCnt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.requestCount)).EndInit();
            this.ServerBox.ResumeLayout(false);
            this.ServerBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ServerUpdate)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.PlayerBox.ResumeLayout(false);
            this.PlayerBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TickCounter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThreadsCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button StartWorkerBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox GuidBox;
        private System.Windows.Forms.RichTextBox LogTB;
        private System.Windows.Forms.NumericUpDown sleepCount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button StopWorkerBtn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown JoinLimitCnt;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown requestCount;
        private System.Windows.Forms.Label PCount;
        private System.Windows.Forms.GroupBox ServerBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label SName;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label Sip;
        private System.Windows.Forms.GroupBox PlayerBox;
        private System.Windows.Forms.Label AccsLabel;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label ThreadsLabel;
        private System.Windows.Forms.NumericUpDown ThreadsCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown ServerUpdate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown TickCounter;
        private System.Windows.Forms.Label SrvUpdateLabel;
        private System.Windows.Forms.CheckBox ProxyCB;
        private System.Windows.Forms.TextBox ProxyTB;
        private System.Windows.Forms.Label ProxyLabel;
    }
}

