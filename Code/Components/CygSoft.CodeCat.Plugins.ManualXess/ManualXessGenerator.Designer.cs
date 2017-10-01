namespace CygSoft.CodeCat.Plugins.ManualXess
{
    partial class ManualXessGenerator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManualXessGenerator));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuGridClear = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGridClearSelectedRows = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuGridRemoveOrphanedCols = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSelectEntireCell = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuToggleOrientation = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.blueprintSyntaxBox = new Alsing.Windows.Forms.SyntaxBoxControl();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabGrid = new System.Windows.Forms.TabPage();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.tabResults = new System.Windows.Forms.TabPage();
            this.resultsSyntaxBox = new Alsing.Windows.Forms.SyntaxBoxControl();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.tabResults.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripDropDownButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1079, 27);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGridClear,
            this.mnuGridClearSelectedRows,
            this.toolStripMenuItem1,
            this.mnuGridRemoveOrphanedCols,
            this.toolStripMenuItem2,
            this.mnuSelectEntireCell});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(51, 24);
            this.toolStripDropDownButton1.Text = "Grid";
            // 
            // mnuGridClear
            // 
            this.mnuGridClear.Name = "mnuGridClear";
            this.mnuGridClear.Size = new System.Drawing.Size(239, 26);
            this.mnuGridClear.Text = "Clear";
            this.mnuGridClear.Click += new System.EventHandler(this.mnuGridClear_Click);
            // 
            // mnuGridClearSelectedRows
            // 
            this.mnuGridClearSelectedRows.Name = "mnuGridClearSelectedRows";
            this.mnuGridClearSelectedRows.Size = new System.Drawing.Size(239, 26);
            this.mnuGridClearSelectedRows.Text = "Clear Selected Rows";
            this.mnuGridClearSelectedRows.Click += new System.EventHandler(this.mnuGridClearSelectedRows_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(236, 6);
            // 
            // mnuGridRemoveOrphanedCols
            // 
            this.mnuGridRemoveOrphanedCols.Name = "mnuGridRemoveOrphanedCols";
            this.mnuGridRemoveOrphanedCols.Size = new System.Drawing.Size(239, 26);
            this.mnuGridRemoveOrphanedCols.Text = "Remove Orpaned Rows";
            this.mnuGridRemoveOrphanedCols.Click += new System.EventHandler(this.mnuGridRemoveOrphanedCols_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(236, 6);
            // 
            // mnuSelectEntireCell
            // 
            this.mnuSelectEntireCell.Checked = true;
            this.mnuSelectEntireCell.CheckOnClick = true;
            this.mnuSelectEntireCell.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuSelectEntireCell.Name = "mnuSelectEntireCell";
            this.mnuSelectEntireCell.Size = new System.Drawing.Size(239, 26);
            this.mnuSelectEntireCell.Text = "Select Entire Cell";
            this.mnuSelectEntireCell.Click += new System.EventHandler(this.mnuSelectEntireCell_Click);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuToggleOrientation});
            this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(75, 24);
            this.toolStripDropDownButton2.Text = "Options";
            // 
            // mnuToggleOrientation
            // 
            this.mnuToggleOrientation.Name = "mnuToggleOrientation";
            this.mnuToggleOrientation.Size = new System.Drawing.Size(209, 26);
            this.mnuToggleOrientation.Text = "Toggle Orientation";
            this.mnuToggleOrientation.Click += new System.EventHandler(this.mnuToggleOrientation_Click);
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 27);
            this.splitContainerMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.blueprintSyntaxBox);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.tabControl);
            this.splitContainerMain.Size = new System.Drawing.Size(1079, 607);
            this.splitContainerMain.SplitterDistance = 227;
            this.splitContainerMain.SplitterWidth = 5;
            this.splitContainerMain.TabIndex = 6;
            // 
            // blueprintSyntaxBox
            // 
            this.blueprintSyntaxBox.ActiveView = Alsing.Windows.Forms.ActiveView.BottomRight;
            this.blueprintSyntaxBox.AutoListPosition = null;
            this.blueprintSyntaxBox.AutoListSelectedText = "a123";
            this.blueprintSyntaxBox.AutoListVisible = false;
            this.blueprintSyntaxBox.BackColor = System.Drawing.Color.White;
            this.blueprintSyntaxBox.BorderStyle = Alsing.Windows.Forms.BorderStyle.None;
            this.blueprintSyntaxBox.CopyAsRTF = false;
            this.blueprintSyntaxBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.blueprintSyntaxBox.FontName = "Courier new";
            this.blueprintSyntaxBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.blueprintSyntaxBox.InfoTipCount = 1;
            this.blueprintSyntaxBox.InfoTipPosition = null;
            this.blueprintSyntaxBox.InfoTipSelectedIndex = 1;
            this.blueprintSyntaxBox.InfoTipVisible = false;
            this.blueprintSyntaxBox.Location = new System.Drawing.Point(0, 0);
            this.blueprintSyntaxBox.LockCursorUpdate = false;
            this.blueprintSyntaxBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.blueprintSyntaxBox.Name = "blueprintSyntaxBox";
            this.blueprintSyntaxBox.ShowScopeIndicator = false;
            this.blueprintSyntaxBox.Size = new System.Drawing.Size(1079, 227);
            this.blueprintSyntaxBox.SmoothScroll = false;
            this.blueprintSyntaxBox.SplitView = false;
            this.blueprintSyntaxBox.SplitviewH = -5;
            this.blueprintSyntaxBox.SplitviewV = -5;
            this.blueprintSyntaxBox.TabGuideColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(233)))), ((int)(((byte)(233)))));
            this.blueprintSyntaxBox.TabIndex = 1;
            this.blueprintSyntaxBox.Text = "syntaxBoxControl1";
            this.blueprintSyntaxBox.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
            this.blueprintSyntaxBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.blueprintSyntaxBox_KeyDown);
            this.blueprintSyntaxBox.Leave += new System.EventHandler(this.blueprintSyntaxBox_Leave);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabGrid);
            this.tabControl.Controls.Add(this.tabResults);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1079, 375);
            this.tabControl.TabIndex = 1;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabGrid
            // 
            this.tabGrid.Controls.Add(this.dataGridView);
            this.tabGrid.Location = new System.Drawing.Point(4, 25);
            this.tabGrid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabGrid.Name = "tabGrid";
            this.tabGrid.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabGrid.Size = new System.Drawing.Size(1071, 346);
            this.tabGrid.TabIndex = 0;
            this.tabGrid.Text = "Data Grid";
            this.tabGrid.UseVisualStyleBackColor = true;
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(4, 4);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(1063, 338);
            this.dataGridView.TabIndex = 1;
            this.dataGridView.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellEnter);
            // 
            // tabResults
            // 
            this.tabResults.Controls.Add(this.resultsSyntaxBox);
            this.tabResults.Location = new System.Drawing.Point(4, 25);
            this.tabResults.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabResults.Name = "tabResults";
            this.tabResults.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabResults.Size = new System.Drawing.Size(1071, 343);
            this.tabResults.TabIndex = 1;
            this.tabResults.Text = "Generated Text";
            this.tabResults.UseVisualStyleBackColor = true;
            // 
            // resultsSyntaxBox
            // 
            this.resultsSyntaxBox.ActiveView = Alsing.Windows.Forms.ActiveView.BottomRight;
            this.resultsSyntaxBox.AutoListPosition = null;
            this.resultsSyntaxBox.AutoListSelectedText = "a123";
            this.resultsSyntaxBox.AutoListVisible = false;
            this.resultsSyntaxBox.BackColor = System.Drawing.Color.White;
            this.resultsSyntaxBox.BorderStyle = Alsing.Windows.Forms.BorderStyle.None;
            this.resultsSyntaxBox.CopyAsRTF = false;
            this.resultsSyntaxBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultsSyntaxBox.FontName = "Courier new";
            this.resultsSyntaxBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.resultsSyntaxBox.InfoTipCount = 1;
            this.resultsSyntaxBox.InfoTipPosition = null;
            this.resultsSyntaxBox.InfoTipSelectedIndex = 1;
            this.resultsSyntaxBox.InfoTipVisible = false;
            this.resultsSyntaxBox.Location = new System.Drawing.Point(4, 4);
            this.resultsSyntaxBox.LockCursorUpdate = false;
            this.resultsSyntaxBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.resultsSyntaxBox.Name = "resultsSyntaxBox";
            this.resultsSyntaxBox.ShowScopeIndicator = false;
            this.resultsSyntaxBox.Size = new System.Drawing.Size(1063, 335);
            this.resultsSyntaxBox.SmoothScroll = false;
            this.resultsSyntaxBox.SplitView = false;
            this.resultsSyntaxBox.SplitviewH = -5;
            this.resultsSyntaxBox.SplitviewV = -5;
            this.resultsSyntaxBox.TabGuideColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(233)))), ((int)(((byte)(233)))));
            this.resultsSyntaxBox.TabIndex = 0;
            this.resultsSyntaxBox.Text = "syntaxBoxControl1";
            this.resultsSyntaxBox.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
            // 
            // ManualXessGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ManualXessGenerator";
            this.Size = new System.Drawing.Size(1079, 634);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.tabResults.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private Alsing.Windows.Forms.SyntaxBoxControl blueprintSyntaxBox;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabGrid;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.TabPage tabResults;
        private Alsing.Windows.Forms.SyntaxBoxControl resultsSyntaxBox;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem mnuGridClear;
        private System.Windows.Forms.ToolStripMenuItem mnuGridClearSelectedRows;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuGridRemoveOrphanedCols;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mnuSelectEntireCell;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem mnuToggleOrientation;
    }
}
