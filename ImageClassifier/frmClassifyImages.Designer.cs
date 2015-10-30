namespace ImageClassifier
{
    partial class frmClassifyImages
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
            this.txtModel = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdClassify = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtModel
            // 
            this.txtModel.Location = new System.Drawing.Point(12, 93);
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(422, 20);
            this.txtModel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Model:";
            // 
            // cmdClassify
            // 
            this.cmdClassify.Location = new System.Drawing.Point(485, 31);
            this.cmdClassify.Name = "cmdClassify";
            this.cmdClassify.Size = new System.Drawing.Size(80, 44);
            this.cmdClassify.TabIndex = 2;
            this.cmdClassify.Text = "Classify";
            this.cmdClassify.UseVisualStyleBackColor = true;
            this.cmdClassify.Click += new System.EventHandler(this.cmdClassify_Click_1);
            // 
            // frmClassifyImages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 395);
            this.Controls.Add(this.cmdClassify);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtModel);
            this.Name = "frmClassifyImages";
            this.Text = "Classify Images";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdClassify;
    }
}