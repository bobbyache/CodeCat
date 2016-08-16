namespace CygSoft.CodeCat.UI.WinForms
{
    partial class QikCodeDocument
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QikCodeDocument));
            this.syntaxDoc = new Alsing.SourceCode.SyntaxDocument(this.components);
            this.chkEdit = new System.Windows.Forms.ToolStripButton();
            this.toolstripTitle = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtTitle = new CygSoft.CodeCat.UI.WinForms.ToolStripSpringTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolstripKeywords = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.txtKeywords = new CygSoft.CodeCat.UI.WinForms.ToolStripSpringTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDiscardChange = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolstripCommands = new System.Windows.Forms.ToolStrip();
            this.btnAddTemplate = new System.Windows.Forms.ToolStripButton();
            this.btnRemoveTemplate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnShowProperties = new System.Windows.Forms.ToolStripButton();
            this.btnShowScript = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCompile = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControlFile = new System.Windows.Forms.TabControl();
            this.inputPropertyGrid = new CygSoft.CodeCat.UI.WinForms.Controls.InputPropertyGrid();
            this.toolstripTitle.SuspendLayout();
            this.toolstripKeywords.SuspendLayout();
            this.toolstripCommands.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            this.chkEdit.Size = new System.Drawing.Size(81, 22);
            this.chkEdit.Text = "Edit Mode";
            // 
            // toolstripTitle
            // 
            this.toolstripTitle.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.txtTitle});
            this.toolstripTitle.Location = new System.Drawing.Point(0, 50);
            this.toolstripTitle.Name = "toolstripTitle";
            this.toolstripTitle.Size = new System.Drawing.Size(878, 25);
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
            this.txtTitle.Size = new System.Drawing.Size(770, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnDelete
            // 
            this.btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(23, 22);
            this.btnDelete.Text = "Delete";
            // 
            // toolstripKeywords
            // 
            this.toolstripKeywords.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel4,
            this.txtKeywords});
            this.toolstripKeywords.Location = new System.Drawing.Point(0, 25);
            this.toolstripKeywords.Name = "toolstripKeywords";
            this.toolstripKeywords.Size = new System.Drawing.Size(878, 25);
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
            this.txtKeywords.Size = new System.Drawing.Size(770, 25);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnDiscardChange
            // 
            this.btnDiscardChange.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDiscardChange.Image = ((System.Drawing.Image)(resources.GetObject("btnDiscardChange.Image")));
            this.btnDiscardChange.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDiscardChange.Name = "btnDiscardChange";
            this.btnDiscardChange.Size = new System.Drawing.Size(23, 22);
            this.btnDiscardChange.Text = "Discard Changes";
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(23, 22);
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // toolstripCommands
            // 
            this.toolstripCommands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSave,
            this.btnDiscardChange,
            this.toolStripSeparator1,
            this.btnDelete,
            this.toolStripSeparator2,
            this.chkEdit,
            this.btnAddTemplate,
            this.btnRemoveTemplate,
            this.toolStripSeparator3,
            this.btnShowProperties,
            this.btnShowScript,
            this.toolStripSeparator4,
            this.btnCompile});
            this.toolstripCommands.Location = new System.Drawing.Point(0, 0);
            this.toolstripCommands.Name = "toolstripCommands";
            this.toolstripCommands.Size = new System.Drawing.Size(878, 25);
            this.toolstripCommands.TabIndex = 5;
            this.toolstripCommands.Text = "toolStrip4";
            // 
            // btnAddTemplate
            // 
            this.btnAddTemplate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAddTemplate.Image = ((System.Drawing.Image)(resources.GetObject("btnAddTemplate.Image")));
            this.btnAddTemplate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddTemplate.Name = "btnAddTemplate";
            this.btnAddTemplate.Size = new System.Drawing.Size(23, 22);
            this.btnAddTemplate.Text = "Add Template";
            this.btnAddTemplate.Click += new System.EventHandler(this.btnAddTemplate_Click);
            // 
            // btnRemoveTemplate
            // 
            this.btnRemoveTemplate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRemoveTemplate.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveTemplate.Image")));
            this.btnRemoveTemplate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRemoveTemplate.Name = "btnRemoveTemplate";
            this.btnRemoveTemplate.Size = new System.Drawing.Size(23, 22);
            this.btnRemoveTemplate.Text = "Remove Template";
            this.btnRemoveTemplate.Click += new System.EventHandler(this.btnRemoveTemplate_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnShowProperties
            // 
            this.btnShowProperties.Checked = true;
            this.btnShowProperties.CheckOnClick = true;
            this.btnShowProperties.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnShowProperties.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnShowProperties.Image = ((System.Drawing.Image)(resources.GetObject("btnShowProperties.Image")));
            this.btnShowProperties.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowProperties.Name = "btnShowProperties";
            this.btnShowProperties.Size = new System.Drawing.Size(23, 22);
            this.btnShowProperties.Text = "Show Properties";
            this.btnShowProperties.CheckedChanged += new System.EventHandler(this.btnShowProperties_CheckedChanged);
            // 
            // btnShowScript
            // 
            this.btnShowScript.CheckOnClick = true;
            this.btnShowScript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnShowScript.Image = ((System.Drawing.Image)(resources.GetObject("btnShowScript.Image")));
            this.btnShowScript.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowScript.Name = "btnShowScript";
            this.btnShowScript.Size = new System.Drawing.Size(23, 22);
            this.btnShowScript.Text = "Hide Qik Script";
            this.btnShowScript.CheckedChanged += new System.EventHandler(this.btnShowScript_CheckedChanged);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnCompile
            // 
            this.btnCompile.Image = ((System.Drawing.Image)(resources.GetObject("btnCompile.Image")));
            this.btnCompile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCompile.Name = "btnCompile";
            this.btnCompile.Size = new System.Drawing.Size(72, 22);
            this.btnCompile.Text = "Compile";
            this.btnCompile.Click += new System.EventHandler(this.btnCompile_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 75);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControlFile);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.inputPropertyGrid);
            this.splitContainer1.Size = new System.Drawing.Size(878, 575);
            this.splitContainer1.SplitterDistance = 509;
            this.splitContainer1.TabIndex = 10;
            // 
            // tabControlFile
            // 
            this.tabControlFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlFile.Location = new System.Drawing.Point(0, 0);
            this.tabControlFile.Name = "tabControlFile";
            this.tabControlFile.SelectedIndex = 0;
            this.tabControlFile.Size = new System.Drawing.Size(509, 575);
            this.tabControlFile.TabIndex = 3;
            // 
            // inputPropertyGrid
            // 
            this.inputPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputPropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.inputPropertyGrid.Name = "inputPropertyGrid";
            this.inputPropertyGrid.Size = new System.Drawing.Size(365, 575);
            this.inputPropertyGrid.TabIndex = 0;
            // 
            // QikCodeDocument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 650);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolstripTitle);
            this.Controls.Add(this.toolstripKeywords);
            this.Controls.Add(this.toolstripCommands);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "QikCodeDocument";
            this.Text = "QikCodeDocument";
            this.toolstripTitle.ResumeLayout(false);
            this.toolstripTitle.PerformLayout();
            this.toolstripKeywords.ResumeLayout(false);
            this.toolstripKeywords.PerformLayout();
            this.toolstripCommands.ResumeLayout(false);
            this.toolstripCommands.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
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
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControlFile;
        private System.Windows.Forms.ToolStripButton btnAddTemplate;
        private System.Windows.Forms.ToolStripButton btnRemoveTemplate;
        private System.Windows.Forms.ToolStripButton btnShowProperties;
        private System.Windows.Forms.ToolStripButton btnCompile;
        private Controls.InputPropertyGrid inputPropertyGrid;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnShowScript;
    }
}