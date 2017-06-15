namespace CygSoft.CodeCat.UI.WinForms.Controls.TopicSections
{
    partial class FileAttachmentsTopicSectionControl
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
            this.fileListview = new System.Windows.Forms.ListView();
            this.colTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCreated = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colModified = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // fileListview
            // 
            this.fileListview.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.fileListview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colTitle,
            this.colDescription,
            this.colFileName,
            this.colCreated,
            this.colModified});
            this.fileListview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileListview.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileListview.FullRowSelect = true;
            this.fileListview.HideSelection = false;
            this.fileListview.Location = new System.Drawing.Point(0, 25);
            this.fileListview.Name = "fileListview";
            this.fileListview.Size = new System.Drawing.Size(429, 389);
            this.fileListview.TabIndex = 13;
            this.fileListview.UseCompatibleStateImageBehavior = false;
            this.fileListview.View = System.Windows.Forms.View.Details;
            // 
            // colTitle
            // 
            this.colTitle.Text = "Title";
            this.colTitle.Width = 550;
            // 
            // colDescription
            // 
            this.colDescription.Text = "Description";
            this.colDescription.Width = 600;
            // 
            // colFileName
            // 
            this.colFileName.Text = "File Name";
            this.colFileName.Width = 180;
            // 
            // colCreated
            // 
            this.colCreated.Text = "Created";
            this.colCreated.Width = 120;
            // 
            // colModified
            // 
            this.colModified.Text = "Modified";
            this.colModified.Width = 120;
            // 
            // FileAttachmentsTopicSectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fileListview);
            this.Name = "FileAttachmentsTopicSectionControl";
            this.Size = new System.Drawing.Size(429, 439);
            this.Controls.SetChildIndex(this.fileListview, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView fileListview;
        private System.Windows.Forms.ColumnHeader colTitle;
        private System.Windows.Forms.ColumnHeader colDescription;
        private System.Windows.Forms.ColumnHeader colFileName;
        private System.Windows.Forms.ColumnHeader colCreated;
        private System.Windows.Forms.ColumnHeader colModified;
    }
}
