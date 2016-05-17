namespace CygSoft.CodeCat.UI.WinForms
{
    partial class InvalidRemoveDialog
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
            this.btnOk = new System.Windows.Forms.Button();
            this.lstInvalidItems = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(436, 144);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 7;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // lstInvalidItems
            // 
            this.lstInvalidItems.FormattingEnabled = true;
            this.lstInvalidItems.Location = new System.Drawing.Point(12, 43);
            this.lstInvalidItems.Name = "lstInvalidItems";
            this.lstInvalidItems.Size = new System.Drawing.Size(499, 95);
            this.lstInvalidItems.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(499, 31);
            this.label1.TabIndex = 5;
            this.label1.Text = "The following snippets will not have keywords based on this removal operation. Th" +
    "e operation is therefore denied.";
            // 
            // InvalidRemoveDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 172);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lstInvalidItems);
            this.Controls.Add(this.label1);
            this.Name = "InvalidRemoveDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Invalid Removal Operation";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ListBox lstInvalidItems;
        private System.Windows.Forms.Label label1;
    }
}