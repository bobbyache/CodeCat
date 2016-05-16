namespace CygSoft.CodeCat.UI.WinForms
{
    partial class SnippetForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SnippetForm));
            this.syntaxDoc = new Alsing.SourceCode.SyntaxDocument(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cboFontSize = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.lblEditStatus = new System.Windows.Forms.ToolStripLabel();
            this.txtIdentifier = new CygSoft.CodeCat.UI.WinForms.ToolStripSpringTextBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtTitle = new CygSoft.CodeCat.UI.WinForms.ToolStripSpringTextBox();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.txtKeywords = new CygSoft.CodeCat.UI.WinForms.ToolStripSpringTextBox();
            this.toolStrip4 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageCode = new System.Windows.Forms.TabPage();
            this.syntaxBox = new Alsing.Windows.Forms.SyntaxBoxControl();
            this.tabPageSnapshots = new System.Windows.Forms.TabPage();
            this.snapshotListCtrl1 = new CygSoft.CodeCat.UI.WinForms.SnapshotListCtrl();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.toolStrip4.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageCode.SuspendLayout();
            this.tabPageSnapshots.SuspendLayout();
            this.SuspendLayout();
            // 
            // syntaxDoc
            // 
            this.syntaxDoc.Lines = new string[] {
        ""};
            this.syntaxDoc.MaxUndoBufferSize = 1000;
            this.syntaxDoc.Modified = false;
            this.syntaxDoc.UndoStep = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cboFontSize,
            this.toolStripLabel2,
            this.toolStripLabel3,
            this.lblEditStatus,
            this.txtIdentifier});
            this.toolStrip1.Location = new System.Drawing.Point(0, 471);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(821, 25);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
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
            // toolStripLabel2
            // 
            this.toolStripLabel2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(25, 22);
            this.toolStripLabel2.Text = "INS";
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(36, 22);
            this.toolStripLabel3.Text = "CAPS";
            // 
            // lblEditStatus
            // 
            this.lblEditStatus.Name = "lblEditStatus";
            this.lblEditStatus.Size = new System.Drawing.Size(72, 22);
            this.lblEditStatus.Text = "No Changes";
            // 
            // txtIdentifier
            // 
            this.txtIdentifier.Name = "txtIdentifier";
            this.txtIdentifier.Size = new System.Drawing.Size(568, 25);
            this.txtIdentifier.Text = "fc61a31d-b0dd-4b91-8c7a-9a8125d8c4bc";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.txtTitle});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(821, 25);
            this.toolStrip2.TabIndex = 10;
            this.toolStrip2.Text = "toolStrip2";
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
            this.txtTitle.Size = new System.Drawing.Size(713, 25);
            // 
            // toolStrip3
            // 
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel4,
            this.txtKeywords});
            this.toolStrip3.Location = new System.Drawing.Point(0, 25);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(821, 25);
            this.toolStrip3.TabIndex = 11;
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.AutoSize = false;
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(65, 22);
            this.toolStripLabel4.Text = "Keywords";
            this.toolStripLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtKeywords
            // 
            this.txtKeywords.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtKeywords.Name = "txtKeywords";
            this.txtKeywords.Size = new System.Drawing.Size(713, 25);
            // 
            // toolStrip4
            // 
            this.toolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStrip4.Location = new System.Drawing.Point(0, 50);
            this.toolStrip4.Name = "toolStrip4";
            this.toolStrip4.Size = new System.Drawing.Size(821, 25);
            this.toolStrip4.TabIndex = 12;
            this.toolStrip4.Text = "toolStrip4";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(104, 22);
            this.toolStripButton1.Text = "Take Snapshot";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(112, 22);
            this.toolStripButton2.Text = "Delete Snapshot";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageCode);
            this.tabControl.Controls.Add(this.tabPageSnapshots);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 75);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(821, 396);
            this.tabControl.TabIndex = 13;
            // 
            // tabPageCode
            // 
            this.tabPageCode.Controls.Add(this.syntaxBox);
            this.tabPageCode.Location = new System.Drawing.Point(4, 22);
            this.tabPageCode.Name = "tabPageCode";
            this.tabPageCode.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCode.Size = new System.Drawing.Size(813, 370);
            this.tabPageCode.TabIndex = 0;
            this.tabPageCode.Text = "Code";
            this.tabPageCode.UseVisualStyleBackColor = true;
            // 
            // syntaxBox
            // 
            this.syntaxBox.ActiveView = Alsing.Windows.Forms.ActiveView.BottomRight;
            this.syntaxBox.AutoListPosition = null;
            this.syntaxBox.AutoListSelectedText = "a123";
            this.syntaxBox.AutoListVisible = false;
            this.syntaxBox.BackColor = System.Drawing.Color.White;
            this.syntaxBox.BorderStyle = Alsing.Windows.Forms.BorderStyle.None;
            this.syntaxBox.CopyAsRTF = false;
            this.syntaxBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.syntaxBox.Document = this.syntaxDoc;
            this.syntaxBox.FontName = "Courier new";
            this.syntaxBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.syntaxBox.InfoTipCount = 1;
            this.syntaxBox.InfoTipPosition = null;
            this.syntaxBox.InfoTipSelectedIndex = 1;
            this.syntaxBox.InfoTipVisible = false;
            this.syntaxBox.Location = new System.Drawing.Point(3, 3);
            this.syntaxBox.LockCursorUpdate = false;
            this.syntaxBox.Name = "syntaxBox";
            this.syntaxBox.ShowScopeIndicator = false;
            this.syntaxBox.Size = new System.Drawing.Size(807, 364);
            this.syntaxBox.SmoothScroll = false;
            this.syntaxBox.SplitviewH = -4;
            this.syntaxBox.SplitviewV = -4;
            this.syntaxBox.TabGuideColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(233)))), ((int)(((byte)(233)))));
            this.syntaxBox.TabIndex = 2;
            this.syntaxBox.Text = "syntaxBoxControl1";
            this.syntaxBox.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
            // 
            // tabPageSnapshots
            // 
            this.tabPageSnapshots.Controls.Add(this.snapshotListCtrl1);
            this.tabPageSnapshots.Location = new System.Drawing.Point(4, 22);
            this.tabPageSnapshots.Name = "tabPageSnapshots";
            this.tabPageSnapshots.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSnapshots.Size = new System.Drawing.Size(813, 370);
            this.tabPageSnapshots.TabIndex = 1;
            this.tabPageSnapshots.Text = "Snapshots";
            this.tabPageSnapshots.UseVisualStyleBackColor = true;
            // 
            // snapshotListCtrl1
            // 
            this.snapshotListCtrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.snapshotListCtrl1.Location = new System.Drawing.Point(3, 3);
            this.snapshotListCtrl1.Name = "snapshotListCtrl1";
            this.snapshotListCtrl1.Size = new System.Drawing.Size(807, 364);
            this.snapshotListCtrl1.TabIndex = 0;
            // 
            // SnippetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 496);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.toolStrip4);
            this.Controls.Add(this.toolStrip3);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "SnippetForm";
            this.Text = "Form4";
            this.Activated += new System.EventHandler(this.SnippetForm_Activated);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.toolStrip4.ResumeLayout(false);
            this.toolStrip4.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPageCode.ResumeLayout(false);
            this.tabPageSnapshots.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Alsing.SourceCode.SyntaxDocument syntaxDoc;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox cboFontSize;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private CygSoft.CodeCat.UI.WinForms.ToolStripSpringTextBox txtTitle;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private CygSoft.CodeCat.UI.WinForms.ToolStripSpringTextBox txtKeywords;
        private System.Windows.Forms.ToolStrip toolStrip4;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageCode;
        private Alsing.Windows.Forms.SyntaxBoxControl syntaxBox;
        private System.Windows.Forms.TabPage tabPageSnapshots;
        private System.Windows.Forms.ToolStripLabel lblEditStatus;
        private CygSoft.CodeCat.UI.WinForms.ToolStripSpringTextBox txtIdentifier;
        private SnapshotListCtrl snapshotListCtrl1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
    }
}