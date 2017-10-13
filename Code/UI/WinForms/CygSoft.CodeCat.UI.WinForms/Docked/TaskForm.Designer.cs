namespace CygSoft.CodeCat.UI.WinForms.Docked
{
    partial class TaskForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNewTask = new System.Windows.Forms.ToolStripButton();
            this.btnDeleteTask = new System.Windows.Forms.ToolStripButton();
            this.btnEditTask = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.taskProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.lblTaskInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.listView = new System.Windows.Forms.ListView();
            this.colTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDateCreated = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuPriority = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPriorityHigh = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPriorityMedium = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPriorityLow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuNewTask = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditTask = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuDeleteTask = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cboFilter = new System.Windows.Forms.ToolStripComboBox();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.btnNewTask,
            this.btnEditTask,
            this.btnDeleteTask,
            this.cboFilter});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(382, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnNewTask
            // 
            this.btnNewTask.Image = ((System.Drawing.Image)(resources.GetObject("btnNewTask.Image")));
            this.btnNewTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewTask.Name = "btnNewTask";
            this.btnNewTask.Size = new System.Drawing.Size(77, 22);
            this.btnNewTask.Text = "New Task";
            // 
            // btnDeleteTask
            // 
            this.btnDeleteTask.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDeleteTask.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteTask.Image")));
            this.btnDeleteTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteTask.Name = "btnDeleteTask";
            this.btnDeleteTask.Size = new System.Drawing.Size(23, 22);
            this.btnDeleteTask.Text = "Delete Task";
            // 
            // btnEditTask
            // 
            this.btnEditTask.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEditTask.Image = ((System.Drawing.Image)(resources.GetObject("btnEditTask.Image")));
            this.btnEditTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditTask.Name = "btnEditTask";
            this.btnEditTask.Size = new System.Drawing.Size(23, 22);
            this.btnEditTask.Text = "Edit Task";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.taskProgressBar,
            this.lblTaskInfo});
            this.statusStrip1.Location = new System.Drawing.Point(0, 460);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(382, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // taskProgressBar
            // 
            this.taskProgressBar.Name = "taskProgressBar";
            this.taskProgressBar.Size = new System.Drawing.Size(100, 16);
            this.taskProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // lblTaskInfo
            // 
            this.lblTaskInfo.Name = "lblTaskInfo";
            this.lblTaskInfo.Size = new System.Drawing.Size(265, 17);
            this.lblTaskInfo.Spring = true;
            this.lblTaskInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listView
            // 
            this.listView.CheckBoxes = true;
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colTitle,
            this.colDateCreated});
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.Location = new System.Drawing.Point(0, 25);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(382, 435);
            this.listView.TabIndex = 3;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            // 
            // colTitle
            // 
            this.colTitle.Text = "Task";
            this.colTitle.Width = 376;
            // 
            // colDateCreated
            // 
            this.colDateCreated.Text = "Created";
            this.colDateCreated.Width = 200;
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuPriority,
            this.toolStripMenuItem1,
            this.mnuNewTask,
            this.mnuEditTask,
            this.toolStripMenuItem2,
            this.mnuDeleteTask});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(113, 104);
            // 
            // mnuPriority
            // 
            this.mnuPriority.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuPriorityHigh,
            this.mnuPriorityMedium,
            this.mnuPriorityLow});
            this.mnuPriority.Name = "mnuPriority";
            this.mnuPriority.Size = new System.Drawing.Size(112, 22);
            this.mnuPriority.Text = "Priority";
            // 
            // mnuPriorityHigh
            // 
            this.mnuPriorityHigh.Name = "mnuPriorityHigh";
            this.mnuPriorityHigh.Size = new System.Drawing.Size(119, 22);
            this.mnuPriorityHigh.Text = "High";
            // 
            // mnuPriorityMedium
            // 
            this.mnuPriorityMedium.Name = "mnuPriorityMedium";
            this.mnuPriorityMedium.Size = new System.Drawing.Size(119, 22);
            this.mnuPriorityMedium.Text = "Medium";
            // 
            // mnuPriorityLow
            // 
            this.mnuPriorityLow.Name = "mnuPriorityLow";
            this.mnuPriorityLow.Size = new System.Drawing.Size(119, 22);
            this.mnuPriorityLow.Text = "Low";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(109, 6);
            // 
            // mnuNewTask
            // 
            this.mnuNewTask.Name = "mnuNewTask";
            this.mnuNewTask.Size = new System.Drawing.Size(112, 22);
            this.mnuNewTask.Text = "New";
            // 
            // mnuEditTask
            // 
            this.mnuEditTask.Name = "mnuEditTask";
            this.mnuEditTask.Size = new System.Drawing.Size(112, 22);
            this.mnuEditTask.Text = "Edit";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(109, 6);
            // 
            // mnuDeleteTask
            // 
            this.mnuDeleteTask.Name = "mnuDeleteTask";
            this.mnuDeleteTask.Size = new System.Drawing.Size(112, 22);
            this.mnuDeleteTask.Text = "Delete";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // cboFilter
            // 
            this.cboFilter.Name = "cboFilter";
            this.cboFilter.Size = new System.Drawing.Size(121, 25);
            // 
            // TaskForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 482);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "TaskForm";
            this.Text = "Current Tasks";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ToolStripButton btnNewTask;
        private System.Windows.Forms.ToolStripButton btnDeleteTask;
        private System.Windows.Forms.ToolStripButton btnEditTask;
        private System.Windows.Forms.ColumnHeader colTitle;
        private System.Windows.Forms.ColumnHeader colDateCreated;
        private System.Windows.Forms.ToolStripProgressBar taskProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel lblTaskInfo;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuPriority;
        private System.Windows.Forms.ToolStripMenuItem mnuPriorityHigh;
        private System.Windows.Forms.ToolStripMenuItem mnuPriorityMedium;
        private System.Windows.Forms.ToolStripMenuItem mnuPriorityLow;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuNewTask;
        private System.Windows.Forms.ToolStripMenuItem mnuEditTask;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mnuDeleteTask;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripComboBox cboFilter;
    }
}