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
            this.ctxMenuViewTopic = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxMenuDeleteTopic = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxMenuViewKeywords = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuAddKeywords = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuRemoveKeywords = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxMenuCopyKeywords = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuCopyIdentifier = new System.Windows.Forms.ToolStripMenuItem();
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
            this.listView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_MouseDoubleClick);
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
            this.ctxMenuViewTopic,
            this.toolStripMenuItem8,
            this.ctxMenuDeleteTopic,
            this.toolStripMenuItem2,
            this.ctxMenuViewKeywords,
            this.ctxMenuAddKeywords,
            this.ctxMenuRemoveKeywords,
            this.toolStripMenuItem5,
            this.ctxMenuCopyKeywords,
            this.ctxMenuCopyIdentifier});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(201, 198);
            // 
            // ctxMenuViewTopic
            // 
            this.ctxMenuViewTopic.Name = "ctxMenuViewTopic";
            this.ctxMenuViewTopic.Size = new System.Drawing.Size(200, 22);
            this.ctxMenuViewTopic.Text = "View\\Modify Work Item";
            this.ctxMenuViewTopic.Click += new System.EventHandler(this.ctxMenuViewTopic_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(197, 6);
            // 
            // ctxMenuDeleteTopic
            // 
            this.ctxMenuDeleteTopic.Name = "ctxMenuDeleteTopic";
            this.ctxMenuDeleteTopic.Size = new System.Drawing.Size(200, 22);
            this.ctxMenuDeleteTopic.Text = "Delete Work Item";
            this.ctxMenuDeleteTopic.Click += new System.EventHandler(this.ctxMenuDeleteTopic_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(197, 6);
            // 
            // ctxMenuViewKeywords
            // 
            this.ctxMenuViewKeywords.Name = "ctxMenuViewKeywords";
            this.ctxMenuViewKeywords.Size = new System.Drawing.Size(200, 22);
            this.ctxMenuViewKeywords.Text = "View Keywords";
            this.ctxMenuViewKeywords.Click += new System.EventHandler(this.ctxMenuViewKeywords_Click);
            // 
            // ctxMenuAddKeywords
            // 
            this.ctxMenuAddKeywords.Name = "ctxMenuAddKeywords";
            this.ctxMenuAddKeywords.Size = new System.Drawing.Size(200, 22);
            this.ctxMenuAddKeywords.Text = "Add Keywords";
            this.ctxMenuAddKeywords.Click += new System.EventHandler(this.ctxMenuAddKeywords_Click);
            // 
            // ctxMenuRemoveKeywords
            // 
            this.ctxMenuRemoveKeywords.Name = "ctxMenuRemoveKeywords";
            this.ctxMenuRemoveKeywords.Size = new System.Drawing.Size(200, 22);
            this.ctxMenuRemoveKeywords.Text = "Remove Keywords";
            this.ctxMenuRemoveKeywords.Click += new System.EventHandler(this.ctxMenuRemoveKeywords_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(197, 6);
            // 
            // ctxMenuCopyKeywords
            // 
            this.ctxMenuCopyKeywords.Name = "ctxMenuCopyKeywords";
            this.ctxMenuCopyKeywords.Size = new System.Drawing.Size(200, 22);
            this.ctxMenuCopyKeywords.Text = "Copy Keywords";
            this.ctxMenuCopyKeywords.Click += new System.EventHandler(this.ctxMenuCopyKeywords_Click);
            // 
            // ctxMenuCopyIdentifier
            // 
            this.ctxMenuCopyIdentifier.Name = "ctxMenuCopyIdentifier";
            this.ctxMenuCopyIdentifier.Size = new System.Drawing.Size(200, 22);
            this.ctxMenuCopyIdentifier.Text = "Copy Identifier";
            this.ctxMenuCopyIdentifier.Click += new System.EventHandler(this.ctxMenuCopyIdentifier_Click);
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
        private System.Windows.Forms.ToolStripMenuItem ctxMenuViewTopic;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuViewKeywords;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuAddKeywords;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuRemoveKeywords;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuCopyKeywords;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuCopyIdentifier;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuDeleteTopic;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
    }
}
