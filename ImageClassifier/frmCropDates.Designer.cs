namespace ImageClassifier
{
    partial class frmCropDates
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
            this.cmdCropDates = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtImageDirectory = new System.Windows.Forms.TextBox();
            this.chkAugment = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cmdCropDates
            // 
            this.cmdCropDates.Location = new System.Drawing.Point(489, 156);
            this.cmdCropDates.Name = "cmdCropDates";
            this.cmdCropDates.Size = new System.Drawing.Size(148, 31);
            this.cmdCropDates.TabIndex = 17;
            this.cmdCropDates.Text = "Crop Dates";
            this.cmdCropDates.UseVisualStyleBackColor = true;
            this.cmdCropDates.Click += new System.EventHandler(this.cmdCropDates_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(18, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 20);
            this.label4.TabIndex = 16;
            this.label4.Text = "Image Directory:";
            // 
            // txtImageDirectory
            // 
            this.txtImageDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtImageDirectory.Location = new System.Drawing.Point(184, 104);
            this.txtImageDirectory.Name = "txtImageDirectory";
            this.txtImageDirectory.Size = new System.Drawing.Size(453, 26);
            this.txtImageDirectory.TabIndex = 15;
            this.txtImageDirectory.Text = "F:\\NewRot";
            // 
            // chkAugment
            // 
            this.chkAugment.AutoSize = true;
            this.chkAugment.Checked = true;
            this.chkAugment.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAugment.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAugment.Location = new System.Drawing.Point(22, 156);
            this.chkAugment.Name = "chkAugment";
            this.chkAugment.Size = new System.Drawing.Size(93, 24);
            this.chkAugment.TabIndex = 18;
            this.chkAugment.Text = "Augment";
            this.chkAugment.UseVisualStyleBackColor = true;
            // 
            // frmCropDates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 209);
            this.Controls.Add(this.chkAugment);
            this.Controls.Add(this.cmdCropDates);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtImageDirectory);
            this.Name = "frmCropDates";
            this.Text = "CropDates";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdCropDates;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtImageDirectory;
        private System.Windows.Forms.CheckBox chkAugment;
    }
}