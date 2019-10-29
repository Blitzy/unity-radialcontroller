namespace RadialControllerWinForm
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
            this.labelServerStatus = new System.Windows.Forms.Label();
            this.labelRadialOutput = new System.Windows.Forms.Label();
            this.labelServerStatusHeader = new System.Windows.Forms.Label();
            this.buttonSendTestMsg = new System.Windows.Forms.Button();
            this.labelLastServerMessageHeader = new System.Windows.Forms.Label();
            this.labelLastServerMessage = new System.Windows.Forms.Label();
            this.labelRadialOutputHeader = new System.Windows.Forms.Label();
            this.labelRunTime = new System.Windows.Forms.Label();
            this.labelRunTimeHeader = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.labelServerVersion = new System.Windows.Forms.Label();
            this.labelRotationResolutionHeader = new System.Windows.Forms.Label();
            this.labelUseAutoHapticFeedbackHeader = new System.Windows.Forms.Label();
            this.labelRotationResolution = new System.Windows.Forms.Label();
            this.labelUseAutoHapticFeedback = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelServerStatus
            // 
            this.labelServerStatus.AutoSize = true;
            this.labelServerStatus.Location = new System.Drawing.Point(24, 68);
            this.labelServerStatus.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelServerStatus.Name = "labelServerStatus";
            this.labelServerStatus.Size = new System.Drawing.Size(183, 25);
            this.labelServerStatus.TabIndex = 0;
            this.labelServerStatus.Text = "[Server status here]";
            // 
            // labelRadialOutput
            // 
            this.labelRadialOutput.AutoSize = true;
            this.labelRadialOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRadialOutput.Location = new System.Drawing.Point(24, 228);
            this.labelRadialOutput.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelRadialOutput.MaximumSize = new System.Drawing.Size(367, 0);
            this.labelRadialOutput.Name = "labelRadialOutput";
            this.labelRadialOutput.Size = new System.Drawing.Size(265, 25);
            this.labelRadialOutput.TabIndex = 2;
            this.labelRadialOutput.Text = "[Radial controller output here]";
            // 
            // labelServerStatusHeader
            // 
            this.labelServerStatusHeader.AutoSize = true;
            this.labelServerStatusHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelServerStatusHeader.Location = new System.Drawing.Point(24, 18);
            this.labelServerStatusHeader.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelServerStatusHeader.Name = "labelServerStatusHeader";
            this.labelServerStatusHeader.Size = new System.Drawing.Size(144, 25);
            this.labelServerStatusHeader.TabIndex = 3;
            this.labelServerStatusHeader.Text = "Server Status";
            // 
            // buttonSendTestMsg
            // 
            this.buttonSendTestMsg.Location = new System.Drawing.Point(24, 509);
            this.buttonSendTestMsg.Margin = new System.Windows.Forms.Padding(6, 96, 6, 6);
            this.buttonSendTestMsg.Name = "buttonSendTestMsg";
            this.buttonSendTestMsg.Size = new System.Drawing.Size(215, 42);
            this.buttonSendTestMsg.TabIndex = 8;
            this.buttonSendTestMsg.Text = "Send Test Message";
            this.buttonSendTestMsg.UseVisualStyleBackColor = true;
            this.buttonSendTestMsg.Click += new System.EventHandler(this.buttonSendTestMsg_Click);
            // 
            // labelLastServerMessageHeader
            // 
            this.labelLastServerMessageHeader.AutoSize = true;
            this.labelLastServerMessageHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLastServerMessageHeader.Location = new System.Drawing.Point(24, 587);
            this.labelLastServerMessageHeader.Margin = new System.Windows.Forms.Padding(6, 30, 6, 0);
            this.labelLastServerMessageHeader.Name = "labelLastServerMessageHeader";
            this.labelLastServerMessageHeader.Size = new System.Drawing.Size(312, 25);
            this.labelLastServerMessageHeader.TabIndex = 7;
            this.labelLastServerMessageHeader.Text = "Last Server Message Received";
            // 
            // labelLastServerMessage
            // 
            this.labelLastServerMessage.AutoSize = true;
            this.labelLastServerMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLastServerMessage.Location = new System.Drawing.Point(24, 612);
            this.labelLastServerMessage.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelLastServerMessage.Name = "labelLastServerMessage";
            this.labelLastServerMessage.Size = new System.Drawing.Size(343, 25);
            this.labelLastServerMessage.TabIndex = 6;
            this.labelLastServerMessage.Text = "[Last server message received output]";
            // 
            // labelRadialOutputHeader
            // 
            this.labelRadialOutputHeader.AutoSize = true;
            this.labelRadialOutputHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRadialOutputHeader.Location = new System.Drawing.Point(24, 203);
            this.labelRadialOutputHeader.Margin = new System.Windows.Forms.Padding(6, 30, 6, 0);
            this.labelRadialOutputHeader.Name = "labelRadialOutputHeader";
            this.labelRadialOutputHeader.Size = new System.Drawing.Size(243, 25);
            this.labelRadialOutputHeader.TabIndex = 1;
            this.labelRadialOutputHeader.Text = "Radial Controller Output";
            // 
            // labelRunTime
            // 
            this.labelRunTime.AutoSize = true;
            this.labelRunTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRunTime.Location = new System.Drawing.Point(24, 148);
            this.labelRunTime.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelRunTime.MaximumSize = new System.Drawing.Size(367, 0);
            this.labelRunTime.Name = "labelRunTime";
            this.labelRunTime.Size = new System.Drawing.Size(203, 25);
            this.labelRunTime.TabIndex = 4;
            this.labelRunTime.Text = "[Run time output here]";
            // 
            // labelRunTimeHeader
            // 
            this.labelRunTimeHeader.AutoSize = true;
            this.labelRunTimeHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRunTimeHeader.Location = new System.Drawing.Point(24, 123);
            this.labelRunTimeHeader.Margin = new System.Windows.Forms.Padding(6, 30, 6, 0);
            this.labelRunTimeHeader.Name = "labelRunTimeHeader";
            this.labelRunTimeHeader.Size = new System.Drawing.Size(104, 25);
            this.labelRunTimeHeader.TabIndex = 5;
            this.labelRunTimeHeader.Text = "Run Time";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.labelServerStatusHeader);
            this.flowLayoutPanel1.Controls.Add(this.labelServerVersion);
            this.flowLayoutPanel1.Controls.Add(this.labelServerStatus);
            this.flowLayoutPanel1.Controls.Add(this.labelRunTimeHeader);
            this.flowLayoutPanel1.Controls.Add(this.labelRunTime);
            this.flowLayoutPanel1.Controls.Add(this.labelRadialOutputHeader);
            this.flowLayoutPanel1.Controls.Add(this.labelRadialOutput);
            this.flowLayoutPanel1.Controls.Add(this.labelRotationResolutionHeader);
            this.flowLayoutPanel1.Controls.Add(this.labelRotationResolution);
            this.flowLayoutPanel1.Controls.Add(this.labelUseAutoHapticFeedbackHeader);
            this.flowLayoutPanel1.Controls.Add(this.labelUseAutoHapticFeedback);
            this.flowLayoutPanel1.Controls.Add(this.buttonSendTestMsg);
            this.flowLayoutPanel1.Controls.Add(this.labelLastServerMessageHeader);
            this.flowLayoutPanel1.Controls.Add(this.labelLastServerMessage);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(18, 18, 0, 0);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(990, 732);
            this.flowLayoutPanel1.TabIndex = 9;
            // 
            // labelServerVersion
            // 
            this.labelServerVersion.AutoSize = true;
            this.labelServerVersion.Location = new System.Drawing.Point(24, 43);
            this.labelServerVersion.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelServerVersion.Name = "labelServerVersion";
            this.labelServerVersion.Size = new System.Drawing.Size(194, 25);
            this.labelServerVersion.TabIndex = 9;
            this.labelServerVersion.Text = "[Server version here]";
            // 
            // labelRotationResolutionHeader
            // 
            this.labelRotationResolutionHeader.AutoSize = true;
            this.labelRotationResolutionHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRotationResolutionHeader.Location = new System.Drawing.Point(24, 283);
            this.labelRotationResolutionHeader.Margin = new System.Windows.Forms.Padding(6, 30, 6, 0);
            this.labelRotationResolutionHeader.Name = "labelRotationResolutionHeader";
            this.labelRotationResolutionHeader.Size = new System.Drawing.Size(309, 25);
            this.labelRotationResolutionHeader.TabIndex = 10;
            this.labelRotationResolutionHeader.Text = "Rotation Resolution In Degrees";
            // 
            // labelUseAutoHapticFeedbackHeader
            // 
            this.labelUseAutoHapticFeedbackHeader.AutoSize = true;
            this.labelUseAutoHapticFeedbackHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUseAutoHapticFeedbackHeader.Location = new System.Drawing.Point(24, 363);
            this.labelUseAutoHapticFeedbackHeader.Margin = new System.Windows.Forms.Padding(6, 30, 6, 0);
            this.labelUseAutoHapticFeedbackHeader.Name = "labelUseAutoHapticFeedbackHeader";
            this.labelUseAutoHapticFeedbackHeader.Size = new System.Drawing.Size(269, 25);
            this.labelUseAutoHapticFeedbackHeader.TabIndex = 12;
            this.labelUseAutoHapticFeedbackHeader.Text = "Use Auto Haptic Feedback";
            // 
            // labelRotationResolution
            // 
            this.labelRotationResolution.AutoSize = true;
            this.labelRotationResolution.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRotationResolution.Location = new System.Drawing.Point(24, 308);
            this.labelRotationResolution.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelRotationResolution.MaximumSize = new System.Drawing.Size(367, 0);
            this.labelRotationResolution.Name = "labelRotationResolution";
            this.labelRotationResolution.Size = new System.Drawing.Size(280, 25);
            this.labelRotationResolution.TabIndex = 13;
            this.labelRotationResolution.Text = "[Rotation resolution value here]";
            // 
            // labelUseAutoHapticFeedback
            // 
            this.labelUseAutoHapticFeedback.AutoSize = true;
            this.labelUseAutoHapticFeedback.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUseAutoHapticFeedback.Location = new System.Drawing.Point(24, 388);
            this.labelUseAutoHapticFeedback.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelUseAutoHapticFeedback.MaximumSize = new System.Drawing.Size(367, 0);
            this.labelUseAutoHapticFeedback.Name = "labelUseAutoHapticFeedback";
            this.labelUseAutoHapticFeedback.Size = new System.Drawing.Size(353, 25);
            this.labelUseAutoHapticFeedback.TabIndex = 14;
            this.labelUseAutoHapticFeedback.Text = "[Use Auto Haptic Feedback value here]";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(990, 732);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Radial Controller Server";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelServerStatus;
        private System.Windows.Forms.Label labelRadialOutput;
        private System.Windows.Forms.Label labelServerStatusHeader;
        private System.Windows.Forms.Button buttonSendTestMsg;
        private System.Windows.Forms.Label labelLastServerMessageHeader;
        private System.Windows.Forms.Label labelLastServerMessage;
        private System.Windows.Forms.Label labelRadialOutputHeader;
        private System.Windows.Forms.Label labelRunTime;
        private System.Windows.Forms.Label labelRunTimeHeader;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label labelServerVersion;
        private System.Windows.Forms.Label labelRotationResolutionHeader;
        private System.Windows.Forms.Label labelRotationResolution;
        private System.Windows.Forms.Label labelUseAutoHapticFeedbackHeader;
        private System.Windows.Forms.Label labelUseAutoHapticFeedback;
    }
}

