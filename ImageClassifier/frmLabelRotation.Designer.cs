namespace ImageClassifier
{
    partial class frmLabelRotation
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
            this.PictureBoxCoin = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxCoin)).BeginInit();
            this.SuspendLayout();
            // 
            // PictureBoxCoin
            // 
            this.PictureBoxCoin.Location = new System.Drawing.Point(12, 12);
            this.PictureBoxCoin.Name = "PictureBoxCoin";
            this.PictureBoxCoin.Size = new System.Drawing.Size(256, 256);
            this.PictureBoxCoin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.PictureBoxCoin.TabIndex = 1;
            this.PictureBoxCoin.TabStop = false;
            this.PictureBoxCoin.Click += new System.EventHandler(this.PictureBoxCoin_Click);
            // 
            // frmLabelRotation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 283);
            this.Controls.Add(this.PictureBoxCoin);
            this.Name = "frmLabelRotation";
            this.Text = "frmLabelRot";
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxCoin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.PictureBox PictureBoxCoin;
    }
}