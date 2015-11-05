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
            this.txtModelDir = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdClassify = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOldImageDirectory = new System.Windows.Forms.TextBox();
            this.chkClassify = new System.Windows.Forms.CheckBox();
            this.chkAddImagesToDataBase = new System.Windows.Forms.CheckBox();
            this.chkIncludeSubDirectories = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNewImageDirectory = new System.Windows.Forms.TextBox();
            this.chkMoveImages = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtModelDir
            // 
            this.txtModelDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtModelDir.Location = new System.Drawing.Point(184, 118);
            this.txtModelDir.Name = "txtModelDir";
            this.txtModelDir.Size = new System.Drawing.Size(516, 26);
            this.txtModelDir.TabIndex = 0;
            this.txtModelDir.Text = "F:/Models/F:/Models/20151104-162316-0a98_epoch_10.0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Model Directory:";
            // 
            // cmdClassify
            // 
            this.cmdClassify.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClassify.Location = new System.Drawing.Point(552, 185);
            this.cmdClassify.Name = "cmdClassify";
            this.cmdClassify.Size = new System.Drawing.Size(148, 69);
            this.cmdClassify.TabIndex = 2;
            this.cmdClassify.Text = "Classify";
            this.cmdClassify.UseVisualStyleBackColor = true;
            this.cmdClassify.Click += new System.EventHandler(this.cmdClassify_Click_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Old Image Directory:";
            // 
            // txtOldImageDirectory
            // 
            this.txtOldImageDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOldImageDirectory.Location = new System.Drawing.Point(184, 45);
            this.txtOldImageDirectory.Name = "txtOldImageDirectory";
            this.txtOldImageDirectory.Size = new System.Drawing.Size(516, 26);
            this.txtOldImageDirectory.TabIndex = 3;
            this.txtOldImageDirectory.Text = "F:/New8/Crops/heads";
            // 
            // chkClassify
            // 
            this.chkClassify.AutoSize = true;
            this.chkClassify.Checked = true;
            this.chkClassify.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkClassify.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkClassify.Location = new System.Drawing.Point(17, 185);
            this.chkClassify.Name = "chkClassify";
            this.chkClassify.Size = new System.Drawing.Size(82, 24);
            this.chkClassify.TabIndex = 5;
            this.chkClassify.Text = "Classify";
            this.chkClassify.UseVisualStyleBackColor = true;
            // 
            // chkAddImagesToDataBase
            // 
            this.chkAddImagesToDataBase.AutoSize = true;
            this.chkAddImagesToDataBase.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAddImagesToDataBase.Location = new System.Drawing.Point(17, 275);
            this.chkAddImagesToDataBase.Name = "chkAddImagesToDataBase";
            this.chkAddImagesToDataBase.Size = new System.Drawing.Size(208, 24);
            this.chkAddImagesToDataBase.TabIndex = 6;
            this.chkAddImagesToDataBase.Text = "Add Images to DataBase";
            this.chkAddImagesToDataBase.UseVisualStyleBackColor = true;
            // 
            // chkIncludeSubDirectories
            // 
            this.chkIncludeSubDirectories.AutoSize = true;
            this.chkIncludeSubDirectories.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIncludeSubDirectories.Location = new System.Drawing.Point(17, 215);
            this.chkIncludeSubDirectories.Name = "chkIncludeSubDirectories";
            this.chkIncludeSubDirectories.Size = new System.Drawing.Size(189, 24);
            this.chkIncludeSubDirectories.TabIndex = 7;
            this.chkIncludeSubDirectories.Text = "Include SubDirectories";
            this.chkIncludeSubDirectories.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(13, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(160, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "New Image Directory:";
            // 
            // txtNewImageDirectory
            // 
            this.txtNewImageDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNewImageDirectory.Location = new System.Drawing.Point(184, 77);
            this.txtNewImageDirectory.Name = "txtNewImageDirectory";
            this.txtNewImageDirectory.Size = new System.Drawing.Size(516, 26);
            this.txtNewImageDirectory.TabIndex = 8;
            this.txtNewImageDirectory.Text = "F:/New7/Crops";
            // 
            // chkMoveImages
            // 
            this.chkMoveImages.AutoSize = true;
            this.chkMoveImages.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMoveImages.Location = new System.Drawing.Point(17, 245);
            this.chkMoveImages.Name = "chkMoveImages";
            this.chkMoveImages.Size = new System.Drawing.Size(123, 24);
            this.chkMoveImages.TabIndex = 10;
            this.chkMoveImages.Text = "Move Images";
            this.chkMoveImages.UseVisualStyleBackColor = true;
            // 
            // frmClassifyImages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 388);
            this.Controls.Add(this.chkMoveImages);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtNewImageDirectory);
            this.Controls.Add(this.chkIncludeSubDirectories);
            this.Controls.Add(this.chkAddImagesToDataBase);
            this.Controls.Add(this.chkClassify);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtOldImageDirectory);
            this.Controls.Add(this.cmdClassify);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtModelDir);
            this.Name = "frmClassifyImages";
            this.Text = "Classify Images";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtModelDir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdClassify;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOldImageDirectory;
        private System.Windows.Forms.CheckBox chkClassify;
        private System.Windows.Forms.CheckBox chkAddImagesToDataBase;
        private System.Windows.Forms.CheckBox chkIncludeSubDirectories;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNewImageDirectory;
        private System.Windows.Forms.CheckBox chkMoveImages;
    }
}