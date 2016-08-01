namespace CygSoft.CodeCat.UI.WinForms
{
    partial class SnippetDocument
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SnippetDocument));
            this.syntaxDoc = new Alsing.SourceCode.SyntaxDocument(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cboFontSize = new System.Windows.Forms.ToolStripComboBox();
            this.lblEditStatus = new System.Windows.Forms.ToolStripLabel();
            this.txtIdentifier = new CygSoft.CodeCat.UI.WinForms.ToolStripSpringTextBox();
            this.toolstripTitle = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtTitle = new CygSoft.CodeCat.UI.WinForms.ToolStripSpringTextBox();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.cboSyntax = new System.Windows.Forms.ToolStripComboBox();
            this.toolstripKeywords = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.txtKeywords = new CygSoft.CodeCat.UI.WinForms.ToolStripSpringTextBox();
            this.toolstripCommands = new System.Windows.Forms.ToolStrip();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnDiscardChange = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnTakeSnapshot = new System.Windows.Forms.ToolStripButton();
            this.btnDeleteSnapshot = new System.Windows.Forms.ToolStripButton();
            this.chkEdit = new System.Windows.Forms.ToolStripButton();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageCode = new System.Windows.Forms.TabPage();
            this.syntaxBox = new Alsing.Windows.Forms.SyntaxBoxControl();
            this.tabPageSnapshots = new System.Windows.Forms.TabPage();
            this.snapshotListCtrl1 = new CygSoft.CodeCat.UI.WinForms.SnapshotListCtrl();
            this.toolStrip1.SuspendLayout();
            this.toolstripTitle.SuspendLayout();
            this.toolstripKeywords.SuspendLayout();
            this.toolstripCommands.SuspendLayout();
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
            this.lblEditStatus,
            this.txtIdentifier});
            this.toolStrip1.Location = new System.Drawing.Point(0, 471);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(821, 25);
            this.toolStrip1.TabIndex = 4;
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
            // lblEditStatus
            // 
            this.lblEditStatus.AutoSize = false;
            this.lblEditStatus.Name = "lblEditStatus";
            this.lblEditStatus.Size = new System.Drawing.Size(100, 22);
            this.lblEditStatus.Text = "No Changes";
            this.lblEditStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIdentifier
            // 
            this.txtIdentifier.Name = "txtIdentifier";
            this.txtIdentifier.ReadOnly = true;
            this.txtIdentifier.Size = new System.Drawing.Size(601, 25);
            this.txtIdentifier.Text = "fc61a31d-b0dd-4b91-8c7a-9a8125d8c4bc";
            // 
            // toolstripTitle
            // 
            this.toolstripTitle.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.txtTitle,
            this.toolStripLabel5,
            this.cboSyntax});
            this.toolstripTitle.Location = new System.Drawing.Point(0, 50);
            this.toolstripTitle.Name = "toolstripTitle";
            this.toolstripTitle.Size = new System.Drawing.Size(821, 25);
            this.toolstripTitle.TabIndex = 2;
            this.toolstripTitle.TabStop = true;
            this.toolstripTitle.Text = "toolStrip2";
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
            this.txtTitle.Size = new System.Drawing.Size(549, 25);
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
            // toolstripKeywords
            // 
            this.toolstripKeywords.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel4,
            this.txtKeywords});
            this.toolstripKeywords.Location = new System.Drawing.Point(0, 25);
            this.toolstripKeywords.Name = "toolstripKeywords";
            this.toolstripKeywords.Size = new System.Drawing.Size(821, 25);
            this.toolstripKeywords.TabIndex = 1;
            this.toolstripKeywords.TabStop = true;
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
            // toolstripCommands
            // 
            this.toolstripCommands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSave,
            this.btnDiscardChange,
            this.toolStripSeparator1,
            this.btnDelete,
            this.toolStripSeparator2,
            this.btnTakeSnapshot,
            this.btnDeleteSnapshot,
            this.chkEdit});
            this.toolstripCommands.Location = new System.Drawing.Point(0, 0);
            this.toolstripCommands.Name = "toolstripCommands";
            this.toolstripCommands.Size = new System.Drawing.Size(821, 25);
            this.toolstripCommands.TabIndex = 0;
            this.toolstripCommands.Text = "toolStrip4";
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(23, 22);
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDiscardChange
            // 
            this.btnDiscardChange.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDiscardChange.Image = ((System.Drawing.Image)(resources.GetObject("btnDiscardChange.Image")));
            this.btnDiscardChange.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDiscardChange.Name = "btnDiscardChange";
            this.btnDiscardChange.Size = new System.Drawing.Size(23, 22);
            this.btnDiscardChange.Text = "Discard Changes";
            this.btnDiscardChange.Click += new System.EventHandler(this.btnDiscardChange_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnDelete
            // 
            this.btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(23, 22);
            this.btnDelete.Text = "Delete";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnTakeSnapshot
            // 
            this.btnTakeSnapshot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTakeSnapshot.Image = ((System.Drawing.Image)(resources.GetObject("btnTakeSnapshot.Image")));
            this.btnTakeSnapshot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTakeSnapshot.Name = "btnTakeSnapshot";
            this.btnTakeSnapshot.Size = new System.Drawing.Size(23, 22);
            this.btnTakeSnapshot.Text = "Take Snapshot";
            this.btnTakeSnapshot.Click += new System.EventHandler(this.btnTakeSnapshot_Click);
            // 
            // btnDeleteSnapshot
            // 
            this.btnDeleteSnapshot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDeleteSnapshot.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteSnapshot.Image")));
            this.btnDeleteSnapshot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteSnapshot.Name = "btnDeleteSnapshot";
            this.btnDeleteSnapshot.Size = new System.Drawing.Size(23, 22);
            this.btnDeleteSnapshot.Text = "Delete Snapshot";
            this.btnDeleteSnapshot.Click += new System.EventHandler(this.btnDeleteSnapshot_Click);
            // 
            // chkEdit
            // 
            this.chkEdit.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.chkEdit.CheckOnClick = true;
            this.chkEdit.Image = ((System.Drawing.Image)(resources.GetObject("chkEdit.Image")));
            this.chkEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.chkEdit.Name = "chkEdit";
            this.chkEdit.Size = new System.Drawing.Size(81, 22);
            this.chkEdit.Text = "Edit Mode";
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
            this.tabControl.TabIndex = 3;
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
            this.Controls.Add(this.toolstripTitle);
            this.Controls.Add(this.toolstripKeywords);
            this.Controls.Add(this.toolstripCommands);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "SnippetForm";
            this.Text = "Form4";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolstripTitle.ResumeLayout(false);
            this.toolstripTitle.PerformLayout();
            this.toolstripKeywords.ResumeLayout(false);
            this.toolstripKeywords.PerformLayout();
            this.toolstripCommands.ResumeLayout(false);
            this.toolstripCommands.PerformLayout();
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
        private System.Windows.Forms.ToolStrip toolstripTitle;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private CygSoft.CodeCat.UI.WinForms.ToolStripSpringTextBox txtTitle;
        private System.Windows.Forms.ToolStrip toolstripKeywords;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private CygSoft.CodeCat.UI.WinForms.ToolStripSpringTextBox txtKeywords;
        private System.Windows.Forms.ToolStrip toolstripCommands;
        private System.Windows.Forms.ToolStripButton btnTakeSnapshot;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageCode;
        private Alsing.Windows.Forms.SyntaxBoxControl syntaxBox;
        private System.Windows.Forms.TabPage tabPageSnapshots;
        private System.Windows.Forms.ToolStripLabel lblEditStatus;
        private CygSoft.CodeCat.UI.WinForms.ToolStripSpringTextBox txtIdentifier;
        private SnapshotListCtrl snapshotListCtrl1;
        private System.Windows.Forms.ToolStripButton btnDeleteSnapshot;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripComboBox cboSyntax;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton chkEdit;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnDiscardChange;
    }
}