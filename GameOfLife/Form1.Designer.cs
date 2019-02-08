namespace GameOfLife
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
            this.btnStart = new System.Windows.Forms.Button();
            this.picGameOfLife = new System.Windows.Forms.PictureBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStep = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picGameOfLife)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(836, 168);
            this.btnStart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // picGameOfLife
            // 
            this.picGameOfLife.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.picGameOfLife.Location = new System.Drawing.Point(13, 27);
            this.picGameOfLife.Margin = new System.Windows.Forms.Padding(4);
            this.picGameOfLife.Name = "picGameOfLife";
            this.picGameOfLife.Size = new System.Drawing.Size(746, 700);
            this.picGameOfLife.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picGameOfLife.TabIndex = 2;
            this.picGameOfLife.TabStop = false;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(836, 310);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "Pause";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStep
            // 
            this.btnStep.Location = new System.Drawing.Point(836, 239);
            this.btnStep.Name = "btnStep";
            this.btnStep.Size = new System.Drawing.Size(75, 23);
            this.btnStep.TabIndex = 4;
            this.btnStep.Text = "Step";
            this.btnStep.UseVisualStyleBackColor = true;
            this.btnStep.Click += new System.EventHandler(this.btnStep_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(836, 392);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 5;
            this.btnTest.Text = "Manual";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 761);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnStep);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.picGameOfLife);
            this.Controls.Add(this.btnStart);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.picGameOfLife)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.PictureBox picGameOfLife;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStep;
        private System.Windows.Forms.Button btnTest;
    }
}

