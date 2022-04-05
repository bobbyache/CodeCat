using CygSoft.CodeCat.Domain.Management;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.ProjectExporter
{
    public partial class ProjectToFileTreeExporter : Form
    {
        public ProjectToFileTreeExporter()
        {
            InitializeComponent();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                ProjectFileExporter exporter = new ProjectFileExporter();
                exporter.Export(txtInput.Text, txtOutput.Text, txtKeywords.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, string.Format("Failed to load the projects.\n{0}", ex.Message), "Project Exporter", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
