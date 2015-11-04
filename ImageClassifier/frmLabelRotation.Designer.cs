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
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxCoin)).BeginInit();
            this.SuspendLayout();
            // 
            // PictureBoxCoin
            // 
            this.PictureBoxCoin.Location = new System.Drawing.Point(12, 12);
            this.PictureBoxCoin.Name = "PictureBoxCoin";
            this.PictureBoxCoin.Size = new System.Drawing.Size(406, 406);
            this.PictureBoxCoin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.PictureBoxCoin.TabIndex = 1;
            this.PictureBoxCoin.TabStop = false;
            this.PictureBoxCoin.Click += new System.EventHandler(this.PictureBoxCoin_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 433);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Click on the top of the L in LIBERTY.";
            // 
            // frmLabelRotation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 461);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PictureBoxCoin);
            this.Name = "frmLabelRotation";
            this.Text = "Label Rotation";
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxCoin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.PictureBox PictureBoxCoin;
        private System.Windows.Forms.Label label1;
    }
}