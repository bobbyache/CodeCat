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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageSetControl));
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.lblEditStatus = new System.Windows.Forms.ToolStripLabel();
            this.lblZoomLevel = new System.Windows.Forms.ToolStripLabel();
            this.lblSize = new System.Windows.Forms.ToolStripLabel();
            this.lblScrollPosition = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtTitle = new CygSoft.CodeCat.UI.WinForms.ToolStripSpringTextBox();
            this.btnImport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnShowList = new System.Windows.Forms.ToolStripButton();
            this.btnShowDescription = new System.Windows.Forms.ToolStripButton();
            this.horizontalSplitter = new System.Windows.Forms.SplitContainer();
            this.verticalSplitter = new System.Windows.Forms.SplitContainer();
            this.listView = new System.Windows.Forms.ListView();
            this.imageBox = new Cyotek.Windows.Forms.ImageBox();
            this.btnForward = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.toolStrip2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.horizontalSplitter)).BeginInit();
            this.horizontalSplitter.Panel1.SuspendLayout();
            this.horizontalSplitter.Panel2.SuspendLayout();
            this.horizontalSplitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.verticalSplitter)).BeginInit();
            this.verticalSplitter.Panel1.SuspendLayout();
            this.verticalSplitter.Panel2.SuspendLayout();
            this.verticalSplitter.SuspendLayout();
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
            this.lblScrollPosition});
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
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.txtTitle,
            this.btnImport,
            this.toolStripSeparator1,
            this.btnShowList,
            this.btnShowDescription});
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
            this.txtTitle.Size = new System.Drawing.Size(583, 25);
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
            // btnShowList
            // 
            this.btnShowList.CheckOnClick = true;
            this.btnShowList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnShowList.Image = ((System.Drawing.Image)(resources.GetObject("btnShowList.Image")));
            this.btnShowList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowList.Name = "btnShowList";
            this.btnShowList.Size = new System.Drawing.Size(23, 22);
            this.btnShowList.Text = "Show List";
            this.btnShowList.Click += new System.EventHandler(this.btnShowList_Click);
            // 
            // btnShowDescription
            // 
            this.btnShowDescription.CheckOnClick = true;
            this.btnShowDescription.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnShowDescription.Image = ((System.Drawing.Image)(resources.GetObject("btnShowDescription.Image")));
            this.btnShowDescription.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowDescription.Name = "btnShowDescription";
            this.btnShowDescription.Size = new System.Drawing.Size(23, 22);
            this.btnShowDescription.Text = "toolStripButton1";
            this.btnShowDescription.Click += new System.EventHandler(this.btnShowDescription_Click);
            // 
            // horizontalSplitter
            // 
            this.horizontalSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.horizontalSplitter.Location = new System.Drawing.Point(0, 25);
            this.horizontalSplitter.Name = "horizontalSplitter";
            this.horizontalSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // horizontalSplitter.Panel1
            // 
            this.horizontalSplitter.Panel1.Controls.Add(this.verticalSplitter);
            // 
            // horizontalSplitter.Panel2
            // 
            this.horizontalSplitter.Panel2.Controls.Add(this.txtDescription);
            this.horizontalSplitter.Size = new System.Drawing.Size(766, 507);
            this.horizontalSplitter.SplitterDistance = 413;
            this.horizontalSplitter.TabIndex = 14;
            // 
            // verticalSplitter
            // 
            this.verticalSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.verticalSplitter.Location = new System.Drawing.Point(0, 0);
            this.verticalSplitter.Name = "verticalSplitter";
            // 
            // verticalSplitter.Panel1
            // 
            this.verticalSplitter.Panel1.Controls.Add(this.listView);
            // 
            // verticalSplitter.Panel2
            // 
            this.verticalSplitter.Panel2.Controls.Add(this.imageBox);
            this.verticalSplitter.Panel2.Controls.Add(this.btnForward);
            this.verticalSplitter.Panel2.Controls.Add(this.btnBack);
            this.verticalSplitter.Size = new System.Drawing.Size(766, 413);
            this.verticalSplitter.SplitterDistance = 161;
            this.verticalSplitter.TabIndex = 0;
            // 
            // listView
            // 
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.Location = new System.Drawing.Point(0, 0);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(161, 413);
            this.listView.TabIndex = 0;
            this.listView.UseCompatibleStateImageBehavior = false;
            // 
            // imageBox
            // 
            this.imageBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBox.Location = new System.Drawing.Point(40, 0);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(521, 413);
            this.imageBox.TabIndex = 2;
            this.imageBox.Text = "what happens if we put all this stuff over here and this just goes over like this" +
    " and then that and then that will this actually do something and then something " +
    "else";
            this.imageBox.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.imageBox.TextBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.imageBox.TextPadding = new System.Windows.Forms.Padding(5);
            // 
            // btnForward
            // 
            this.btnForward.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnForward.Location = new System.Drawing.Point(561, 0);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(40, 413);
            this.btnForward.TabIndex = 1;
            this.btnForward.Text = ">";
            this.btnForward.UseVisualStyleBackColor = true;
            // 
            // btnBack
            // 
            this.btnBack.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnBack.Location = new System.Drawing.Point(0, 0);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(40, 413);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "<";
            this.btnBack.UseVisualStyleBackColor = true;
            // 
            // txtDescription
            // 
            this.txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescription.Location = new System.Drawing.Point(0, 0);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(766, 90);
            this.txtDescription.TabIndex = 0;
            // 
            // ImageSetControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.horizontalSplitter);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.toolStrip2);
            this.Name = "ImageSetControl";
            this.Size = new System.Drawing.Size(766, 557);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.horizontalSplitter.Panel1.ResumeLayout(false);
            this.horizontalSplitter.Panel2.ResumeLayout(false);
            this.horizontalSplitter.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.horizontalSplitter)).EndInit();
            this.horizontalSplitter.ResumeLayout(false);
            this.verticalSplitter.Panel1.ResumeLayout(false);
            this.verticalSplitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.verticalSplitter)).EndInit();
            this.verticalSplitter.ResumeLayout(false);
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
        private System.Windows.Forms.SplitContainer horizontalSplitter;
        private System.Windows.Forms.SplitContainer verticalSplitter;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.ListView listView;
        private Cyotek.Windows.Forms.ImageBox imageBox;
        private System.Windows.Forms.Button btnForward;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.ToolStripButton btnShowList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnShowDescription;
    }
}
