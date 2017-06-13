namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    partial class CodeItemCtrl
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
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.txtTitle = new CygSoft.CodeCat.UI.WinForms.ToolStripSpringTextBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.lblEditStatus = new System.Windows.Forms.ToolStripLabel();
            this.syntaxBoxControl = new Alsing.Windows.Forms.SyntaxBoxControl();
            this.syntaxDocument = new Alsing.SourceCode.SyntaxDocument(this.components);
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripLabel5});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(656, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.AutoSize = false;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(65, 22);
            this.toolStripLabel1.Text = "Title";
            this.toolStripLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(41, 22);
            this.toolStripLabel5.Text = "Syntax";
            // 
            // txtTitle
            // 
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(384, 25);
            // 
            // toolStrip2
            // 
            this.toolStrip2.AutoSize = false;
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.Location = new System.Drawing.Point(0, 492);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(656, 25);
            this.toolStrip2.TabIndex = 5;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // lblEditStatus
            // 
            this.lblEditStatus.AutoSize = false;
            this.lblEditStatus.Name = "lblEditStatus";
            this.lblEditStatus.Size = new System.Drawing.Size(100, 22);
            this.lblEditStatus.Text = "No Changes";
            this.lblEditStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.syntaxBoxControl.Location = new System.Drawing.Point(0, 25);
            this.syntaxBoxControl.LockCursorUpdate = false;
            this.syntaxBoxControl.Name = "syntaxBoxControl";
            this.syntaxBoxControl.ShowScopeIndicator = false;
            this.syntaxBoxControl.Size = new System.Drawing.Size(656, 467);
            this.syntaxBoxControl.SmoothScroll = false;
            this.syntaxBoxControl.SplitviewH = -4;
            this.syntaxBoxControl.SplitviewV = -4;
            this.syntaxBoxControl.TabGuideColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(233)))), ((int)(((byte)(233)))));
            this.syntaxBoxControl.TabIndex = 6;
            this.syntaxBoxControl.Text = "syntaxBoxControl1";
            this.syntaxBoxControl.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
            // 
            // syntaxDocument
            // 
            this.syntaxDocument.Lines = new string[] {
        ""};
            this.syntaxDocument.MaxUndoBufferSize = 1000;
            this.syntaxDocument.Modified = false;
            this.syntaxDocument.UndoStep = 0;
            // 
            // CodeItemCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.syntaxBoxControl);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip1);
            this.Name = "CodeItemCtrl";
            this.Size = new System.Drawing.Size(656, 517);
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.Controls.SetChildIndex(this.toolStrip2, 0);
            this.Controls.SetChildIndex(this.syntaxBoxControl, 0);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private ToolStripSpringTextBox txtTitle;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel lblEditStatus;
        private Alsing.Windows.Forms.SyntaxBoxControl syntaxBoxControl;
        private Alsing.SourceCode.SyntaxDocument syntaxDocument;
    }
}
