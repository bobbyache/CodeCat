namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    partial class RtfDocumentControl
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
            this.rtfEditor = new CygSoft.CodeCat.UI.WinForms.Controls.RtfEditorControl();
            this.SuspendLayout();
            // 
            // rtfEditor
            // 
            this.rtfEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtfEditor.Location = new System.Drawing.Point(0, 0);
            this.rtfEditor.Name = "rtfEditor";
            this.rtfEditor.Size = new System.Drawing.Size(716, 545);
            this.rtfEditor.TabIndex = 14;
            this.rtfEditor.TextRtf = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang7177{\\fonttbl{\\f0\\fnil\\fcharset0 Calibri;}}\r" +
    "\n\\viewkind4\\uc1\\pard\\f0\\fs24\\par\r\n}\r\n";
            // 
            // RtfDocumentControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rtfEditor);
            this.Name = "RtfDocumentControl";
            this.Size = new System.Drawing.Size(716, 545);
            this.Controls.SetChildIndex(this.rtfEditor, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private RtfEditorControl rtfEditor;
    }
}
