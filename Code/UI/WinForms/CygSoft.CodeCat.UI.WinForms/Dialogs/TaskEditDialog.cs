using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.TaskListing.Infrastructure;
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
        private ITask task;
        IAppFacade application;

        public TaskEditDialog(IAppFacade application, ITask task, string[] categories)
        {
            InitializeComponent();

            if (application == null)
                throw new ArgumentNullException("Application is a required constructor parameter and cannot be null");
            this.application = application;

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
                task.Priority = application.TaskPriorityFromText(cboPriority.Text);
                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidateFields()
        {
            if (txtTitle.Text.Trim() == "")
            {
                Gui.Dialogs.MissingRequiredFieldMessageBox(this, "Title");
                return false;
            }
            return true;
        }
    }
}
