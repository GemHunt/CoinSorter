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
            this.cmdLabelAll = new System.Windows.Forms.Button();
            this.listBoxWorkingLabel = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listBoxClickList = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmdGetImages = new System.Windows.Forms.Button();
            this.groupBoxImages = new System.Windows.Forms.GroupBox();
            this.cmdGetMore = new System.Windows.Forms.Button();
            this.radioLabelDesigns = new System.Windows.Forms.RadioButton();
            this.radioDates = new System.Windows.Forms.RadioButton();
            this.chkLabeled = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioYears = new System.Windows.Forms.RadioButton();
            this.radioDecades = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            // cmdGetImages
            // 
            this.cmdGetImages.Location = new System.Drawing.Point(951, 6);
            this.cmdGetImages.Name = "cmdGetImages";
            this.cmdGetImages.Size = new System.Drawing.Size(111, 36);
            this.cmdGetImages.TabIndex = 7;
            this.cmdGetImages.Text = "Get Images";
            this.cmdGetImages.UseVisualStyleBackColor = true;
            this.cmdGetImages.Click += new System.EventHandler(this.cmdGetImages_Click);
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
            this.cmdGetMore.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmdGetMore_KeyPress);
            // 
            // radioLabelDesigns
            // 
            this.radioLabelDesigns.AutoSize = true;
            this.radioLabelDesigns.Location = new System.Drawing.Point(37, 13);
            this.radioLabelDesigns.Name = "radioLabelDesigns";
            this.radioLabelDesigns.Size = new System.Drawing.Size(92, 17);
            this.radioLabelDesigns.TabIndex = 11;
            this.radioLabelDesigns.Text = "Label Designs";
            this.radioLabelDesigns.UseVisualStyleBackColor = true;
            // 
            // radioDates
            // 
            this.radioDates.AutoSize = true;
            this.radioDates.Checked = true;
            this.radioDates.Location = new System.Drawing.Point(135, 13);
            this.radioDates.Name = "radioDates";
            this.radioDates.Size = new System.Drawing.Size(82, 17);
            this.radioDates.TabIndex = 12;
            this.radioDates.TabStop = true;
            this.radioDates.Text = "Label Dates";
            this.radioDates.UseVisualStyleBackColor = true;
            // 
            // chkLabeled
            // 
            this.chkLabeled.AutoSize = true;
            this.chkLabeled.Checked = true;
            this.chkLabeled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLabeled.Location = new System.Drawing.Point(831, 17);
            this.chkLabeled.Name = "chkLabeled";
            this.chkLabeled.Size = new System.Drawing.Size(64, 17);
            this.chkLabeled.TabIndex = 13;
            this.chkLabeled.Text = "Labeled";
            this.chkLabeled.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioDates);
            this.groupBox1.Controls.Add(this.radioLabelDesigns);
            this.groupBox1.Location = new System.Drawing.Point(181, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(278, 37);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioYears);
            this.groupBox2.Controls.Add(this.radioDecades);
            this.groupBox2.Location = new System.Drawing.Point(475, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(278, 37);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            // 
            // radioYears
            // 
            this.radioYears.AutoSize = true;
            this.radioYears.Location = new System.Drawing.Point(135, 13);
            this.radioYears.Name = "radioYears";
            this.radioYears.Size = new System.Drawing.Size(52, 17);
            this.radioYears.TabIndex = 12;
            this.radioYears.Text = "Years";
            this.radioYears.UseVisualStyleBackColor = true;
            // 
            // radioDecades
            // 
            this.radioDecades.AutoSize = true;
            this.radioDecades.Checked = true;
            this.radioDecades.Location = new System.Drawing.Point(37, 13);
            this.radioDecades.Name = "radioDecades";
            this.radioDecades.Size = new System.Drawing.Size(68, 17);
            this.radioDecades.TabIndex = 11;
            this.radioDecades.TabStop = true;
            this.radioDecades.Tag = "";
            this.radioDecades.Text = "Decades";
            this.radioDecades.UseVisualStyleBackColor = true;
            // 
            // frmLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 961);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chkLabeled);
            this.Controls.Add(this.cmdGetMore);
            this.Controls.Add(this.listBoxWorkingLabel);
            this.Controls.Add(this.groupBoxImages);
            this.Controls.Add(this.cmdGetImages);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listBoxClickList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmdLabelAll);
            this.Controls.Add(this.listBoxLabelAllShown);
            this.Name = "frmLabel";
            this.Text = "Image Labeling Wizard";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxLabelAllShown;
        private System.Windows.Forms.Button cmdLabelAll;
        private System.Windows.Forms.ListBox listBoxWorkingLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBoxClickList;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button cmdGetImages;
        private System.Windows.Forms.GroupBox groupBoxImages;
        private System.Windows.Forms.Button cmdGetMore;
        private System.Windows.Forms.RadioButton radioLabelDesigns;
        private System.Windows.Forms.RadioButton radioDates;
        private System.Windows.Forms.CheckBox chkLabeled;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioYears;
        private System.Windows.Forms.RadioButton radioDecades;

    }
}