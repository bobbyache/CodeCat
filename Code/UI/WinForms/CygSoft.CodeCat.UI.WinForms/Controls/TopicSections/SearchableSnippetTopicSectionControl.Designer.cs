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
            this.searchPanel = new System.Windows.Forms.Panel();
            this.btnFind = new System.Windows.Forms.Button();
            this.keywordsLabel = new System.Windows.Forms.Label();
            this.keywordsTextBox = new System.Windows.Forms.TextBox();
            this.searchPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchPanel
            // 
            this.searchPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchPanel.Controls.Add(this.btnFind);
            this.searchPanel.Controls.Add(this.keywordsLabel);
            this.searchPanel.Controls.Add(this.keywordsTextBox);
            this.searchPanel.Location = new System.Drawing.Point(2, 27);
            this.searchPanel.Margin = new System.Windows.Forms.Padding(2);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(686, 21);
            this.searchPanel.TabIndex = 7;
            // 
            // btnFind
            // 
            this.btnFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFind.Location = new System.Drawing.Point(647, 2);
            this.btnFind.Margin = new System.Windows.Forms.Padding(2);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(35, 18);
            this.btnFind.TabIndex = 4;
            this.btnFind.UseVisualStyleBackColor = true;
            // 
            // keywordsLabel
            // 
            this.keywordsLabel.AutoSize = true;
            this.keywordsLabel.Location = new System.Drawing.Point(9, 5);
            this.keywordsLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.keywordsLabel.Name = "keywordsLabel";
            this.keywordsLabel.Size = new System.Drawing.Size(53, 13);
            this.keywordsLabel.TabIndex = 1;
            this.keywordsLabel.Text = "Keywords";
            // 
            // keywordsTextBox
            // 
            this.keywordsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.keywordsTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.keywordsTextBox.Location = new System.Drawing.Point(64, 1);
            this.keywordsTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.keywordsTextBox.Name = "keywordsTextBox";
            this.keywordsTextBox.Size = new System.Drawing.Size(581, 20);
            this.keywordsTextBox.TabIndex = 0;
            // 
            // SearchableSnippetTopicSectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.searchPanel);
            this.Name = "SearchableSnippetTopicSectionControl";
            this.Size = new System.Drawing.Size(688, 463);
            this.Controls.SetChildIndex(this.searchPanel, 0);
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel searchPanel;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Label keywordsLabel;
        private System.Windows.Forms.TextBox keywordsTextBox;
    }
}
