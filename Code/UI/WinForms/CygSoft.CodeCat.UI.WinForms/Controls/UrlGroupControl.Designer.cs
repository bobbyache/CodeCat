namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    partial class UrlGroupControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UrlGroupControl));
            this.syntaxDocument = new Alsing.SourceCode.SyntaxDocument(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtTitle = new CygSoft.CodeCat.UI.WinForms.ToolStripSpringTextBox();
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.cboFontSize = new System.Windows.Forms.ToolStripComboBox();
            this.lblEditStatus = new System.Windows.Forms.ToolStripLabel();
            this.urlListview = new System.Windows.Forms.ListView();
            this.colTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCreated = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colModified = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuNavigate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // syntaxDocument
            // 
            this.syntaxDocument.Lines = new string[] {
        ""};
            this.syntaxDocument.MaxUndoBufferSize = 1000;
            this.syntaxDocument.Modified = false;
            this.syntaxDocument.UndoStep = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.txtTitle,
            this.btnAdd,
            this.btnEdit,
            this.toolStripSeparator1,
            this.btnDelete});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(595, 25);
            this.toolStrip1.TabIndex = 7;
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
            this.txtTitle.Size = new System.Drawing.Size(412, 25);
            // 
            // btnAdd
            // 
            this.btnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(23, 22);
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(23, 22);
            this.btnEdit.Text = "Edit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnDelete
            // 
            this.btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(23, 22);
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.AutoSize = false;
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cboFontSize,
            this.lblEditStatus});
            this.toolStrip2.Location = new System.Drawing.Point(0, 405);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(595, 25);
            this.toolStrip2.TabIndex = 8;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // cboFontSize
            // 
            this.cboFontSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFontSize.Items.AddRange(new object[] {
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16"});
            this.cboFontSize.Name = "cboFontSize";
            this.cboFontSize.Size = new System.Drawing.Size(75, 25);
            // 
            // lblEditStatus
            // 
            this.lblEditStatus.AutoSize = false;
            this.lblEditStatus.Name = "lblEditStatus";
            this.lblEditStatus.Size = new System.Drawing.Size(100, 22);
            this.lblEditStatus.Text = "No Changes";
            this.lblEditStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // urlListview
            // 
            this.urlListview.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.urlListview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colTitle,
            this.colDescription,
            this.colCreated,
            this.colModified});
            this.urlListview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.urlListview.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.urlListview.HideSelection = false;
            this.urlListview.HotTracking = true;
            this.urlListview.HoverSelection = true;
            this.urlListview.Location = new System.Drawing.Point(0, 25);
            this.urlListview.Name = "urlListview";
            this.urlListview.Size = new System.Drawing.Size(595, 380);
            this.urlListview.TabIndex = 9;
            this.urlListview.UseCompatibleStateImageBehavior = false;
            this.urlListview.View = System.Windows.Forms.View.Details;
            this.urlListview.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.urlListview_ColumnClick);
            this.urlListview.MouseClick += new System.Windows.Forms.MouseEventHandler(this.urlListview_MouseClick);
            // 
            // colTitle
            // 
            this.colTitle.Text = "Title";
            this.colTitle.Width = 550;
            // 
            // colDescription
            // 
            this.colDescription.DisplayIndex = 3;
            this.colDescription.Text = "Description";
            this.colDescription.Width = 600;
            // 
            // colCreated
            // 
            this.colCreated.DisplayIndex = 1;
            this.colCreated.Text = "Created";
            this.colCreated.Width = 120;
            // 
            // colModified
            // 
            this.colModified.DisplayIndex = 2;
            this.colModified.Text = "Modified";
            this.colModified.Width = 120;
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNavigate,
            this.toolStripMenuItem1,
            this.mnuEdit,
            this.mnuDelete});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(131, 76);
            // 
            // mnuNavigate
            // 
            this.mnuNavigate.Name = "mnuNavigate";
            this.mnuNavigate.Size = new System.Drawing.Size(130, 22);
            this.mnuNavigate.Text = "Navigate...";
            this.mnuNavigate.Click += new System.EventHandler(this.mnuNavigate_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(127, 6);
            // 
            // mnuEdit
            // 
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.Size = new System.Drawing.Size(130, 22);
            this.mnuEdit.Text = "Edit";
            this.mnuEdit.Click += new System.EventHandler(this.mnuEdit_Click);
            // 
            // mnuDelete
            // 
            this.mnuDelete.Name = "mnuDelete";
            this.mnuDelete.Size = new System.Drawing.Size(130, 22);
            this.mnuDelete.Text = "Delete";
            this.mnuDelete.Click += new System.EventHandler(this.mnuDelete_Click);
            // 
            // UrlGroupControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.urlListview);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.toolStrip2);
            this.Name = "UrlGroupControl";
            this.Size = new System.Drawing.Size(595, 430);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Alsing.SourceCode.SyntaxDocument syntaxDocument;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private ToolStripSpringTextBox txtTitle;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripComboBox cboFontSize;
        private System.Windows.Forms.ToolStripLabel lblEditStatus;
        private System.Windows.Forms.ListView urlListview;
        private System.Windows.Forms.ColumnHeader colTitle;
        private System.Windows.Forms.ColumnHeader colCreated;
        private System.Windows.Forms.ColumnHeader colModified;
        private System.Windows.Forms.ColumnHeader colDescription;
        private System.Windows.Forms.ToolStripButton btnAdd;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuNavigate;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit;
        private System.Windows.Forms.ToolStripMenuItem mnuDelete;
    }
}
