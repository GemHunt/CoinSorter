namespace ImageClassifier
{
    partial class frmLabel
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
            this.listBoxLabelAllShown = new System.Windows.Forms.ListBox();
            this.txtMainImageDirectory = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdLabelAll = new System.Windows.Forms.Button();
            this.listBoxWorkingLabel = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listBoxClickList = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmdReadDirectoryAndRefresh = new System.Windows.Forms.Button();
            this.groupBoxImages = new System.Windows.Forms.GroupBox();
            this.cmdGetMore = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxLabelAllShown
            // 
            this.listBoxLabelAllShown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxLabelAllShown.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxLabelAllShown.FormattingEnabled = true;
            this.listBoxLabelAllShown.ItemHeight = 20;
            this.listBoxLabelAllShown.Location = new System.Drawing.Point(12, 639);
            this.listBoxLabelAllShown.Name = "listBoxLabelAllShown";
            this.listBoxLabelAllShown.Size = new System.Drawing.Size(134, 264);
            this.listBoxLabelAllShown.TabIndex = 0;
            // 
            // txtMainImageDirectory
            // 
            this.txtMainImageDirectory.Location = new System.Drawing.Point(288, 22);
            this.txtMainImageDirectory.Name = "txtMainImageDirectory";
            this.txtMainImageDirectory.Size = new System.Drawing.Size(657, 20);
            this.txtMainImageDirectory.TabIndex = 1;
            this.txtMainImageDirectory.Text = "F:\\new6\\crops";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(172, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Main Image Directory:";
            // 
            // cmdLabelAll
            // 
            this.cmdLabelAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdLabelAll.Location = new System.Drawing.Point(12, 922);
            this.cmdLabelAll.Name = "cmdLabelAll";
            this.cmdLabelAll.Size = new System.Drawing.Size(134, 36);
            this.cmdLabelAll.TabIndex = 3;
            this.cmdLabelAll.Text = "Label All       \'A\' Key";
            this.cmdLabelAll.UseVisualStyleBackColor = true;
            this.cmdLabelAll.Click += new System.EventHandler(this.cmdLabelAll_Click);
            // 
            // listBoxWorkingLabel
            // 
            this.listBoxWorkingLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxWorkingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxWorkingLabel.FormattingEnabled = true;
            this.listBoxWorkingLabel.ItemHeight = 20;
            this.listBoxWorkingLabel.Location = new System.Drawing.Point(12, 25);
            this.listBoxWorkingLabel.Name = "listBoxWorkingLabel";
            this.listBoxWorkingLabel.Size = new System.Drawing.Size(134, 264);
            this.listBoxWorkingLabel.TabIndex = 4;
            this.listBoxWorkingLabel.SelectedIndexChanged += new System.EventHandler(this.listBoxWorkingLabel_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 623);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Label All Shown As:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Working Label:";
            // 
            // listBoxClickList
            // 
            this.listBoxClickList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxClickList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxClickList.FormattingEnabled = true;
            this.listBoxClickList.ItemHeight = 20;
            this.listBoxClickList.Location = new System.Drawing.Point(12, 334);
            this.listBoxClickList.Name = "listBoxClickList";
            this.listBoxClickList.Size = new System.Drawing.Size(134, 264);
            this.listBoxClickList.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 318);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Click to Label As:";
            // 
            // cmdReadDirectoryAndRefresh
            // 
            this.cmdReadDirectoryAndRefresh.Location = new System.Drawing.Point(951, 6);
            this.cmdReadDirectoryAndRefresh.Name = "cmdReadDirectoryAndRefresh";
            this.cmdReadDirectoryAndRefresh.Size = new System.Drawing.Size(111, 36);
            this.cmdReadDirectoryAndRefresh.TabIndex = 7;
            this.cmdReadDirectoryAndRefresh.Text = "Read Directory And Refresh Labels";
            this.cmdReadDirectoryAndRefresh.UseVisualStyleBackColor = true;
            this.cmdReadDirectoryAndRefresh.Click += new System.EventHandler(this.cmdReadDirectoryAndRefresh_Click);
            // 
            // groupBoxImages
            // 
            this.groupBoxImages.Location = new System.Drawing.Point(152, 48);
            this.groupBoxImages.Name = "groupBoxImages";
            this.groupBoxImages.Size = new System.Drawing.Size(1100, 910);
            this.groupBoxImages.TabIndex = 8;
            this.groupBoxImages.TabStop = false;
            // 
            // cmdGetMore
            // 
            this.cmdGetMore.Location = new System.Drawing.Point(1068, 6);
            this.cmdGetMore.Name = "cmdGetMore";
            this.cmdGetMore.Size = new System.Drawing.Size(111, 36);
            this.cmdGetMore.TabIndex = 9;
            this.cmdGetMore.Text = "Get More";
            this.cmdGetMore.UseVisualStyleBackColor = true;
            this.cmdGetMore.Click += new System.EventHandler(this.cmdGetMore_Click);
            // 
            // frmLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 961);
            this.Controls.Add(this.cmdGetMore);
            this.Controls.Add(this.listBoxWorkingLabel);
            this.Controls.Add(this.groupBoxImages);
            this.Controls.Add(this.cmdReadDirectoryAndRefresh);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listBoxClickList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmdLabelAll);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMainImageDirectory);
            this.Controls.Add(this.listBoxLabelAllShown);
            this.Name = "frmLabel";
            this.Text = "Image Labeling Wizard";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxLabelAllShown;
        private System.Windows.Forms.TextBox txtMainImageDirectory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdLabelAll;
        private System.Windows.Forms.ListBox listBoxWorkingLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBoxClickList;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button cmdReadDirectoryAndRefresh;
        private System.Windows.Forms.GroupBox groupBoxImages;
        private System.Windows.Forms.Button cmdGetMore;

    }
}