namespace CygSoft.CodeCat.PluginTests
{
    partial class TestBench
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
            this.pluginMenu = new System.Windows.Forms.MenuStrip();
            this.mnuPlugins = new System.Windows.Forms.ToolStripMenuItem();
            this.pluginPanel = new System.Windows.Forms.Panel();
            this.mnuXessManual = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSqlToCsString = new System.Windows.Forms.ToolStripMenuItem();
            this.txtCurrentPlugin = new System.Windows.Forms.ToolStripTextBox();
            this.pluginMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // pluginMenu
            // 
            this.pluginMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuPlugins,
            this.txtCurrentPlugin});
            this.pluginMenu.Location = new System.Drawing.Point(0, 0);
            this.pluginMenu.Name = "pluginMenu";
            this.pluginMenu.Size = new System.Drawing.Size(640, 27);
            this.pluginMenu.TabIndex = 0;
            this.pluginMenu.Text = "menuStrip1";
            // 
            // mnuPlugins
            // 
            this.mnuPlugins.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuXessManual,
            this.mnuSqlToCsString});
            this.mnuPlugins.Name = "mnuPlugins";
            this.mnuPlugins.Size = new System.Drawing.Size(58, 23);
            this.mnuPlugins.Text = "Plugins";
            // 
            // pluginPanel
            // 
            this.pluginPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pluginPanel.Location = new System.Drawing.Point(12, 27);
            this.pluginPanel.Name = "pluginPanel";
            this.pluginPanel.Size = new System.Drawing.Size(616, 333);
            this.pluginPanel.TabIndex = 1;
            // 
            // mnuXessManual
            // 
            this.mnuXessManual.Name = "mnuXessManual";
            this.mnuXessManual.Size = new System.Drawing.Size(227, 22);
            this.mnuXessManual.Text = "Xess Manual Generator";
            // 
            // mnuSqlToCsString
            // 
            this.mnuSqlToCsString.Name = "mnuSqlToCsString";
            this.mnuSqlToCsString.Size = new System.Drawing.Size(227, 22);
            this.mnuSqlToCsString.Text = "Unformatted Sql to C# String";
            // 
            // txtCurrentPlugin
            // 
            this.txtCurrentPlugin.Enabled = false;
            this.txtCurrentPlugin.Name = "txtCurrentPlugin";
            this.txtCurrentPlugin.Size = new System.Drawing.Size(400, 23);
            // 
            // TestBench
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 372);
            this.Controls.Add(this.pluginPanel);
            this.Controls.Add(this.pluginMenu);
            this.MainMenuStrip = this.pluginMenu;
            this.Name = "TestBench";
            this.Text = "Plugins Test Bench";
            this.pluginMenu.ResumeLayout(false);
            this.pluginMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip pluginMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuPlugins;
        private System.Windows.Forms.Panel pluginPanel;
        private System.Windows.Forms.ToolStripMenuItem mnuXessManual;
        private System.Windows.Forms.ToolStripMenuItem mnuSqlToCsString;
        private System.Windows.Forms.ToolStripTextBox txtCurrentPlugin;
    }
}

