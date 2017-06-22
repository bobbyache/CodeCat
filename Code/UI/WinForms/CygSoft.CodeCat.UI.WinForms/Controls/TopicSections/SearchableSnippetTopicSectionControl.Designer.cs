namespace CygSoft.CodeCat.UI.WinForms.Controls.TopicSections
{
    partial class SearchableSnippetTopicSectionControl
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
            this.splitter = new System.Windows.Forms.SplitContainer();
            this.listView = new System.Windows.Forms.ListView();
            this.colTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSyntax = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.syntaxBox = new Alsing.Windows.Forms.SyntaxBoxControl();
            this.syntaxDocument = new Alsing.SourceCode.SyntaxDocument(this.components);
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuCopyCode = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.btnFind = new System.Windows.Forms.Button();
            this.keywordsTextBox = new CygSoft.CodeCat.UI.WinForms.Controls.AutoCompleteSearch.SearchTextBox(this.components);
            this.lstAutoComplete = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitter)).BeginInit();
            this.splitter.Panel1.SuspendLayout();
            this.splitter.Panel2.SuspendLayout();
            this.splitter.SuspendLayout();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitter
            // 
            this.splitter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitter.Location = new System.Drawing.Point(4, 53);
            this.splitter.Name = "splitter";
            this.splitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitter.Panel1
            // 
            this.splitter.Panel1.Controls.Add(this.lstAutoComplete);
            this.splitter.Panel1.Controls.Add(this.listView);
            // 
            // splitter.Panel2
            // 
            this.splitter.Panel2.Controls.Add(this.syntaxBox);
            this.splitter.Size = new System.Drawing.Size(680, 382);
            this.splitter.SplitterDistance = 226;
            this.splitter.TabIndex = 8;
            // 
            // listView
            // 
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colTitle,
            this.colSyntax});
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.FullRowSelect = true;
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(0, 0);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(680, 226);
            this.listView.TabIndex = 0;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            // 
            // colTitle
            // 
            this.colTitle.Text = "Title";
            this.colTitle.Width = 645;
            // 
            // colSyntax
            // 
            this.colSyntax.Text = "Syntax";
            this.colSyntax.Width = 100;
            // 
            // syntaxBox
            // 
            this.syntaxBox.ActiveView = Alsing.Windows.Forms.ActiveView.BottomRight;
            this.syntaxBox.AutoListPosition = null;
            this.syntaxBox.AutoListSelectedText = "a123";
            this.syntaxBox.AutoListVisible = false;
            this.syntaxBox.BackColor = System.Drawing.Color.White;
            this.syntaxBox.BorderStyle = Alsing.Windows.Forms.BorderStyle.None;
            this.syntaxBox.CopyAsRTF = false;
            this.syntaxBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.syntaxBox.Document = this.syntaxDocument;
            this.syntaxBox.FontName = "Courier new";
            this.syntaxBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.syntaxBox.InfoTipCount = 1;
            this.syntaxBox.InfoTipPosition = null;
            this.syntaxBox.InfoTipSelectedIndex = 1;
            this.syntaxBox.InfoTipVisible = false;
            this.syntaxBox.Location = new System.Drawing.Point(0, 0);
            this.syntaxBox.LockCursorUpdate = false;
            this.syntaxBox.Name = "syntaxBox";
            this.syntaxBox.ShowScopeIndicator = false;
            this.syntaxBox.Size = new System.Drawing.Size(680, 152);
            this.syntaxBox.SmoothScroll = false;
            this.syntaxBox.SplitviewH = -4;
            this.syntaxBox.SplitviewV = -4;
            this.syntaxBox.TabGuideColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(233)))), ((int)(((byte)(233)))));
            this.syntaxBox.TabIndex = 3;
            this.syntaxBox.Text = "syntaxBoxControl1";
            this.syntaxBox.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
            // 
            // syntaxDocument
            // 
            this.syntaxDocument.Lines = new string[] {
        ""};
            this.syntaxDocument.MaxUndoBufferSize = 1000;
            this.syntaxDocument.Modified = false;
            this.syntaxDocument.UndoStep = 0;
            // 
            // contextMenu
            // 
            this.contextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCopyCode,
            this.toolStripMenuItem1,
            this.mnuNew,
            this.mnuEdit,
            this.mnuDelete,
            this.toolStripMenuItem2,
            this.mnuCopy,
            this.mnuPaste});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(134, 148);
            // 
            // mnuCopyCode
            // 
            this.mnuCopyCode.Name = "mnuCopyCode";
            this.mnuCopyCode.Size = new System.Drawing.Size(133, 22);
            this.mnuCopyCode.Text = "Copy Code";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(130, 6);
            // 
            // mnuNew
            // 
            this.mnuNew.Name = "mnuNew";
            this.mnuNew.Size = new System.Drawing.Size(133, 22);
            this.mnuNew.Text = "New";
            // 
            // mnuEdit
            // 
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.Size = new System.Drawing.Size(133, 22);
            this.mnuEdit.Text = "Edit";
            // 
            // mnuDelete
            // 
            this.mnuDelete.Name = "mnuDelete";
            this.mnuDelete.Size = new System.Drawing.Size(133, 22);
            this.mnuDelete.Text = "Delete";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(130, 6);
            // 
            // mnuCopy
            // 
            this.mnuCopy.Name = "mnuCopy";
            this.mnuCopy.Size = new System.Drawing.Size(133, 22);
            this.mnuCopy.Text = "Copy";
            // 
            // mnuPaste
            // 
            this.mnuPaste.Name = "mnuPaste";
            this.mnuPaste.Size = new System.Drawing.Size(133, 22);
            this.mnuPaste.Text = "Paste";
            // 
            // btnFind
            // 
            this.btnFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFind.Location = new System.Drawing.Point(649, 29);
            this.btnFind.Margin = new System.Windows.Forms.Padding(2);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(35, 18);
            this.btnFind.TabIndex = 10;
            this.btnFind.UseVisualStyleBackColor = true;
            // 
            // keywordsTextBox
            // 
            this.keywordsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.keywordsTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.keywordsTextBox.Location = new System.Drawing.Point(4, 28);
            this.keywordsTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.keywordsTextBox.Name = "keywordsTextBox";
            this.keywordsTextBox.Size = new System.Drawing.Size(643, 20);
            this.keywordsTextBox.TabIndex = 9;
            // 
            // lstAutoComplete
            // 
            this.lstAutoComplete.FormattingEnabled = true;
            this.lstAutoComplete.Location = new System.Drawing.Point(337, -24);
            this.lstAutoComplete.Name = "lstAutoComplete";
            this.lstAutoComplete.Size = new System.Drawing.Size(120, 95);
            this.lstAutoComplete.TabIndex = 11;
            // 
            // SearchableSnippetTopicSectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.keywordsTextBox);
            this.Controls.Add(this.splitter);
            this.Name = "SearchableSnippetTopicSectionControl";
            this.Size = new System.Drawing.Size(688, 463);
            this.Controls.SetChildIndex(this.splitter, 0);
            this.Controls.SetChildIndex(this.keywordsTextBox, 0);
            this.Controls.SetChildIndex(this.btnFind, 0);
            this.splitter.Panel1.ResumeLayout(false);
            this.splitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitter)).EndInit();
            this.splitter.ResumeLayout(false);
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitter;
        private Alsing.Windows.Forms.SyntaxBoxControl syntaxBox;
        private Alsing.SourceCode.SyntaxDocument syntaxDocument;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader colTitle;
        private System.Windows.Forms.ColumnHeader colSyntax;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuCopyCode;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuNew;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit;
        private System.Windows.Forms.ToolStripMenuItem mnuDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mnuCopy;
        private System.Windows.Forms.ToolStripMenuItem mnuPaste;
        private System.Windows.Forms.Button btnFind;
        private AutoCompleteSearch.SearchTextBox keywordsTextBox;
        private System.Windows.Forms.ListBox lstAutoComplete;
    }
}
