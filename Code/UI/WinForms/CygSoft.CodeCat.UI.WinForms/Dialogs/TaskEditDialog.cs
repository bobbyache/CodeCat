using CygSoft.CodeCat.UI.WinForms.UiHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static CygSoft.CodeCat.UI.WinForms.Docked.TaskForm;

namespace CygSoft.CodeCat.UI.WinForms.Dialogs
{
    public partial class TaskEditDialog : Form
    {
        private Task task;

        public TaskEditDialog(Task task, string[] categories)
        {
            InitializeComponent();
            this.task = task;
            txtTitle.Text = task.Title;
            cboPriority.Items.AddRange(categories);
            cboPriority.Sorted = true;
            cboPriority.SelectedItem = task.Category;



            this.Shown += TaskEditDialog_Shown;
            btnOk.Click += btnOk_Click;
            btnCancel.Click += btnCancel_Click;
        }

        private void TaskEditDialog_Shown(object sender, EventArgs e)
        {
            txtTitle.SelectAll();
            txtTitle.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                task.Title = txtTitle.Text;
                task.Priority = TaskList.PriorityFromText(cboPriority.Text);
                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidateFields()
        {
            if (txtTitle.Text.Trim() == "")
            {
                Gui.Dialogs.NoInputValueForMandatoryField(this, "Title");
                return false;
            }
            return true;
        }
    }
}
