namespace CygSoft.CodeCat.UI.WinForms
{
    partial class SearchForm
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
            this.searchPanel = new System.Windows.Forms.Panel();
            this.findButton = new System.Windows.Forms.Button();
            this.keywordsLabel = new System.Windows.Forms.Label();
            this.keywordsTextBox = new System.Windows.Forms.TextBox();
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
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuContextDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.searchPanel.SuspendLayout();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchPanel
            // 
            this.searchPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchPanel.Controls.Add(this.findButton);
            this.searchPanel.Controls.Add(this.keywordsLabel);
            this.searchPanel.Controls.Add(this.keywordsTextBox);
            this.searchPanel.Location = new System.Drawing.Point(0, 0);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(961, 27);
            this.searchPanel.TabIndex = 2;
            // 
            // findButton
            // 
            this.findButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.findButton.Location = new System.Drawing.Point(909, 2);
            this.findButton.Name = "findButton";
            this.findButton.Size = new System.Drawing.Size(47, 23);
            this.findButton.TabIndex = 4;
            this.findButton.UseVisualStyleBackColor = true;
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
            this.keywordsTextBox.Size = new System.Drawing.Size(797, 20);
            this.keywordsTextBox.TabIndex = 0;
            // 
            // listView
            // 
            this.listView.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader4,
            this.columnHeader5});
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(0, 28);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(961, 430);
            this.listView.TabIndex = 3;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
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
            this.menuContextCopyIdentifier,
            this.toolStripMenuItem4,
            this.menuContextDelete});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(186, 198);
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
            // 
            // menuContextAddKeywords
            // 
            this.menuContextAddKeywords.Name = "menuContextAddKeywords";
            this.menuContextAddKeywords.Size = new System.Drawing.Size(185, 22);
            this.menuContextAddKeywords.Text = "Add Keywords";
            // 
            // menuContextRemoveKeywords
            // 
            this.menuContextRemoveKeywords.Name = "menuContextRemoveKeywords";
            this.menuContextRemoveKeywords.Size = new System.Drawing.Size(185, 22);
            this.menuContextRemoveKeywords.Text = "Remove Keywords";
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
            // 
            // menuContextCopyIdentifier
            // 
            this.menuContextCopyIdentifier.Name = "menuContextCopyIdentifier";
            this.menuContextCopyIdentifier.Size = new System.Drawing.Size(185, 22);
            this.menuContextCopyIdentifier.Text = "Copy Identifier";
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
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(961, 457);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.searchPanel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "SearchForm";
            this.Text = "SearchForm";
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel searchPanel;
        private System.Windows.Forms.Button findButton;
        private System.Windows.Forms.Label keywordsLabel;
        private System.Windows.Forms.TextBox keywordsTextBox;
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
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem menuContextDelete;
    }
}