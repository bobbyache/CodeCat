namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    partial class ImageSetControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageSetControl));
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.lblEditStatus = new System.Windows.Forms.ToolStripLabel();
            this.lblZoomLevel = new System.Windows.Forms.ToolStripLabel();
            this.lblSize = new System.Windows.Forms.ToolStripLabel();
            this.lblScrollPosition = new System.Windows.Forms.ToolStripLabel();
            this.lblImagePosition = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtTitle = new CygSoft.CodeCat.UI.WinForms.ToolStripSpringTextBox();
            this.btnImport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnMoveLeft = new System.Windows.Forms.ToolStripButton();
            this.btnMoveRight = new System.Windows.Forms.ToolStripButton();
            this.btnDisplayText = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDeleteImage = new System.Windows.Forms.ToolStripButton();
            this.btnForward = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.imageBox = new Cyotek.Windows.Forms.ImageBox();
            this.imageContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxFileImportMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxClipboardImportMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.imageContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip2
            // 
            this.toolStrip2.AutoSize = false;
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblEditStatus,
            this.lblZoomLevel,
            this.lblSize,
            this.lblScrollPosition,
            this.lblImagePosition});
            this.toolStrip2.Location = new System.Drawing.Point(0, 532);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(766, 25);
            this.toolStrip2.TabIndex = 12;
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
            // lblImagePosition
            // 
            this.lblImagePosition.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.lblImagePosition.Name = "lblImagePosition";
            this.lblImagePosition.Size = new System.Drawing.Size(82, 22);
            this.lblImagePosition.Text = "Position 1 of 5";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.txtTitle,
            this.btnImport,
            this.toolStripSeparator1,
            this.btnMoveLeft,
            this.btnMoveRight,
            this.btnDisplayText,
            this.toolStripSeparator2,
            this.btnDeleteImage});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(766, 25);
            this.toolStrip1.TabIndex = 13;
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
            this.txtTitle.Size = new System.Drawing.Size(531, 25);
            // 
            // btnImport
            // 
            this.btnImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImport.Image = ((System.Drawing.Image)(resources.GetObject("btnImport.Image")));
            this.btnImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(23, 22);
            this.btnImport.Text = "Import";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnMoveLeft
            // 
            this.btnMoveLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMoveLeft.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveLeft.Image")));
            this.btnMoveLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMoveLeft.Name = "btnMoveLeft";
            this.btnMoveLeft.Size = new System.Drawing.Size(23, 22);
            this.btnMoveLeft.Text = "Move Left";
            this.btnMoveLeft.Click += new System.EventHandler(this.btnMoveLeft_Click);
            // 
            // btnMoveRight
            // 
            this.btnMoveRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMoveRight.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveRight.Image")));
            this.btnMoveRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMoveRight.Name = "btnMoveRight";
            this.btnMoveRight.Size = new System.Drawing.Size(23, 22);
            this.btnMoveRight.Text = "Move Right";
            this.btnMoveRight.Click += new System.EventHandler(this.btnMoveRight_Click);
            // 
            // btnDisplayText
            // 
            this.btnDisplayText.Checked = true;
            this.btnDisplayText.CheckOnClick = true;
            this.btnDisplayText.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnDisplayText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDisplayText.Image = ((System.Drawing.Image)(resources.GetObject("btnDisplayText.Image")));
            this.btnDisplayText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDisplayText.Name = "btnDisplayText";
            this.btnDisplayText.Size = new System.Drawing.Size(23, 22);
            this.btnDisplayText.Text = "Display Text";
            this.btnDisplayText.Click += new System.EventHandler(this.btnDisplayText_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnDeleteImage
            // 
            this.btnDeleteImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDeleteImage.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteImage.Image")));
            this.btnDeleteImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteImage.Name = "btnDeleteImage";
            this.btnDeleteImage.Size = new System.Drawing.Size(23, 22);
            this.btnDeleteImage.Text = "Delete Image";
            this.btnDeleteImage.Click += new System.EventHandler(this.btnDeleteImage_Click);
            // 
            // btnForward
            // 
            this.btnForward.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnForward.Location = new System.Drawing.Point(726, 25);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(40, 507);
            this.btnForward.TabIndex = 18;
            this.btnForward.Text = ">";
            this.btnForward.UseVisualStyleBackColor = true;
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // btnBack
            // 
            this.btnBack.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnBack.Location = new System.Drawing.Point(0, 25);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(40, 507);
            this.btnBack.TabIndex = 17;
            this.btnBack.Text = "<";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // imageBox
            // 
            this.imageBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imageBox.Location = new System.Drawing.Point(40, 25);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(686, 507);
            this.imageBox.TabIndex = 20;
            this.imageBox.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.imageBox.TextBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.imageBox.TextPadding = new System.Windows.Forms.Padding(5);
            this.imageBox.ZoomChanged += new System.EventHandler(this.imageBox_ZoomChanged);
            this.imageBox.Scroll += new System.Windows.Forms.ScrollEventHandler(this.imageBox_Scroll);
            this.imageBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imageBox_MouseUp);
            this.imageBox.Resize += new System.EventHandler(this.imageBox_Resize);
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
            // ImageSetControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.imageBox);
            this.Controls.Add(this.btnForward);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.toolStrip2);
            this.Name = "ImageSetControl";
            this.Size = new System.Drawing.Size(766, 557);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.imageContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel lblEditStatus;
        private System.Windows.Forms.ToolStripLabel lblZoomLevel;
        private System.Windows.Forms.ToolStripLabel lblSize;
        private System.Windows.Forms.ToolStripLabel lblScrollPosition;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private ToolStripSpringTextBox txtTitle;
        private System.Windows.Forms.ToolStripButton btnImport;
        private System.Windows.Forms.Button btnForward;
        private System.Windows.Forms.Button btnBack;
        private Cyotek.Windows.Forms.ImageBox imageBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnMoveLeft;
        private System.Windows.Forms.ToolStripButton btnMoveRight;
        private System.Windows.Forms.ToolStripLabel lblImagePosition;
        private System.Windows.Forms.ContextMenuStrip imageContextMenu;
        private System.Windows.Forms.ToolStripMenuItem ctxFileImportMenu;
        private System.Windows.Forms.ToolStripMenuItem ctxClipboardImportMenu;
        private System.Windows.Forms.ToolStripButton btnDisplayText;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnDeleteImage;
    }
}
