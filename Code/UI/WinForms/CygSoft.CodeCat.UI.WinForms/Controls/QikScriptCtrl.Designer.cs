namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    partial class QikScriptCtrl
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
            this.components = new System.ComponentModel.Container();
            this.syntaxDocument = new Alsing.SourceCode.SyntaxDocument(this.components);
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.cboFontSize = new System.Windows.Forms.ToolStripComboBox();
            this.lblEditStatus = new System.Windows.Forms.ToolStripLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.syntaxBoxControl = new Alsing.Windows.Forms.SyntaxBoxControl();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // syntaxDocument
            // 
            this.syntaxDocument.Lines = new string[] {
        ""};
            this.syntaxDocument.MaxUndoBufferSize = 1000;
            this.syntaxDocument.Modified = false;
            this.syntaxDocument.UndoStep = 0;
            // 
            // toolStrip2
            // 
            this.toolStrip2.AutoSize = false;
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cboFontSize,
            this.lblEditStatus});
            this.toolStrip2.Location = new System.Drawing.Point(0, 439);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(595, 25);
            this.toolStrip2.TabIndex = 6;
            this.toolStrip2.Text = "toolStrip2";
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
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.syntaxBoxControl);
            this.splitContainer1.Size = new System.Drawing.Size(595, 439);
            this.splitContainer1.SplitterDistance = 340;
            this.splitContainer1.TabIndex = 7;
            // 
            // syntaxBoxControl
            // 
            this.syntaxBoxControl.ActiveView = Alsing.Windows.Forms.ActiveView.BottomRight;
            this.syntaxBoxControl.AutoListPosition = null;
            this.syntaxBoxControl.AutoListSelectedText = "a123";
            this.syntaxBoxControl.AutoListVisible = false;
            this.syntaxBoxControl.BackColor = System.Drawing.Color.White;
            this.syntaxBoxControl.BorderStyle = Alsing.Windows.Forms.BorderStyle.None;
            this.syntaxBoxControl.CopyAsRTF = false;
            this.syntaxBoxControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.syntaxBoxControl.Document = this.syntaxDocument;
            this.syntaxBoxControl.FontName = "Courier new";
            this.syntaxBoxControl.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.syntaxBoxControl.InfoTipCount = 1;
            this.syntaxBoxControl.InfoTipPosition = null;
            this.syntaxBoxControl.InfoTipSelectedIndex = 1;
            this.syntaxBoxControl.InfoTipVisible = false;
            this.syntaxBoxControl.Location = new System.Drawing.Point(0, 0);
            this.syntaxBoxControl.LockCursorUpdate = false;
            this.syntaxBoxControl.Name = "syntaxBoxControl";
            this.syntaxBoxControl.ShowScopeIndicator = false;
            this.syntaxBoxControl.Size = new System.Drawing.Size(595, 340);
            this.syntaxBoxControl.SmoothScroll = false;
            this.syntaxBoxControl.SplitviewH = -4;
            this.syntaxBoxControl.SplitviewV = -4;
            this.syntaxBoxControl.TabGuideColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(233)))), ((int)(((byte)(233)))));
            this.syntaxBoxControl.TabIndex = 0;
            this.syntaxBoxControl.Text = "syntaxBoxControl1";
            this.syntaxBoxControl.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
            // 
            // QikScriptCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip2);
            this.Name = "QikScriptCtrl";
            this.Size = new System.Drawing.Size(595, 464);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Alsing.SourceCode.SyntaxDocument syntaxDocument;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripComboBox cboFontSize;
        private System.Windows.Forms.ToolStripLabel lblEditStatus;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Alsing.Windows.Forms.SyntaxBoxControl syntaxBoxControl;
    }
}
