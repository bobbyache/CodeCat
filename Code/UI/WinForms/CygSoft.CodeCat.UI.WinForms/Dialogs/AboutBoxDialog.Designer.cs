namespace CygSoft.CodeCat.UI.WinForms
{
    partial class AboutBoxDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBoxDialog));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabInfo = new System.Windows.Forms.TabPage();
            this.tabLicense = new System.Windows.Forms.TabPage();
            this.tabCredits = new System.Windows.Forms.TabPage();
            this.txtLicense = new System.Windows.Forms.TextBox();
            this.rtfCredits = new System.Windows.Forms.RichTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.labelCopyright = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelProductName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabInfo.SuspendLayout();
            this.tabLicense.SuspendLayout();
            this.tabCredits.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabInfo);
            this.tabControl1.Controls.Add(this.tabLicense);
            this.tabControl1.Controls.Add(this.tabCredits);
            this.tabControl1.Location = new System.Drawing.Point(2, 6);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(569, 623);
            this.tabControl1.TabIndex = 5;
            // 
            // tabInfo
            // 
            this.tabInfo.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabInfo.Controls.Add(this.labelCopyright);
            this.tabInfo.Controls.Add(this.labelVersion);
            this.tabInfo.Controls.Add(this.labelProductName);
            this.tabInfo.Controls.Add(this.label1);
            this.tabInfo.Controls.Add(this.pictureBox1);
            this.tabInfo.Controls.Add(this.pictureBox2);
            this.tabInfo.Location = new System.Drawing.Point(4, 22);
            this.tabInfo.Name = "tabInfo";
            this.tabInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabInfo.Size = new System.Drawing.Size(561, 597);
            this.tabInfo.TabIndex = 0;
            this.tabInfo.Text = "Info";
            // 
            // tabLicense
            // 
            this.tabLicense.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabLicense.Controls.Add(this.txtLicense);
            this.tabLicense.Location = new System.Drawing.Point(4, 22);
            this.tabLicense.Name = "tabLicense";
            this.tabLicense.Padding = new System.Windows.Forms.Padding(3);
            this.tabLicense.Size = new System.Drawing.Size(557, 597);
            this.tabLicense.TabIndex = 1;
            this.tabLicense.Text = "License";
            // 
            // tabCredits
            // 
            this.tabCredits.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabCredits.Controls.Add(this.rtfCredits);
            this.tabCredits.Location = new System.Drawing.Point(4, 22);
            this.tabCredits.Name = "tabCredits";
            this.tabCredits.Size = new System.Drawing.Size(557, 597);
            this.tabCredits.TabIndex = 2;
            this.tabCredits.Text = "Credits";
            // 
            // txtLicense
            // 
            this.txtLicense.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLicense.Location = new System.Drawing.Point(3, 3);
            this.txtLicense.Multiline = true;
            this.txtLicense.Name = "txtLicense";
            this.txtLicense.ReadOnly = true;
            this.txtLicense.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLicense.Size = new System.Drawing.Size(551, 591);
            this.txtLicense.TabIndex = 5;
            // 
            // rtfCredits
            // 
            this.rtfCredits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtfCredits.Location = new System.Drawing.Point(0, 0);
            this.rtfCredits.Name = "rtfCredits";
            this.rtfCredits.ReadOnly = true;
            this.rtfCredits.Size = new System.Drawing.Size(557, 597);
            this.rtfCredits.TabIndex = 4;
            this.rtfCredits.Text = "";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(165, 523);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(249, 68);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(6, 6);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(533, 507);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // labelCopyright
            // 
            this.labelCopyright.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.labelCopyright.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCopyright.ForeColor = System.Drawing.Color.Black;
            this.labelCopyright.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelCopyright.Location = new System.Drawing.Point(129, 128);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new System.Drawing.Size(214, 19);
            this.labelCopyright.TabIndex = 11;
            this.labelCopyright.Text = "Robbie Blake";
            this.labelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelVersion
            // 
            this.labelVersion.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.labelVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVersion.ForeColor = System.Drawing.Color.Black;
            this.labelVersion.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelVersion.Location = new System.Drawing.Point(129, 109);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(214, 19);
            this.labelVersion.TabIndex = 10;
            this.labelVersion.Text = "Robbie Blake";
            this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelProductName
            // 
            this.labelProductName.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.labelProductName.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProductName.ForeColor = System.Drawing.Color.Black;
            this.labelProductName.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelProductName.Location = new System.Drawing.Point(129, 62);
            this.labelProductName.Name = "labelProductName";
            this.labelProductName.Size = new System.Drawing.Size(214, 47);
            this.labelProductName.TabIndex = 9;
            this.labelProductName.Text = "Robbie Blake";
            this.labelProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(129, 147);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(214, 19);
            this.label1.TabIndex = 8;
            this.label1.Text = "Robbie Blake";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AboutBoxDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 630);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AboutBoxDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AboutBoxDialog";
            this.tabControl1.ResumeLayout(false);
            this.tabInfo.ResumeLayout(false);
            this.tabLicense.ResumeLayout(false);
            this.tabLicense.PerformLayout();
            this.tabCredits.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabInfo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TabPage tabLicense;
        private System.Windows.Forms.TextBox txtLicense;
        private System.Windows.Forms.TabPage tabCredits;
        private System.Windows.Forms.RichTextBox rtfCredits;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label labelCopyright;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelProductName;
        private System.Windows.Forms.Label label1;

    }
}