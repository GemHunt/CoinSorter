namespace ImageClassifier
{
    partial class frmAugmentImages
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
            this.label4 = new System.Windows.Forms.Label();
            this.txtImageDirectory = new System.Windows.Forms.TextBox();
            this.cmdAugment = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "Image Directory:";
            // 
            // txtImageDirectory
            // 
            this.txtImageDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtImageDirectory.Location = new System.Drawing.Point(168, 117);
            this.txtImageDirectory.Name = "txtImageDirectory";
            this.txtImageDirectory.Size = new System.Drawing.Size(453, 26);
            this.txtImageDirectory.TabIndex = 10;
            this.txtImageDirectory.Text = "F:/New5";
            // 
            // cmdAugment
            // 
            this.cmdAugment.Location = new System.Drawing.Point(473, 165);
            this.cmdAugment.Name = "cmdAugment";
            this.cmdAugment.Size = new System.Drawing.Size(148, 31);
            this.cmdAugment.TabIndex = 12;
            this.cmdAugment.Text = "Augment";
            this.cmdAugment.UseVisualStyleBackColor = true;
            this.cmdAugment.Click += new System.EventHandler(this.cmdAugment_Click);
            // 
            // frmAugmentImages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 261);
            this.Controls.Add(this.cmdAugment);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtImageDirectory);
            this.Name = "frmAugmentImages";
            this.Text = "frmAugmentImages";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtImageDirectory;
        private System.Windows.Forms.Button cmdAugment;
    }
}