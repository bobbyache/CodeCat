namespace CygSoft.CodeCat.Plugins.SqlExtraction
{
    partial class SqlToCSharpStringGenerator
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnClear = new System.Windows.Forms.ToolStripButton();
            this.btnPartialFormat = new System.Windows.Forms.ToolStripButton();
            this.btnGenerate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuOptions = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuAutoCopyResult = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.fromTextbox = new Alsing.Windows.Forms.SyntaxBoxControl();
            this.fromDocument = new Alsing.SourceCode.SyntaxDocument(this.components);
            this.toTextbox = new Alsing.Windows.Forms.SyntaxBoxControl();
            this.toDocument = new Alsing.SourceCode.SyntaxDocument(this.components);
            this.mnuToggleOrientation = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClear,
            this.btnPartialFormat,
            this.btnGenerate,
            this.toolStripSeparator1,
            this.mnuOptions});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(493, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnClear
            // 
            this.btnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(38, 22);
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // btnPartialFormat
            // 
            this.btnPartialFormat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPartialFormat.Name = "btnPartialFormat";
            this.btnPartialFormat.Size = new System.Drawing.Size(49, 22);
            this.btnPartialFormat.Text = "Format";
            this.btnPartialFormat.Click += new System.EventHandler(this.BtnPartialFormat_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(58, 22);
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.Click += new System.EventHandler(this.BtnGenerate_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // mnuOptions
            // 
            this.mnuOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAutoCopyResult,
            this.mnuToggleOrientation});
            this.mnuOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuOptions.Name = "mnuOptions";
            this.mnuOptions.Size = new System.Drawing.Size(13, 22);
            this.mnuOptions.Text = "toolStripDropDownButton1";
            // 
            // mnuAutoCopyResult
            // 
            this.mnuAutoCopyResult.CheckOnClick = true;
            this.mnuAutoCopyResult.Name = "mnuAutoCopyResult";
            this.mnuAutoCopyResult.Size = new System.Drawing.Size(280, 22);
            this.mnuAutoCopyResult.Text = "Auto copy result to clipboard";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.fromTextbox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.toTextbox);
            this.splitContainer1.Size = new System.Drawing.Size(493, 406);
            this.splitContainer1.SplitterDistance = 203;
            this.splitContainer1.TabIndex = 4;
            // 
            // fromTextbox
            // 
            this.fromTextbox.ActiveView = Alsing.Windows.Forms.ActiveView.BottomRight;
            this.fromTextbox.AutoListPosition = null;
            this.fromTextbox.AutoListSelectedText = "a123";
            this.fromTextbox.AutoListVisible = false;
            this.fromTextbox.BackColor = System.Drawing.Color.White;
            this.fromTextbox.BorderStyle = Alsing.Windows.Forms.BorderStyle.None;
            this.fromTextbox.CopyAsRTF = false;
            this.fromTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fromTextbox.Document = this.fromDocument;
            this.fromTextbox.FontName = "Courier new";
            this.fromTextbox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.fromTextbox.InfoTipCount = 1;
            this.fromTextbox.InfoTipPosition = null;
            this.fromTextbox.InfoTipSelectedIndex = 1;
            this.fromTextbox.InfoTipVisible = false;
            this.fromTextbox.Location = new System.Drawing.Point(0, 0);
            this.fromTextbox.LockCursorUpdate = false;
            this.fromTextbox.Name = "fromTextbox";
            this.fromTextbox.ShowScopeIndicator = false;
            this.fromTextbox.Size = new System.Drawing.Size(493, 203);
            this.fromTextbox.SmoothScroll = false;
            this.fromTextbox.SplitviewH = -4;
            this.fromTextbox.SplitviewV = -4;
            this.fromTextbox.TabGuideColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(233)))), ((int)(((byte)(233)))));
            this.fromTextbox.TabIndex = 0;
            this.fromTextbox.Text = "syntaxBoxControl1";
            this.fromTextbox.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
            // 
            // fromDocument
            // 
            this.fromDocument.Lines = new string[] {
        ""};
            this.fromDocument.MaxUndoBufferSize = 1000;
            this.fromDocument.Modified = false;
            this.fromDocument.UndoStep = 0;
            // 
            // toTextbox
            // 
            this.toTextbox.ActiveView = Alsing.Windows.Forms.ActiveView.BottomRight;
            this.toTextbox.AutoListPosition = null;
            this.toTextbox.AutoListSelectedText = "a123";
            this.toTextbox.AutoListVisible = false;
            this.toTextbox.BackColor = System.Drawing.Color.White;
            this.toTextbox.BorderStyle = Alsing.Windows.Forms.BorderStyle.None;
            this.toTextbox.CopyAsRTF = false;
            this.toTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toTextbox.Document = this.toDocument;
            this.toTextbox.FontName = "Courier new";
            this.toTextbox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.toTextbox.InfoTipCount = 1;
            this.toTextbox.InfoTipPosition = null;
            this.toTextbox.InfoTipSelectedIndex = 1;
            this.toTextbox.InfoTipVisible = false;
            this.toTextbox.Location = new System.Drawing.Point(0, 0);
            this.toTextbox.LockCursorUpdate = false;
            this.toTextbox.Name = "toTextbox";
            this.toTextbox.ShowScopeIndicator = false;
            this.toTextbox.Size = new System.Drawing.Size(493, 199);
            this.toTextbox.SmoothScroll = false;
            this.toTextbox.SplitviewH = -4;
            this.toTextbox.SplitviewV = -4;
            this.toTextbox.TabGuideColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(233)))), ((int)(((byte)(233)))));
            this.toTextbox.TabIndex = 2;
            this.toTextbox.Text = "syntaxBoxControl2";
            this.toTextbox.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
            // 
            // toDocument
            // 
            this.toDocument.Lines = new string[] {
        ""};
            this.toDocument.MaxUndoBufferSize = 1000;
            this.toDocument.Modified = false;
            this.toDocument.UndoStep = 0;
            // 
            // mnuToggleOrientation
            // 
            this.mnuToggleOrientation.Name = "mnuToggleOrientation";
            this.mnuToggleOrientation.Size = new System.Drawing.Size(280, 22);
            this.mnuToggleOrientation.Text = "Toggle Right-to-Left or Top-to-Bottom";
            this.mnuToggleOrientation.Click += new System.EventHandler(this.mnuToggleOrientation_Click);
            // 
            // SqlToCSharpStringGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "SqlToCSharpStringGenerator";
            this.Size = new System.Drawing.Size(493, 431);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnGenerate;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Alsing.Windows.Forms.SyntaxBoxControl fromTextbox;
        private Alsing.SourceCode.SyntaxDocument fromDocument;
        private Alsing.Windows.Forms.SyntaxBoxControl toTextbox;
        private Alsing.SourceCode.SyntaxDocument toDocument;
        private System.Windows.Forms.ToolStripDropDownButton mnuOptions;
        private System.Windows.Forms.ToolStripMenuItem mnuAutoCopyResult;
        private System.Windows.Forms.ToolStripButton btnClear;
        private System.Windows.Forms.ToolStripButton btnPartialFormat;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuToggleOrientation;
    }
}
