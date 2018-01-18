namespace CygSoft.CodeCat.Plugins.ToSingleLine
{
    partial class ToSingleLineGenerator
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
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuOptions = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuAutoCopyResult = new System.Windows.Forms.ToolStripMenuItem();
            this.fromDocument = new Alsing.SourceCode.SyntaxDocument(this.components);
            this.fromTextbox = new Alsing.Windows.Forms.SyntaxBoxControl();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClear,
            this.toolStripSeparator1,
            this.mnuOptions});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(629, 25);
            this.toolStrip1.TabIndex = 7;
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // mnuOptions
            // 
            this.mnuOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAutoCopyResult});
            this.mnuOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuOptions.Name = "mnuOptions";
            this.mnuOptions.Size = new System.Drawing.Size(13, 22);
            this.mnuOptions.Text = "toolStripDropDownButton1";
            // 
            // mnuAutoCopyResult
            // 
            this.mnuAutoCopyResult.CheckOnClick = true;
            this.mnuAutoCopyResult.Name = "mnuAutoCopyResult";
            this.mnuAutoCopyResult.Size = new System.Drawing.Size(228, 22);
            this.mnuAutoCopyResult.Text = "Auto copy result to clipboard";
            // 
            // fromDocument
            // 
            this.fromDocument.Lines = new string[] {
        ""};
            this.fromDocument.MaxUndoBufferSize = 1000;
            this.fromDocument.Modified = false;
            this.fromDocument.UndoStep = 0;
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
            this.fromTextbox.Location = new System.Drawing.Point(0, 25);
            this.fromTextbox.LockCursorUpdate = false;
            this.fromTextbox.Name = "fromTextbox";
            this.fromTextbox.ShowScopeIndicator = false;
            this.fromTextbox.Size = new System.Drawing.Size(629, 441);
            this.fromTextbox.SmoothScroll = false;
            this.fromTextbox.SplitviewH = -4;
            this.fromTextbox.SplitviewV = -4;
            this.fromTextbox.TabGuideColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(233)))), ((int)(((byte)(233)))));
            this.fromTextbox.TabIndex = 9;
            this.fromTextbox.Text = "syntaxBoxControl1";
            this.fromTextbox.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
            // 
            // ToSingleLineGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fromTextbox);
            this.Controls.Add(this.toolStrip1);
            this.Name = "ToSingleLineGenerator";
            this.Size = new System.Drawing.Size(629, 466);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton mnuOptions;
        private System.Windows.Forms.ToolStripMenuItem mnuAutoCopyResult;
        private Alsing.SourceCode.SyntaxDocument fromDocument;
        private Alsing.Windows.Forms.SyntaxBoxControl fromTextbox;
    }
}
