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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtTitle = new CygSoft.CodeCat.UI.WinForms.ToolStripSpringTextBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.lblEditStatus = new System.Windows.Forms.ToolStripLabel();
            this.rtfEditor = new CygSoft.CodeCat.UI.WinForms.Controls.RtfFileEditorControl();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.txtTitle});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(716, 25);
            this.toolStrip1.TabIndex = 12;
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
            // txtTitle
            // 
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(608, 25);
            // 
            // toolStrip2
            // 
            this.toolStrip2.AutoSize = false;
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblEditStatus});
            this.toolStrip2.Location = new System.Drawing.Point(0, 520);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(716, 25);
            this.toolStrip2.TabIndex = 13;
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
            // rtfEditor
            // 
            this.rtfEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtfEditor.Location = new System.Drawing.Point(0, 25);
            this.rtfEditor.Name = "rtfEditor";
            this.rtfEditor.Size = new System.Drawing.Size(716, 495);
            this.rtfEditor.TabIndex = 14;
            // 
            // RtfDocumentControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rtfEditor);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.toolStrip2);
            this.Name = "RtfDocumentControl";
            this.Size = new System.Drawing.Size(716, 545);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private ToolStripSpringTextBox txtTitle;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel lblEditStatus;
        private RtfFileEditorControl rtfEditor;
    }
}
