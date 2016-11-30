namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    partial class CodeSearchResultsControl
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
            this.listView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuContextViewSnippet = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.menuContextViewKeywords = new System.Windows.Forms.ToolStripMenuItem();
            this.menuContextAddKeywords = new System.Windows.Forms.ToolStripMenuItem();
            this.menuContextRemoveKeywords = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.menuContextCopyKeywords = new System.Windows.Forms.ToolStripMenuItem();
            this.menuContextCopyIdentifier = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView
            // 
            this.listView.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader4,
            this.columnHeader5});
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.FullRowSelect = true;
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(0, 0);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(537, 548);
            this.listView.TabIndex = 4;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_ColumnClick);
            this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            this.listView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView_MouseClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Title";
            this.columnHeader1.Width = 528;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Date Created";
            this.columnHeader4.Width = 138;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Date Modified";
            this.columnHeader5.Width = 141;
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuContextViewSnippet,
            this.toolStripMenuItem8,
            this.menuContextViewKeywords,
            this.menuContextAddKeywords,
            this.menuContextRemoveKeywords,
            this.toolStripMenuItem5,
            this.menuContextCopyKeywords,
            this.menuContextCopyIdentifier});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(186, 148);
            // 
            // menuContextViewSnippet
            // 
            this.menuContextViewSnippet.Name = "menuContextViewSnippet";
            this.menuContextViewSnippet.Size = new System.Drawing.Size(185, 22);
            this.menuContextViewSnippet.Text = "View\\Modify Snippet";
            this.menuContextViewSnippet.Click += new System.EventHandler(this.menuContextViewSnippet_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(182, 6);
            // 
            // menuContextViewKeywords
            // 
            this.menuContextViewKeywords.Name = "menuContextViewKeywords";
            this.menuContextViewKeywords.Size = new System.Drawing.Size(185, 22);
            this.menuContextViewKeywords.Text = "View Keywords";
            this.menuContextViewKeywords.Click += new System.EventHandler(this.menuContextViewKeywords_Click);
            // 
            // menuContextAddKeywords
            // 
            this.menuContextAddKeywords.Name = "menuContextAddKeywords";
            this.menuContextAddKeywords.Size = new System.Drawing.Size(185, 22);
            this.menuContextAddKeywords.Text = "Add Keywords";
            this.menuContextAddKeywords.Click += new System.EventHandler(this.menuContextAddKeywords_Click);
            // 
            // menuContextRemoveKeywords
            // 
            this.menuContextRemoveKeywords.Name = "menuContextRemoveKeywords";
            this.menuContextRemoveKeywords.Size = new System.Drawing.Size(185, 22);
            this.menuContextRemoveKeywords.Text = "Remove Keywords";
            this.menuContextRemoveKeywords.Click += new System.EventHandler(this.menuContextRemoveKeywords_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(182, 6);
            // 
            // menuContextCopyKeywords
            // 
            this.menuContextCopyKeywords.Name = "menuContextCopyKeywords";
            this.menuContextCopyKeywords.Size = new System.Drawing.Size(185, 22);
            this.menuContextCopyKeywords.Text = "Copy Keywords";
            this.menuContextCopyKeywords.Click += new System.EventHandler(this.menuContextCopyKeywords_Click);
            // 
            // menuContextCopyIdentifier
            // 
            this.menuContextCopyIdentifier.Name = "menuContextCopyIdentifier";
            this.menuContextCopyIdentifier.Size = new System.Drawing.Size(185, 22);
            this.menuContextCopyIdentifier.Text = "Copy Identifier";
            this.menuContextCopyIdentifier.Click += new System.EventHandler(this.menuContextCopyIdentifier_Click);
            // 
            // CodeSearchResultsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listView);
            this.Name = "CodeSearchResultsControl";
            this.Size = new System.Drawing.Size(537, 548);
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem menuContextViewSnippet;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem menuContextViewKeywords;
        private System.Windows.Forms.ToolStripMenuItem menuContextAddKeywords;
        private System.Windows.Forms.ToolStripMenuItem menuContextRemoveKeywords;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem menuContextCopyKeywords;
        private System.Windows.Forms.ToolStripMenuItem menuContextCopyIdentifier;
    }
}
