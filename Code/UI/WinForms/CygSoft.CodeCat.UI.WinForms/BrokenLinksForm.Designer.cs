namespace CygSoft.CodeCat.UI.WinForms
{
    partial class BrokenLinksForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.unIndexedlistView = new System.Windows.Forms.ListView();
            this.columnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.missingFileListView = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.okButton = new System.Windows.Forms.Button();
            this.refreshInfoButton = new System.Windows.Forms.Button();
            this.unindexedContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuOpenSnippet = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExploreTo = new System.Windows.Forms.ToolStripMenuItem();
            this.unindexedContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // unIndexedlistView
            // 
            this.unIndexedlistView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.unIndexedlistView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader});
            this.unIndexedlistView.Location = new System.Drawing.Point(-1, 21);
            this.unIndexedlistView.Name = "unIndexedlistView";
            this.unIndexedlistView.Size = new System.Drawing.Size(666, 97);
            this.unIndexedlistView.TabIndex = 0;
            this.unIndexedlistView.UseCompatibleStateImageBehavior = false;
            this.unIndexedlistView.View = System.Windows.Forms.View.Details;
            this.unIndexedlistView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.unIndexedlistView_MouseClick);
            this.unIndexedlistView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.unIndexedlistView_MouseDoubleClick);
            // 
            // columnHeader
            // 
            this.columnHeader.Text = "File Path";
            this.columnHeader.Width = 622;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Snippets (not indexed)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Snippets (missing)";
            // 
            // missingFileListView
            // 
            this.missingFileListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.missingFileListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.missingFileListView.Location = new System.Drawing.Point(-1, 137);
            this.missingFileListView.Name = "missingFileListView";
            this.missingFileListView.Size = new System.Drawing.Size(666, 97);
            this.missingFileListView.TabIndex = 2;
            this.missingFileListView.UseCompatibleStateImageBehavior = false;
            this.missingFileListView.View = System.Windows.Forms.View.Details;
            this.missingFileListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.missingFileListView_MouseDoubleClick);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "File Path";
            this.columnHeader2.Width = 626;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(590, 240);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 4;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // refreshInfoButton
            // 
            this.refreshInfoButton.Location = new System.Drawing.Point(3, 240);
            this.refreshInfoButton.Name = "refreshInfoButton";
            this.refreshInfoButton.Size = new System.Drawing.Size(75, 23);
            this.refreshInfoButton.TabIndex = 5;
            this.refreshInfoButton.Text = "Refresh Info";
            this.refreshInfoButton.UseVisualStyleBackColor = true;
            this.refreshInfoButton.Click += new System.EventHandler(this.refreshInfoButton_Click);
            // 
            // unindexedContextMenu
            // 
            this.unindexedContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOpenSnippet,
            this.mnuExploreTo});
            this.unindexedContextMenu.Name = "unindexedContextMenu";
            this.unindexedContextMenu.Size = new System.Drawing.Size(147, 48);
            // 
            // mnuOpenSnippet
            // 
            this.mnuOpenSnippet.Name = "mnuOpenSnippet";
            this.mnuOpenSnippet.Size = new System.Drawing.Size(152, 22);
            this.mnuOpenSnippet.Text = "Open Snippet";
            this.mnuOpenSnippet.Click += new System.EventHandler(this.mnuOpenSnippet_Click);
            // 
            // mnuExploreTo
            // 
            this.mnuExploreTo.Name = "mnuExploreTo";
            this.mnuExploreTo.Size = new System.Drawing.Size(152, 22);
            this.mnuExploreTo.Text = "Explore to...";
            this.mnuExploreTo.Click += new System.EventHandler(this.mnuExploreTo_Click);
            // 
            // BrokenLinksForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 265);
            this.Controls.Add(this.refreshInfoButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.missingFileListView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.unIndexedlistView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "BrokenLinksForm";
            this.Text = "BrokenLinksForm";
            this.Activated += new System.EventHandler(this.BrokenLinksForm_Activated);
            this.unindexedContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView unIndexedlistView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView missingFileListView;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.ColumnHeader columnHeader;
        private System.Windows.Forms.Button refreshInfoButton;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ContextMenuStrip unindexedContextMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuOpenSnippet;
        private System.Windows.Forms.ToolStripMenuItem mnuExploreTo;
    }
}