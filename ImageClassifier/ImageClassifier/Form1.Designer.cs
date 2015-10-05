namespace ImageClassifier
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
            this.button1 = new System.Windows.Forms.Button();
            this.cmdWebCam = new System.Windows.Forms.Button();
            this.cmdRead = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(133, 137);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmdWebCam
            // 
            this.cmdWebCam.Location = new System.Drawing.Point(294, 51);
            this.cmdWebCam.Name = "cmdWebCam";
            this.cmdWebCam.Size = new System.Drawing.Size(75, 23);
            this.cmdWebCam.TabIndex = 1;
            this.cmdWebCam.Text = "Web Cam";
            this.cmdWebCam.UseVisualStyleBackColor = true;
            this.cmdWebCam.Click += new System.EventHandler(this.cmdWebCam_Click);
            // 
            // cmdRead
            // 
            this.cmdRead.Location = new System.Drawing.Point(281, 126);
            this.cmdRead.Name = "cmdRead";
            this.cmdRead.Size = new System.Drawing.Size(96, 67);
            this.cmdRead.TabIndex = 2;
            this.cmdRead.Text = "Read Port";
            this.cmdRead.UseVisualStyleBackColor = true;
            this.cmdRead.Click += new System.EventHandler(this.cmdRead_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 245);
            this.Controls.Add(this.cmdRead);
            this.Controls.Add(this.cmdWebCam);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button cmdWebCam;
        private System.Windows.Forms.Button cmdRead;
    }
}

