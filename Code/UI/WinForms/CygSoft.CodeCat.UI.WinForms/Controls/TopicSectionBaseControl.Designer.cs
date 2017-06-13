namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    partial class TopicSectionBaseControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.HeaderToolstrip = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtTitle = new CygSoft.CodeCat.UI.WinForms.ToolStripSpringTextBox();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.cboSyntax = new System.Windows.Forms.ToolStripComboBox();
            this.FooterToolstrip = new System.Windows.Forms.ToolStrip();
            this.cboFontSize = new System.Windows.Forms.ToolStripComboBox();
            this.lblEditStatus = new System.Windows.Forms.ToolStripLabel();
            this.HeaderToolstrip.SuspendLayout();
            this.FooterToolstrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // HeaderToolstrip
            // 
            this.HeaderToolstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.txtTitle,
            this.toolStripLabel5,
            this.cboSyntax});
            this.HeaderToolstrip.Location = new System.Drawing.Point(0, 0);
            this.HeaderToolstrip.Name = "HeaderToolstrip";
            this.HeaderToolstrip.Size = new System.Drawing.Size(861, 25);
            this.HeaderToolstrip.TabIndex = 1;
            this.HeaderToolstrip.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.AutoSize = false;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(65, 22);
            this.toolStripLabel1.Text = "Title";
            this.toolStripLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTitle
            // 
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(589, 25);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(41, 22);
            this.toolStripLabel5.Text = "Syntax";
            // 
            // cboSyntax
            // 
            this.cboSyntax.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSyntax.Name = "cboSyntax";
            this.cboSyntax.Size = new System.Drawing.Size(121, 25);
            // 
            // FooterToolstrip
            // 
            this.FooterToolstrip.AutoSize = false;
            this.FooterToolstrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.FooterToolstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cboFontSize,
            this.lblEditStatus});
            this.FooterToolstrip.Location = new System.Drawing.Point(0, 508);
            this.FooterToolstrip.Name = "FooterToolstrip";
            this.FooterToolstrip.Size = new System.Drawing.Size(861, 25);
            this.FooterToolstrip.TabIndex = 6;
            this.FooterToolstrip.Text = "toolStrip2";
            // 
            // cboFontSize
            // 
            this.cboFontSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFontSize.Items.AddRange(new object[] {
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16"});
            this.cboFontSize.Name = "cboFontSize";
            this.cboFontSize.Size = new System.Drawing.Size(75, 25);
            // 
            // lblEditStatus
            // 
            this.lblEditStatus.AutoSize = false;
            this.lblEditStatus.Name = "lblEditStatus";
            this.lblEditStatus.Size = new System.Drawing.Size(100, 22);
            this.lblEditStatus.Text = "No Changes";
            this.lblEditStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TopicSectionBaseControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.FooterToolstrip);
            this.Controls.Add(this.HeaderToolstrip);
            this.Name = "TopicSectionBaseControl";
            this.Size = new System.Drawing.Size(861, 533);
            this.HeaderToolstrip.ResumeLayout(false);
            this.HeaderToolstrip.PerformLayout();
            this.FooterToolstrip.ResumeLayout(false);
            this.FooterToolstrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        protected System.Windows.Forms.ToolStrip FooterToolstrip;
        protected System.Windows.Forms.ToolStrip HeaderToolstrip;
        protected System.Windows.Forms.ToolStripLabel lblEditStatus;
        private System.Windows.Forms.ToolStripComboBox cboFontSize;
        private System.Windows.Forms.ToolStripComboBox cboSyntax;
        private ToolStripSpringTextBox txtTitle;
    }
}
