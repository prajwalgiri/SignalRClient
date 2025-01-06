namespace SignalRClientApp
{
    partial class ClientApp
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
            splitContainer1 = new SplitContainer();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            txtLog = new RichTextBox();
            groupBox3 = new GroupBox();
            btnConnect = new Button();
            lblStep1 = new Label();
            lblStep2 = new Label();
            lblStep3 = new Label();
            lblstep4 = new Label();
            lblStep1Tick = new Label();
            lblStep2Tick = new Label();
            lblStep3Tick = new Label();
            lblStep4Tick = new Label();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(groupBox1);
            splitContainer1.Panel1.Paint += splitContainer1_Panel1_Paint;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(groupBox2);
            splitContainer1.Size = new Size(800, 450);
            splitContainer1.SplitterDistance = 375;
            splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnConnect);
            groupBox1.Controls.Add(groupBox3);
            groupBox1.Location = new Point(10, 17);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(359, 422);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Connection";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(txtLog);
            groupBox2.Location = new Point(3, 17);
            groupBox2.Name = "groupBox2";
            groupBox2.RightToLeft = RightToLeft.Yes;
            groupBox2.Size = new Size(406, 422);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Log";
            // 
            // txtLog
            // 
            txtLog.BackColor = SystemColors.ActiveBorder;
            txtLog.Enabled = false;
            txtLog.Location = new Point(6, 22);
            txtLog.Name = "txtLog";
            txtLog.ScrollBars = RichTextBoxScrollBars.Vertical;
            txtLog.Size = new Size(394, 386);
            txtLog.TabIndex = 0;
            txtLog.Text = "";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(lblStep4Tick);
            groupBox3.Controls.Add(lblStep3Tick);
            groupBox3.Controls.Add(lblStep2Tick);
            groupBox3.Controls.Add(lblStep1Tick);
            groupBox3.Controls.Add(lblstep4);
            groupBox3.Controls.Add(lblStep3);
            groupBox3.Controls.Add(lblStep2);
            groupBox3.Controls.Add(lblStep1);
            groupBox3.Location = new Point(6, 22);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(347, 280);
            groupBox3.TabIndex = 0;
            groupBox3.TabStop = false;
            groupBox3.Text = "Steps";
            groupBox3.Enter += groupBox3_Enter;
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(246, 344);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(75, 23);
            btnConnect.TabIndex = 1;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            // 
            // lblStep1
            // 
            lblStep1.AutoSize = true;
            lblStep1.Location = new Point(15, 31);
            lblStep1.Name = "lblStep1";
            lblStep1.Size = new Size(67, 15);
            lblStep1.TabIndex = 0;
            lblStep1.Text = "1. First Step";
            // 
            // lblStep2
            // 
            lblStep2.AutoSize = true;
            lblStep2.Location = new Point(15, 67);
            lblStep2.Name = "lblStep2";
            lblStep2.Size = new Size(84, 15);
            lblStep2.TabIndex = 1;
            lblStep2.Text = "2. Second Step";
            // 
            // lblStep3
            // 
            lblStep3.AutoSize = true;
            lblStep3.Location = new Point(15, 106);
            lblStep3.Name = "lblStep3";
            lblStep3.Size = new Size(72, 15);
            lblStep3.TabIndex = 2;
            lblStep3.Text = "3. Third Step";
            // 
            // lblstep4
            // 
            lblstep4.AutoSize = true;
            lblstep4.Location = new Point(15, 139);
            lblstep4.Name = "lblstep4";
            lblstep4.Size = new Size(80, 15);
            lblstep4.TabIndex = 3;
            lblstep4.Text = "4. Fourth Step";
            // 
            // lblStep1Tick
            // 
            lblStep1Tick.AutoSize = true;
            lblStep1Tick.Location = new Point(133, 31);
            lblStep1Tick.Name = "lblStep1Tick";
            lblStep1Tick.Size = new Size(51, 15);
            lblStep1Tick.TabIndex = 4;
            lblStep1Tick.Tag = "lblStepStatus";
            lblStep1Tick.Text = "Pending";
            lblStep1Tick.Visible = false;
            // 
            // lblStep2Tick
            // 
            lblStep2Tick.AutoSize = true;
            lblStep2Tick.Location = new Point(133, 67);
            lblStep2Tick.Name = "lblStep2Tick";
            lblStep2Tick.Size = new Size(51, 15);
            lblStep2Tick.TabIndex = 5;
            lblStep2Tick.Tag = "lblStepStatus";
            lblStep2Tick.Text = "Pending";
            lblStep2Tick.Visible = false;
            // 
            // lblStep3Tick
            // 
            lblStep3Tick.AutoSize = true;
            lblStep3Tick.Location = new Point(133, 106);
            lblStep3Tick.Name = "lblStep3Tick";
            lblStep3Tick.Size = new Size(51, 15);
            lblStep3Tick.TabIndex = 6;
            lblStep3Tick.Tag = "lblStepStatus";
            lblStep3Tick.Text = "Pending";
            lblStep3Tick.Visible = false;
            // 
            // lblStep4Tick
            // 
            lblStep4Tick.AutoSize = true;
            lblStep4Tick.Location = new Point(133, 139);
            lblStep4Tick.Name = "lblStep4Tick";
            lblStep4Tick.Size = new Size(51, 15);
            lblStep4Tick.TabIndex = 7;
            lblStep4Tick.Tag = "lblStepStatus";
            lblStep4Tick.Text = "Pending";
            lblStep4Tick.Visible = false;
            // 
            // ClientApp
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(splitContainer1);
            Name = "ClientApp";
            Text = "ClientApp";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Button btnConnect;
        private GroupBox groupBox3;
        private RichTextBox txtLog;
        private Label lblstep4;
        private Label lblStep3;
        private Label lblStep2;
        private Label lblStep1;
        private Label lblStep4Tick;
        private Label lblStep3Tick;
        private Label lblStep2Tick;
        private Label lblStep1Tick;
    }
}