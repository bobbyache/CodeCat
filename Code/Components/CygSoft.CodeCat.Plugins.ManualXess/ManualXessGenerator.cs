using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CygSoft.CodeCat.Plugins.Generators;

namespace CygSoft.CodeCat.Plugins.ManualXess
{
    public partial class ManualXessGenerator: UserControl, IGeneratorPlugin
    {
        public ManualXessGenerator()
        {
            InitializeComponent();

            //this.blueprintSyntaxBox.Document.SyntaxFile = "blueprint.syn";
            dataGridView.DataSource = workSet.Table;
        }

        public string Id
        {
            get
            {
                return "ManualXessGenerator";
            }
        }

        public string Title
        {
            get
            {
                return "Quick Adhoc Batch Generator";
            }
        }

        public event EventHandler Generated;

        private WorkSet workSet = new WorkSet();
        private bool loadingData = false;
        private bool selectAllInCell = true;

        private void GenerateText()
        {
            dataGridView.EndEdit();
            resultsSyntaxBox.Document.Text = workSet.GenerateText();
        }

        private void dataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (loadingData)
                dataGridView.BeginEdit(true);
            else
                dataGridView.BeginEdit(selectAllInCell);
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabResults)
            {
                GenerateText();
            }
        }

        private void blueprintSyntaxBox_Leave(object sender, EventArgs e)
        {
            workSet.BlueprintText = blueprintSyntaxBox.Document.Text;
            dataGridView.DataSource = workSet.Table;
            GenerateText();
        }

        private void blueprintSyntaxBox_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Control && e.KeyCode == Keys.Alt && e.KeyCode == Keys.V)
            if (e.KeyCode == Keys.V && e.Alt)
            {
                //MessageBox.Show("Pressed");
                blueprintSyntaxBox.Selection.Text = "@{" + blueprintSyntaxBox.Selection.Text + "}";
            }
        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            // http://stackoverflow.com/questions/4077260/copy-and-paste-multiple-cells-within-datagridview
            // http://www.codeproject.com/Articles/36850/DataGridView-Copy-and-Paste

        }

        private void mnuGridClear_Click(object sender, EventArgs e)
        {
            workSet.Table.Rows.Clear();
        }

        private void mnuGridClearSelectedRows_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in this.dataGridView.SelectedRows)
            {
                dataGridView.Rows.RemoveAt(item.Index);
            }
        }

        private void mnuGridRemoveOrphanedCols_Click(object sender, EventArgs e)
        {
            workSet.RemoveOrphanedColumns();
        }

        private void mnuSelectEntireCell_Click(object sender, EventArgs e)
        {
            //mnuSelectEntireCell.Checked = !mnuSelectEntireCell.Checked;
            selectAllInCell = mnuSelectEntireCell.Checked;
        }

        private void mnuToggleOrientation_Click(object sender, EventArgs e)
        {
            if (splitContainerMain.Orientation == Orientation.Horizontal)
                splitContainerMain.Orientation = Orientation.Vertical;
            else
                splitContainerMain.Orientation = Orientation.Horizontal;
        }
    }
}
