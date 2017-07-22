namespace CygSoft.CodeCat.UI.WinForms.Docked
{
    partial class CategoryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CategoryForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnAddCategory = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnAddCategoryAsSibling = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAddCategoryAsChild = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAddCategoryItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.categoryTreeControl1 = new CygSoft.CodeCat.UI.WinForms.Controls.CategoryTreeControl();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddCategory,
            this.btnDelete});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(455, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnAddCategory
            // 
            this.btnAddCategory.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddCategoryItem,
            this.toolStripMenuItem1,
            this.btnAddCategoryAsSibling,
            this.btnAddCategoryAsChild});
            this.btnAddCategory.Image = ((System.Drawing.Image)(resources.GetObject("btnAddCategory.Image")));
            this.btnAddCategory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddCategory.Name = "btnAddCategory";
            this.btnAddCategory.Size = new System.Drawing.Size(58, 22);
            this.btnAddCategory.Text = "Add";
            // 
            // btnAddCategoryAsSibling
            // 
            this.btnAddCategoryAsSibling.Name = "btnAddCategoryAsSibling";
            this.btnAddCategoryAsSibling.Size = new System.Drawing.Size(161, 22);
            this.btnAddCategoryAsSibling.Text = "Sibling Category";
            this.btnAddCategoryAsSibling.Click += new System.EventHandler(this.btnAddCategoryAsSibling_Click);
            // 
            // btnAddCategoryAsChild
            // 
            this.btnAddCategoryAsChild.Name = "btnAddCategoryAsChild";
            this.btnAddCategoryAsChild.Size = new System.Drawing.Size(161, 22);
            this.btnAddCategoryAsChild.Text = "Child Category";
            this.btnAddCategoryAsChild.Click += new System.EventHandler(this.btnAddCategoryAsChild_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(158, 6);
            // 
            // btnAddCategoryItem
            // 
            this.btnAddCategoryItem.Name = "btnAddCategoryItem";
            this.btnAddCategoryItem.Size = new System.Drawing.Size(161, 22);
            this.btnAddCategoryItem.Text = "Add Item(s)";
            this.btnAddCategoryItem.Click += new System.EventHandler(this.btnAddCategoryItem_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(60, 22);
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 239);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(455, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // categoryTreeControl1
            // 
            this.categoryTreeControl1.AllowDrop = true;
            this.categoryTreeControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.categoryTreeControl1.Location = new System.Drawing.Point(0, 25);
            this.categoryTreeControl1.Name = "categoryTreeControl1";
            this.categoryTreeControl1.Size = new System.Drawing.Size(455, 214);
            this.categoryTreeControl1.TabIndex = 2;
            // 
            // CategoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 261);
            this.Controls.Add(this.categoryTreeControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "CategoryForm";
            this.Text = "CategoryForm";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private Controls.CategoryTreeControl categoryTreeControl1;
        private System.Windows.Forms.ToolStripDropDownButton btnAddCategory;
        private System.Windows.Forms.ToolStripMenuItem btnAddCategoryAsSibling;
        private System.Windows.Forms.ToolStripMenuItem btnAddCategoryAsChild;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem btnAddCategoryItem;
    }
}