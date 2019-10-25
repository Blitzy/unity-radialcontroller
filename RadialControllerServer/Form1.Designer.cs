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
            this.labelRadialOutputHeader = new System.Windows.Forms.Label();
            this.labelRadialOutput = new System.Windows.Forms.Label();
            this.labelServerStatusHeader = new System.Windows.Forms.Label();
            this.labelRunTime = new System.Windows.Forms.Label();
            this.labelRunTimeHeader = new System.Windows.Forms.Label();
            this.labelFrameCountHeader = new System.Windows.Forms.Label();
            this.labelFrameCount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelServerStatus
            // 
            this.labelServerStatus.AutoSize = true;
            this.labelServerStatus.Location = new System.Drawing.Point(12, 42);
            this.labelServerStatus.Name = "labelServerStatus";
            this.labelServerStatus.Size = new System.Drawing.Size(99, 13);
            this.labelServerStatus.TabIndex = 0;
            this.labelServerStatus.Text = "[Server status here]";
            // 
            // labelRadialOutputHeader
            // 
            this.labelRadialOutputHeader.AutoSize = true;
            this.labelRadialOutputHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRadialOutputHeader.Location = new System.Drawing.Point(12, 195);
            this.labelRadialOutputHeader.Name = "labelRadialOutputHeader";
            this.labelRadialOutputHeader.Size = new System.Drawing.Size(143, 13);
            this.labelRadialOutputHeader.TabIndex = 1;
            this.labelRadialOutputHeader.Text = "Radial Controller Output";
            // 
            // labelRadialOutput
            // 
            this.labelRadialOutput.AutoSize = true;
            this.labelRadialOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRadialOutput.Location = new System.Drawing.Point(12, 217);
            this.labelRadialOutput.MaximumSize = new System.Drawing.Size(200, 0);
            this.labelRadialOutput.Name = "labelRadialOutput";
            this.labelRadialOutput.Size = new System.Drawing.Size(146, 13);
            this.labelRadialOutput.TabIndex = 2;
            this.labelRadialOutput.Text = "[Radial controller output here]";
            // 
            // labelServerStatusHeader
            // 
            this.labelServerStatusHeader.AutoSize = true;
            this.labelServerStatusHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelServerStatusHeader.Location = new System.Drawing.Point(12, 22);
            this.labelServerStatusHeader.Name = "labelServerStatusHeader";
            this.labelServerStatusHeader.Size = new System.Drawing.Size(84, 13);
            this.labelServerStatusHeader.TabIndex = 3;
            this.labelServerStatusHeader.Text = "Server Status";
            // 
            // labelRunTime
            // 
            this.labelRunTime.AutoSize = true;
            this.labelRunTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRunTime.Location = new System.Drawing.Point(12, 100);
            this.labelRunTime.MaximumSize = new System.Drawing.Size(200, 0);
            this.labelRunTime.Name = "labelRunTime";
            this.labelRunTime.Size = new System.Drawing.Size(112, 13);
            this.labelRunTime.TabIndex = 4;
            this.labelRunTime.Text = "[Run time output here]";
            // 
            // labelRunTimeHeader
            // 
            this.labelRunTimeHeader.AutoSize = true;
            this.labelRunTimeHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRunTimeHeader.Location = new System.Drawing.Point(12, 76);
            this.labelRunTimeHeader.Name = "labelRunTimeHeader";
            this.labelRunTimeHeader.Size = new System.Drawing.Size(61, 13);
            this.labelRunTimeHeader.TabIndex = 5;
            this.labelRunTimeHeader.Text = "Run Time";
            // 
            // labelFrameCountHeader
            // 
            this.labelFrameCountHeader.AutoSize = true;
            this.labelFrameCountHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFrameCountHeader.Location = new System.Drawing.Point(12, 133);
            this.labelFrameCountHeader.Name = "labelFrameCountHeader";
            this.labelFrameCountHeader.Size = new System.Drawing.Size(78, 13);
            this.labelFrameCountHeader.TabIndex = 7;
            this.labelFrameCountHeader.Text = "Frame Count";
            // 
            // labelFrameCount
            // 
            this.labelFrameCount.AutoSize = true;
            this.labelFrameCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFrameCount.Location = new System.Drawing.Point(12, 157);
            this.labelFrameCount.MaximumSize = new System.Drawing.Size(200, 0);
            this.labelFrameCount.Name = "labelFrameCount";
            this.labelFrameCount.Size = new System.Drawing.Size(129, 13);
            this.labelFrameCount.TabIndex = 6;
            this.labelFrameCount.Text = "[Frame count output here]";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 333);
            this.Controls.Add(this.labelFrameCountHeader);
            this.Controls.Add(this.labelFrameCount);
            this.Controls.Add(this.labelRunTimeHeader);
            this.Controls.Add(this.labelRunTime);
            this.Controls.Add(this.labelServerStatusHeader);
            this.Controls.Add(this.labelRadialOutput);
            this.Controls.Add(this.labelRadialOutputHeader);
            this.Controls.Add(this.labelServerStatus);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Radial Controller Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelServerStatus;
        private System.Windows.Forms.Label labelRadialOutputHeader;
        private System.Windows.Forms.Label labelRadialOutput;
        private System.Windows.Forms.Label labelServerStatusHeader;
        private System.Windows.Forms.Label labelRunTime;
        private System.Windows.Forms.Label labelRunTimeHeader;
        private System.Windows.Forms.Label labelFrameCountHeader;
        private System.Windows.Forms.Label labelFrameCount;
    }
}

