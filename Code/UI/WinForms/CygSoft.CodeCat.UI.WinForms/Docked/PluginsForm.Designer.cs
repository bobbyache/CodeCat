namespace CygSoft.CodeCat.UI.WinForms.Docked
{
    partial class PluginsForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuGenerators = new System.Windows.Forms.ToolStripMenuItem();
            this.currentGenerator = new CygSoft.CodeCat.UI.WinForms.ToolStripSpringTextBox();
            this.uiPluginPanel = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGenerators,
            this.currentGenerator});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(353, 31);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuGenerators
            // 
            this.mnuGenerators.Name = "mnuGenerators";
            this.mnuGenerators.Size = new System.Drawing.Size(93, 27);
            this.mnuGenerators.Text = "Generators";
            // 
            // currentGenerator
            // 
            this.currentGenerator.Enabled = false;
            this.currentGenerator.Name = "currentGenerator";
            this.currentGenerator.Size = new System.Drawing.Size(137, 27);
            // 
            // uiPluginPanel
            // 
            this.uiPluginPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPluginPanel.Location = new System.Drawing.Point(0, 31);
            this.uiPluginPanel.Name = "uiPluginPanel";
            this.uiPluginPanel.Size = new System.Drawing.Size(353, 289);
            this.uiPluginPanel.TabIndex = 2;
            // 
            // GeneratorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 320);
            this.Controls.Add(this.uiPluginPanel);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GeneratorForm";
            this.Text = "GeneratorForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuGenerators;
        private System.Windows.Forms.Panel uiPluginPanel;
        private ToolStripSpringTextBox currentGenerator;
    }
}