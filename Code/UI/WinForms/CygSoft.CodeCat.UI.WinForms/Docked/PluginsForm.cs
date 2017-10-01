using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Plugins.Generators;
using CygSoft.CodeCat.UI.WinForms.UiHelpers;
using System;
using System.IO;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace CygSoft.CodeCat.UI.WinForms.Docked
{
    public partial class PluginsForm :  DockContent
    {
        private AppFacade application;
        private GeneratorPlugins generatorPlugins;

        public PluginsForm(AppFacade application)
        {
            if (application == null)
                throw new ArgumentException("Application must be provided.");

            InitializeComponent();

            this.application = application;
            this.Text = "Generators";

            LoadPlugins();
        }

        private void LoadPlugins()
        {
            try
            {
                generatorPlugins = new GeneratorPlugins(ConfigSettings.PluginsFolder);
                generatorPlugins.LoadPlugins();

                mnuGenerators.DropDownItems.Clear();

                foreach (GeneratorPlugins.PluginListItem item in generatorPlugins.PluginItems)
                {
                    ToolStripMenuItem menuItem = new ToolStripMenuItem(item.Title);
                    menuItem.Name = item.Id;
                    menuItem.Click += MenuItem_Click;
                    mnuGenerators.DropDownItems.Add(menuItem);
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                Gui.Dialogs.PluginFolderNotFoundNotification(this, ex);
            }
        }

        private void MenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            UserControl pluginControl = generatorPlugins.GetPlugin(item.Name) as UserControl;
            uiPluginPanel.Controls.Clear();
            uiPluginPanel.Controls.Add(pluginControl);
            pluginControl.Dock = DockStyle.Fill;
            currentGenerator.Text = item.Text;
        }
    }
}
