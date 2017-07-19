using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.TaskListing.Infrastructure;
using CygSoft.CodeCat.UI.WinForms.Dialogs;
using CygSoft.CodeCat.UI.WinForms.UiHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace CygSoft.CodeCat.UI.WinForms.Docked
{
    public partial class TaskForm : DockContent
    {
        private AppFacade application;

        public TaskForm(AppFacade application)
        {
            InitializeComponent();
            if (application == null)
                throw new ArgumentNullException("Application is a required constructor parameter and cannot be null");
            this.application = application;
            this.Shown += TaskForm_Shown;
        }

        private void TaskForm_Shown(object sender, EventArgs e)
        {
            application.LoadTasks();
            LoadTaskList();
        }

        private void ListView_ItemCheck(object sender, ItemCheckEventArgs e)
        {

            if (e.CurrentValue != e.NewValue)
            {
                ListViewItem item = listView.Items[e.Index];
                if (item != null)
                {
                    bool itemChecked = e.NewValue == CheckState.Checked;
                    ITask checkedTask = item.Tag as ITask;
                    checkedTask.Completed = itemChecked;
                    FormatTaskItem(item, itemChecked);
                    application.SaveTasks();
                }
            }
        }


        private void FormatTaskItem(ListViewItem item)
        {
            bool isCompleted = item.Checked;
            FormatTaskItem(item, isCompleted);
        }

        private void FormatTaskItem(ListViewItem item, bool isCompleted)
        {
            item.Font = isCompleted ? new Font(item.Font, FontStyle.Strikeout) : new Font(item.Font, FontStyle.Regular);
            item.ForeColor = isCompleted ? Color.Gray : Color.Black;
        }

        private void LoadTaskList()
        {
            listView.ItemCheck -= ListView_ItemCheck;
            Gui.GroupedListView.LoadAllItems(listView, application.CurrrentTasks, application.TaskPriorities, CreateListviewItem, true);
            listView.ItemCheck += ListView_ItemCheck;
        }

        private ListViewItem CreateListviewItem(ListView listView, ITask item, bool select = false)
        {
            ListViewItem listItem = new ListViewItem();

            //listItem.Name = item.Id;
            //listItem.ImageKey = item.FileExtension;
            listItem.Checked = item.Completed ? true : false;
            FormatTaskItem(listItem);
            listItem.Tag = item;
            listItem.ToolTipText = item.Title;
            listItem.Text = item.Title;
            listItem.Group = listView.Groups[item.Category];
            listView.Items.Add(listItem);

            return listItem;
        }

        private void btnNewTask_Click(object sender, EventArgs e)
        {
            ITask task = application.CreateTask();
            TaskEditDialog dialog = new TaskEditDialog(application, task, application.TaskPriorities);
            DialogResult result = dialog.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                application.AddTask(task);
                application.SaveTasks();
                CreateListviewItem(listView, task, true);
            }
        }

        private void btnDeleteTask_Click(object sender, EventArgs e)
        {
            IEnumerable<ITask> tasks = Gui.GroupedListView.SelectedItems<ITask>(listView);
            application.DeleteTasks(tasks.ToArray());
            application.SaveTasks();
            LoadTaskList();
        }

        private void btnEditTask_Click(object sender, EventArgs e)
        {
            ITask task = Gui.GroupedListView.SelectedItem<ITask>(listView);

            if (task != null)
            {
                TaskEditDialog dialog = new TaskEditDialog(application, task, application.TaskPriorities);
                DialogResult result = dialog.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    application.SaveTasks();
                    listView.SelectedItems[0].Text = task.Title;
                    listView.SelectedItems[0].Group = listView.Groups[task.Category];
                }
            }
        }
    }
}
