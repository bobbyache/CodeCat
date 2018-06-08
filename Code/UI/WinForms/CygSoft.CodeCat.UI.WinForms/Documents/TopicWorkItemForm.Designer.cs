using CygSoft.CodeCat.UI.WinForms.CustomControls;

namespace CygSoft.CodeCat.UI.WinForms
{
    partial class TopicWorkItemForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TopicWorkItemForm));
            this.syntaxDoc = new Alsing.SourceCode.SyntaxDocument(this.components);
            this.chkEdit = new System.Windows.Forms.ToolStripButton();
            this.toolstripTitle = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtTitle = new CygSoft.CodeCat.UI.WinForms.CustomControls.ToolStripSpringTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolstripKeywords = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.txtKeywords = new CygSoft.CodeCat.UI.WinForms.CustomControls.ToolStripSpringTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDiscardChange = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolstripCommands = new System.Windows.Forms.ToolStrip();
            this.btnMenu = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAddItem = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnAddHyperlinks = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRemoveCodeItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnMoveLeft = new System.Windows.Forms.ToolStripButton();
            this.btnMoveRight = new System.Windows.Forms.ToolStripButton();
            this.txtToolStripTitle = new CygSoft.CodeCat.UI.WinForms.CustomControls.ToolStripSpringTextBox();
            this.btnOpenFolder = new System.Windows.Forms.ToolStripButton();
            this.tabControlFile = new System.Windows.Forms.TabControl();
            this.toolstripTitle.SuspendLayout();
            this.toolstripKeywords.SuspendLayout();
            this.toolstripCommands.SuspendLayout();
            this.SuspendLayout();
            // 
            // syntaxDoc
            // 
            this.syntaxDoc.Lines = new string[] {
        ""};
            this.syntaxDoc.MaxUndoBufferSize = 1000;
            this.syntaxDoc.Modified = false;
            this.syntaxDoc.UndoStep = 0;
            // 
            // chkEdit
            // 
            this.chkEdit.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.chkEdit.CheckOnClick = true;
            this.chkEdit.Image = ((System.Drawing.Image)(resources.GetObject("chkEdit.Image")));
            this.chkEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.chkEdit.Name = "chkEdit";
            this.chkEdit.Size = new System.Drawing.Size(85, 24);
            this.chkEdit.Text = "Edit Mode";
            // 
            // toolstripTitle
            // 
            this.toolstripTitle.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolstripTitle.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.txtTitle});
            this.toolstripTitle.Location = new System.Drawing.Point(0, 52);
            this.toolstripTitle.Name = "toolstripTitle";
            this.toolstripTitle.Size = new System.Drawing.Size(658, 25);
            this.toolstripTitle.TabIndex = 7;
            this.toolstripTitle.TabStop = true;
            this.toolstripTitle.Text = "toolStrip2";
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
            this.txtTitle.Size = new System.Drawing.Size(550, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // btnDelete
            // 
            this.btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(24, 24);
            this.btnDelete.Text = "Delete";
            // 
            // toolstripKeywords
            // 
            this.toolstripKeywords.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolstripKeywords.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel4,
            this.txtKeywords});
            this.toolstripKeywords.Location = new System.Drawing.Point(0, 27);
            this.toolstripKeywords.Name = "toolstripKeywords";
            this.toolstripKeywords.Size = new System.Drawing.Size(658, 25);
            this.toolstripKeywords.TabIndex = 6;
            this.toolstripKeywords.TabStop = true;
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.AutoSize = false;
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(65, 22);
            this.toolStripLabel4.Text = "Keywords";
            this.toolStripLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtKeywords
            // 
            this.txtKeywords.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtKeywords.Name = "txtKeywords";
            this.txtKeywords.Size = new System.Drawing.Size(550, 25);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // btnDiscardChange
            // 
            this.btnDiscardChange.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDiscardChange.Image = ((System.Drawing.Image)(resources.GetObject("btnDiscardChange.Image")));
            this.btnDiscardChange.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDiscardChange.Name = "btnDiscardChange";
            this.btnDiscardChange.Size = new System.Drawing.Size(24, 24);
            this.btnDiscardChange.Text = "Discard Changes";
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(24, 24);
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // toolstripCommands
            // 
            this.toolstripCommands.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolstripCommands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnMenu,
            this.toolStripSeparator6,
            this.btnSave,
            this.btnDiscardChange,
            this.toolStripSeparator1,
            this.btnDelete,
            this.toolStripSeparator2,
            this.chkEdit,
            this.btnAddItem,
            this.btnRemoveCodeItem,
            this.toolStripSeparator3,
            this.btnMoveLeft,
            this.btnMoveRight,
            this.txtToolStripTitle,
            this.btnOpenFolder});
            this.toolstripCommands.Location = new System.Drawing.Point(0, 0);
            this.toolstripCommands.Name = "toolstripCommands";
            this.toolstripCommands.Size = new System.Drawing.Size(658, 27);
            this.toolstripCommands.TabIndex = 5;
            this.toolstripCommands.Text = "toolStrip4";
            // 
            // btnMenu
            // 
            this.btnMenu.Image = ((System.Drawing.Image)(resources.GetObject("btnMenu.Image")));
            this.btnMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(64, 24);
            this.btnMenu.Text = "Tabs";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 27);
            // 
            // btnAddItem
            // 
            this.btnAddItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddHyperlinks,
            this.toolStripMenuItem2});
            this.btnAddItem.Image = ((System.Drawing.Image)(resources.GetObject("btnAddItem.Image")));
            this.btnAddItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(104, 24);
            this.btnAddItem.Text = "Add Section";
            // 
            // btnAddHyperlinks
            // 
            this.btnAddHyperlinks.Name = "btnAddHyperlinks";
            this.btnAddHyperlinks.Size = new System.Drawing.Size(158, 22);
            this.btnAddHyperlinks.Text = "Web References";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(155, 6);
            // 
            // btnRemoveCodeItem
            // 
            this.btnRemoveCodeItem.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveCodeItem.Image")));
            this.btnRemoveCodeItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRemoveCodeItem.Name = "btnRemoveCodeItem";
            this.btnRemoveCodeItem.Size = new System.Drawing.Size(106, 24);
            this.btnRemoveCodeItem.Text = "Delete Section";
            this.btnRemoveCodeItem.Click += new System.EventHandler(this.btnRemoveCodeItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // btnMoveLeft
            // 
            this.btnMoveLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMoveLeft.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveLeft.Image")));
            this.btnMoveLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMoveLeft.Name = "btnMoveLeft";
            this.btnMoveLeft.Size = new System.Drawing.Size(24, 24);
            this.btnMoveLeft.Text = "Move Left";
            this.btnMoveLeft.Click += new System.EventHandler(this.btnMoveLeft_Click);
            // 
            // btnMoveRight
            // 
            this.btnMoveRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMoveRight.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveRight.Image")));
            this.btnMoveRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMoveRight.Name = "btnMoveRight";
            this.btnMoveRight.Size = new System.Drawing.Size(24, 24);
            this.btnMoveRight.Text = "Move Right";
            this.btnMoveRight.Click += new System.EventHandler(this.btnMoveRight_Click);
            // 
            // txtToolStripTitle
            // 
            this.txtToolStripTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.txtToolStripTitle.Name = "txtToolStripTitle";
            this.txtToolStripTitle.ReadOnly = true;
            this.txtToolStripTitle.Size = new System.Drawing.Size(103, 27);
            this.txtToolStripTitle.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpenFolder.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenFolder.Image")));
            this.btnOpenFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(24, 24);
            this.btnOpenFolder.Text = "Open Folder";
            // 
            // tabControlFile
            // 
            this.tabControlFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlFile.Location = new System.Drawing.Point(0, 77);
            this.tabControlFile.Margin = new System.Windows.Forms.Padding(2);
            this.tabControlFile.Name = "tabControlFile";
            this.tabControlFile.SelectedIndex = 0;
            this.tabControlFile.Size = new System.Drawing.Size(658, 420);
            this.tabControlFile.TabIndex = 8;
            // 
            // TopicWorkItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 497);
            this.Controls.Add(this.tabControlFile);
            this.Controls.Add(this.toolstripTitle);
            this.Controls.Add(this.toolstripKeywords);
            this.Controls.Add(this.toolstripCommands);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "TopicWorkItemForm";
            this.Text = "TopicDocumentForm";
            this.toolstripTitle.ResumeLayout(false);
            this.toolstripTitle.PerformLayout();
            this.toolstripKeywords.ResumeLayout(false);
            this.toolstripKeywords.PerformLayout();
            this.toolstripCommands.ResumeLayout(false);
            this.toolstripCommands.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Alsing.SourceCode.SyntaxDocument syntaxDoc;
        private System.Windows.Forms.ToolStripButton chkEdit;
        private System.Windows.Forms.ToolStrip toolstripTitle;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private ToolStripSpringTextBox txtTitle;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStrip toolstripKeywords;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private ToolStripSpringTextBox txtKeywords;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnDiscardChange;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStrip toolstripCommands;
        private System.Windows.Forms.ToolStripButton btnRemoveCodeItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnMoveLeft;
        private System.Windows.Forms.ToolStripButton btnMoveRight;
        private System.Windows.Forms.TabControl tabControlFile;
        private System.Windows.Forms.ToolStripDropDownButton btnMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private ToolStripSpringTextBox txtToolStripTitle;
        private System.Windows.Forms.ToolStripDropDownButton btnAddItem;
        private System.Windows.Forms.ToolStripMenuItem btnAddHyperlinks;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripButton btnOpenFolder;
    }
}