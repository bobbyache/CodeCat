namespace CygSoft.CodeCat.UI.WinForms
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openRecentMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createNewFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.openFolderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitAppMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.snippetsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addSnippetMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewSnippetMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.addKeywordsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeKeywordsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.copyKeywordsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyIdentifierMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteSnippetMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.brokenLinksMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchPanel = new System.Windows.Forms.Panel();
            this.findButton = new System.Windows.Forms.Button();
            this.keywordsLabel = new System.Windows.Forms.Label();
            this.keywordsTextBox = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.indexCountLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuContextViewSnippet = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.menuContextViewKeywords = new System.Windows.Forms.ToolStripMenuItem();
            this.menuContextAddKeywords = new System.Windows.Forms.ToolStripMenuItem();
            this.menuContextRemoveKeywords = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.menuContextCopyKeywords = new System.Windows.Forms.ToolStripMenuItem();
            this.menuContextCopyIdentifier = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuContextDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.searchPanel.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.snippetsMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(955, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileMenuItem,
            this.openRecentMenuItem,
            this.createNewFileMenuItem,
            this.toolStripMenuItem2,
            this.openFolderMenuItem,
            this.toolStripMenuItem1,
            this.exitAppMenuItem});
            this.fileMenuItem.Name = "fileMenuItem";
            this.fileMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileMenuItem.Text = "File";
            // 
            // openFileMenuItem
            // 
            this.openFileMenuItem.Name = "openFileMenuItem";
            this.openFileMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openFileMenuItem.Size = new System.Drawing.Size(188, 22);
            this.openFileMenuItem.Text = "Open...";
            this.openFileMenuItem.Click += new System.EventHandler(this.openFileMenuItem_Click);
            // 
            // openRecentMenuItem
            // 
            this.openRecentMenuItem.Name = "openRecentMenuItem";
            this.openRecentMenuItem.Size = new System.Drawing.Size(188, 22);
            this.openRecentMenuItem.Text = "Open Recent";
            // 
            // createNewFileMenuItem
            // 
            this.createNewFileMenuItem.Name = "createNewFileMenuItem";
            this.createNewFileMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.createNewFileMenuItem.Size = new System.Drawing.Size(188, 22);
            this.createNewFileMenuItem.Text = "Create New...";
            this.createNewFileMenuItem.Click += new System.EventHandler(this.createNewFileMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(185, 6);
            // 
            // openFolderMenuItem
            // 
            this.openFolderMenuItem.Name = "openFolderMenuItem";
            this.openFolderMenuItem.Size = new System.Drawing.Size(188, 22);
            this.openFolderMenuItem.Text = "Open Project Folder...";
            this.openFolderMenuItem.Click += new System.EventHandler(this.openFolderMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(185, 6);
            // 
            // exitAppMenuItem
            // 
            this.exitAppMenuItem.Name = "exitAppMenuItem";
            this.exitAppMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitAppMenuItem.Size = new System.Drawing.Size(188, 22);
            this.exitAppMenuItem.Text = "Exit";
            this.exitAppMenuItem.Click += new System.EventHandler(this.exitAppMenuItem_Click);
            // 
            // snippetsMenuItem
            // 
            this.snippetsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addSnippetMenuItem,
            this.viewSnippetMenuItem,
            this.toolStripMenuItem3,
            this.addKeywordsMenuItem,
            this.removeKeywordsMenuItem,
            this.toolStripMenuItem7,
            this.copyKeywordsMenuItem,
            this.copyIdentifierMenuItem,
            this.toolStripMenuItem6,
            this.deleteSnippetMenuItem});
            this.snippetsMenuItem.Name = "snippetsMenuItem";
            this.snippetsMenuItem.Size = new System.Drawing.Size(64, 20);
            this.snippetsMenuItem.Text = "Snippets";
            // 
            // addSnippetMenuItem
            // 
            this.addSnippetMenuItem.Name = "addSnippetMenuItem";
            this.addSnippetMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
            this.addSnippetMenuItem.Size = new System.Drawing.Size(231, 22);
            this.addSnippetMenuItem.Text = "Add Snippet...";
            this.addSnippetMenuItem.Click += new System.EventHandler(this.addSnippetMenuItem_Click);
            // 
            // viewSnippetMenuItem
            // 
            this.viewSnippetMenuItem.Name = "viewSnippetMenuItem";
            this.viewSnippetMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.V)));
            this.viewSnippetMenuItem.Size = new System.Drawing.Size(231, 22);
            this.viewSnippetMenuItem.Text = "View\\Modify Snippet...";
            this.viewSnippetMenuItem.Click += new System.EventHandler(this.viewSnippetMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(228, 6);
            // 
            // addKeywordsMenuItem
            // 
            this.addKeywordsMenuItem.Name = "addKeywordsMenuItem";
            this.addKeywordsMenuItem.Size = new System.Drawing.Size(231, 22);
            this.addKeywordsMenuItem.Text = "Add Keywords";
            this.addKeywordsMenuItem.Click += new System.EventHandler(this.addKeywordsMenuItem_Click);
            // 
            // removeKeywordsMenuItem
            // 
            this.removeKeywordsMenuItem.Name = "removeKeywordsMenuItem";
            this.removeKeywordsMenuItem.Size = new System.Drawing.Size(231, 22);
            this.removeKeywordsMenuItem.Text = "Remove Keywords";
            this.removeKeywordsMenuItem.Click += new System.EventHandler(this.removeKeywordsMenuItem_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(228, 6);
            // 
            // copyKeywordsMenuItem
            // 
            this.copyKeywordsMenuItem.Name = "copyKeywordsMenuItem";
            this.copyKeywordsMenuItem.Size = new System.Drawing.Size(231, 22);
            this.copyKeywordsMenuItem.Text = "Copy Keywords";
            this.copyKeywordsMenuItem.Click += new System.EventHandler(this.copyKeywordsMenuItem_Click);
            // 
            // copyIdentifierMenuItem
            // 
            this.copyIdentifierMenuItem.Name = "copyIdentifierMenuItem";
            this.copyIdentifierMenuItem.Size = new System.Drawing.Size(231, 22);
            this.copyIdentifierMenuItem.Text = "Copy Identifier";
            this.copyIdentifierMenuItem.Click += new System.EventHandler(this.copyIdentifierMenuItem_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(228, 6);
            // 
            // deleteSnippetMenuItem
            // 
            this.deleteSnippetMenuItem.Name = "deleteSnippetMenuItem";
            this.deleteSnippetMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D)));
            this.deleteSnippetMenuItem.Size = new System.Drawing.Size(231, 22);
            this.deleteSnippetMenuItem.Text = "Delete Snippet";
            this.deleteSnippetMenuItem.Click += new System.EventHandler(this.deleteSnippetMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.brokenLinksMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // brokenLinksMenuItem
            // 
            this.brokenLinksMenuItem.Name = "brokenLinksMenuItem";
            this.brokenLinksMenuItem.Size = new System.Drawing.Size(210, 22);
            this.brokenLinksMenuItem.Text = "Find Broken Snippet Links";
            this.brokenLinksMenuItem.Click += new System.EventHandler(this.brokenLinksMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.Name = "aboutMenuItem";
            this.aboutMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutMenuItem.Text = "About...";
            this.aboutMenuItem.Click += new System.EventHandler(this.aboutMenuItem_Click);
            // 
            // searchPanel
            // 
            this.searchPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchPanel.Controls.Add(this.findButton);
            this.searchPanel.Controls.Add(this.keywordsLabel);
            this.searchPanel.Controls.Add(this.keywordsTextBox);
            this.searchPanel.Location = new System.Drawing.Point(0, 27);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(955, 27);
            this.searchPanel.TabIndex = 1;
            // 
            // findButton
            // 
            this.findButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.findButton.Location = new System.Drawing.Point(903, 2);
            this.findButton.Name = "findButton";
            this.findButton.Size = new System.Drawing.Size(47, 23);
            this.findButton.TabIndex = 4;
            this.findButton.UseVisualStyleBackColor = true;
            this.findButton.Click += new System.EventHandler(this.findButton_Click);
            // 
            // keywordsLabel
            // 
            this.keywordsLabel.AutoSize = true;
            this.keywordsLabel.Location = new System.Drawing.Point(12, 6);
            this.keywordsLabel.Name = "keywordsLabel";
            this.keywordsLabel.Size = new System.Drawing.Size(53, 13);
            this.keywordsLabel.TabIndex = 1;
            this.keywordsLabel.Text = "Keywords";
            // 
            // keywordsTextBox
            // 
            this.keywordsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.keywordsTextBox.Location = new System.Drawing.Point(106, 3);
            this.keywordsTextBox.Name = "keywordsTextBox";
            this.keywordsTextBox.Size = new System.Drawing.Size(791, 20);
            this.keywordsTextBox.TabIndex = 0;
            this.keywordsTextBox.TextChanged += new System.EventHandler(this.keywordsTextBox_TextChanged);
            // 
            // listView1
            // 
            this.listView1.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader4,
            this.columnHeader5});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 56);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(955, 401);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseClick);
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
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
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.indexCountLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 460);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(955, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // indexCountLabel
            // 
            this.indexCountLabel.Name = "indexCountLabel";
            this.indexCountLabel.Size = new System.Drawing.Size(97, 17);
            this.indexCountLabel.Text = "No Items Loaded";
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
            this.menuContextCopyIdentifier,
            this.toolStripMenuItem4,
            this.menuContextDelete});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(186, 176);
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
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(182, 6);
            // 
            // menuContextDelete
            // 
            this.menuContextDelete.Name = "menuContextDelete";
            this.menuContextDelete.Size = new System.Drawing.Size(185, 22);
            this.menuContextDelete.Text = "Delete";
            this.menuContextDelete.Click += new System.EventHandler(this.menuContextDelete_Click);
            // 
            // MainForm
            // 
            this.AcceptButton = this.findButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(955, 482);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.searchPanel);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createNewFileMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitAppMenuItem;
        private System.Windows.Forms.Panel searchPanel;
        private System.Windows.Forms.Label keywordsLabel;
        private System.Windows.Forms.TextBox keywordsTextBox;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button findButton;
        private System.Windows.Forms.ToolStripStatusLabel indexCountLabel;
        private System.Windows.Forms.ToolStripMenuItem openRecentMenuItem;
        private System.Windows.Forms.ToolStripMenuItem snippetsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addSnippetMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewSnippetMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteSnippetMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem openFolderMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem brokenLinksMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem menuContextViewSnippet;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem menuContextCopyKeywords;
        private System.Windows.Forms.ToolStripMenuItem menuContextCopyIdentifier;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem menuContextDelete;
        private System.Windows.Forms.ToolStripMenuItem copyKeywordsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyIdentifierMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem menuContextAddKeywords;
        private System.Windows.Forms.ToolStripMenuItem menuContextRemoveKeywords;
        private System.Windows.Forms.ToolStripMenuItem addKeywordsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeKeywordsMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem menuContextViewKeywords;
    }
}

