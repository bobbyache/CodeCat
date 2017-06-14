namespace CygSoft.CodeCat.UI.WinForms.Controls.TopicSections
{
    partial class CodeTopicSectionControl
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
            this.syntaxBoxControl = new Alsing.Windows.Forms.SyntaxBoxControl();
            this.syntaxDocument = new Alsing.SourceCode.SyntaxDocument(this.components);
            this.SuspendLayout();
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
            this.Name = "CodeItemCtrl";
            this.Size = new System.Drawing.Size(656, 517);
            this.Controls.SetChildIndex(this.syntaxBoxControl, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Alsing.Windows.Forms.SyntaxBoxControl syntaxBoxControl;
        private Alsing.SourceCode.SyntaxDocument syntaxDocument;
    }
}
