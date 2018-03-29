namespace CygSoft.CodeCat.UI.WinForms.Controls.TopicSections
{
    partial class TaskListTopicSectionControl
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
            this.taskListControl1 = new CygSoft.CodeCat.UI.WinForms.Controls.TaskListControl();
            this.SuspendLayout();
            // 
            // taskListControl1
            // 
            this.taskListControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.taskListControl1.Location = new System.Drawing.Point(0, 25);
            this.taskListControl1.Name = "taskListControl1";
            this.taskListControl1.Size = new System.Drawing.Size(727, 463);
            this.taskListControl1.TabIndex = 7;
            // 
            // TaskListTopicSectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.taskListControl1);
            this.Name = "TaskListTopicSectionControl";
            this.Size = new System.Drawing.Size(727, 513);
            this.Controls.SetChildIndex(this.taskListControl1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TaskListControl taskListControl1;
    }
}
