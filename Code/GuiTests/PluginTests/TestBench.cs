using CygSoft.CodeCat.Plugins.Generators;
using CygSoft.CodeCat.Plugins.ManualXess;
using CygSoft.CodeCat.Plugins.SqlExtraction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CygSoft.CodeCat.PluginTests
{
    public partial class TestBench : Form
    {
        private Dictionary<string, UserControl> pluginsDictionary = new Dictionary<string, UserControl>();

        public TestBench()
        {
            InitializeComponent();

            foreach (ToolStripMenuItem item in mnuPlugins.DropDownItems)
                item.Click += PluginMenu_Click;

        }

        private void PluginMenu_Click(object sender, EventArgs e)
        {
            UserControl userControl = null;
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            string menuName = menuItem.Name;

            switch (menuName)
            {
                case "mnuXessManual":
                    userControl = CreatePluginControl<ManualXessGenerator>(menuName);
                    break;

                case "mnuSqlToCsString":
                    userControl = CreatePluginControl<SqlToCSharpStringGenerator>(menuName);
                    break;

                case "mnuXmlFormatter":
                    userControl = CreatePluginControl<Plugins.XmlFormatter.Formatter>(menuName);
                    break;

                case "mnuTSqlFormatter":
                    userControl = CreatePluginControl<Plugins.TSqlFormatter.Formatter>(menuName);
                    break;
            }

            if (userControl != null)
            {
                DisplayPlugin(menuName, userControl);
                UncheckMenus();
                CheckMenu(menuItem);
            }
        }

        private UserControl CreatePluginControl<T>(string id) where T : IGeneratorPlugin, new()
        {
            UserControl userControl;
            if (!pluginsDictionary.ContainsKey(id))
                pluginsDictionary.Add(id, (new T()) as UserControl);
            userControl = pluginsDictionary[id];

            return userControl;
        }

        private void UncheckMenus()
        {
            foreach (ToolStripMenuItem item in mnuPlugins.DropDownItems)
            {
                item.Checked = false;
            }
        }

        private void CheckMenu(ToolStripMenuItem menu)
        {
            menu.Checked = true;
        }

        private void DisplayPlugin(string name, UserControl userControl)
        {

            this.pluginPanel.Controls.Clear();
            this.pluginPanel.Controls.Add(userControl);
            userControl.Dock = DockStyle.Fill;

            txtCurrentPlugin.Text = ((IGeneratorPlugin)userControl).Title;

            foreach (ToolStripItem item in pluginMenu.Items)
            {
                if (!(item is ToolStripTextBox))
                    ((ToolStripMenuItem)item).Checked = item.Name == txtCurrentPlugin.Text ? true : false;
            }

        }
    }
}
