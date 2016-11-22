namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    partial class ImageControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageControl));
            this.syntaxDocument = new Alsing.SourceCode.SyntaxDocument(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtTitle = new CygSoft.CodeCat.UI.WinForms.ToolStripSpringTextBox();
            this.btnImport = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.lblEditStatus = new System.Windows.Forms.ToolStripLabel();
            this.lblZoomLevel = new System.Windows.Forms.ToolStripLabel();
            this.lblSize = new System.Windows.Forms.ToolStripLabel();
            this.lblScrollPosition = new System.Windows.Forms.ToolStripLabel();
            this.imageContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxFileImportMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxClipboardImportMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.imageBox = new Cyotek.Windows.Forms.ImageBox();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.imageContextMenu.SuspendLayout();
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
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.txtTitle,
            this.btnImport});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(626, 25);
            this.toolStrip1.TabIndex = 10;
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
            this.txtTitle.Size = new System.Drawing.Size(495, 25);
            // 
            // btnImport
            // 
            this.btnImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImport.Image = ((System.Drawing.Image)(resources.GetObject("btnImport.Image")));
            this.btnImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(23, 22);
            this.btnImport.Text = "Import";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.AutoSize = false;
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblEditStatus,
            this.lblZoomLevel,
            this.lblSize,
            this.lblScrollPosition});
            this.toolStrip2.Location = new System.Drawing.Point(0, 504);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(626, 25);
            this.toolStrip2.TabIndex = 11;
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
            // lblZoomLevel
            // 
            this.lblZoomLevel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.lblZoomLevel.Image = ((System.Drawing.Image)(resources.GetObject("lblZoomLevel.Image")));
            this.lblZoomLevel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.lblZoomLevel.Name = "lblZoomLevel";
            this.lblZoomLevel.Size = new System.Drawing.Size(29, 22);
            this.lblZoomLevel.Text = "0";
            // 
            // lblSize
            // 
            this.lblSize.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.lblSize.Image = ((System.Drawing.Image)(resources.GetObject("lblSize.Image")));
            this.lblSize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(29, 22);
            this.lblSize.Text = "0";
            // 
            // lblScrollPosition
            // 
            this.lblScrollPosition.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.lblScrollPosition.Image = ((System.Drawing.Image)(resources.GetObject("lblScrollPosition.Image")));
            this.lblScrollPosition.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.lblScrollPosition.Name = "lblScrollPosition";
            this.lblScrollPosition.Size = new System.Drawing.Size(29, 22);
            this.lblScrollPosition.Text = "0";
            // 
            // imageContextMenu
            // 
            this.imageContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxFileImportMenu,
            this.ctxClipboardImportMenu});
            this.imageContextMenu.Name = "imageContextMenu";
            this.imageContextMenu.Size = new System.Drawing.Size(195, 48);
            // 
            // ctxFileImportMenu
            // 
            this.ctxFileImportMenu.Name = "ctxFileImportMenu";
            this.ctxFileImportMenu.Size = new System.Drawing.Size(194, 22);
            this.ctxFileImportMenu.Text = "Import from File...";
            this.ctxFileImportMenu.Click += new System.EventHandler(this.ctxFileImportMenu_Click);
            // 
            // ctxClipboardImportMenu
            // 
            this.ctxClipboardImportMenu.Name = "ctxClipboardImportMenu";
            this.ctxClipboardImportMenu.Size = new System.Drawing.Size(194, 22);
            this.ctxClipboardImportMenu.Text = "Import from Clipboard";
            this.ctxClipboardImportMenu.Click += new System.EventHandler(this.ctxClipboardImportMenu_Click);
            // 
            // imageBox
            // 
            this.imageBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBox.Location = new System.Drawing.Point(0, 25);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(626, 479);
            this.imageBox.TabIndex = 12;
            this.imageBox.ZoomChanged += new System.EventHandler(this.imageBox_ZoomChanged);
            this.imageBox.Scroll += new System.Windows.Forms.ScrollEventHandler(this.imageBox_Scroll);
            this.imageBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imageBox_MouseUp);
            this.imageBox.Resize += new System.EventHandler(this.imageBox_Resize);
            // 
            // ImageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.imageBox);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.toolStrip2);
            this.Name = "ImageControl";
            this.Size = new System.Drawing.Size(626, 529);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.imageContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Alsing.SourceCode.SyntaxDocument syntaxDocument;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private ToolStripSpringTextBox txtTitle;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel lblEditStatus;
        private System.Windows.Forms.ToolStripButton btnImport;
        private System.Windows.Forms.ContextMenuStrip imageContextMenu;
        private System.Windows.Forms.ToolStripMenuItem ctxFileImportMenu;
        private System.Windows.Forms.ToolStripMenuItem ctxClipboardImportMenu;
        private Cyotek.Windows.Forms.ImageBox imageBox;
        private System.Windows.Forms.ToolStripLabel lblZoomLevel;
        private System.Windows.Forms.ToolStripLabel lblSize;
        private System.Windows.Forms.ToolStripLabel lblScrollPosition;
    }
}
