using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Infrastructure.Graphics;
using CygSoft.CodeCat.TaskListing.Infrastructure;
using CygSoft.CodeCat.UI.WinForms.Dialogs;
using CygSoft.CodeCat.UI.WinForms.UiHelpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace CygSoft.CodeCat.UI.WinForms.Docked
{
    public partial class TaskForm : DockContent
    {
        private IAppFacade application;
        private IImageResources imageResources;

        public TaskForm(IAppFacade application, IImageResources imageResources)
        {
            InitializeComponent();

            Icon = Gui.Drawing.IconFromImage(imageResources.GetImage(ImageKeys.EditText));
            HideOnClose = true;
            DockAreas = DockAreas.DockLeft | DockAreas.DockRight;

            btnNewTask.Image = imageResources.GetImage(ImageKeys.AddSnippet);
            btnEditTask.Image = imageResources.GetImage(ImageKeys.EditSnippet);
            btnDeleteTask.Image = imageResources.GetImage(ImageKeys.DeleteSnippet);

            taskProgressBar.Maximum = 100;
            btnNewTask.Enabled = false;
            btnEditTask.Enabled = false;
            btnDeleteTask.Enabled = false;

            if (imageResources == null)
                throw new ArgumentNullException("Image Resources is a required constructor parameter and cannot be null");

            this.imageResources = imageResources;

            if (application == null)
                throw new ArgumentNullException("Application is a required constructor parameter and cannot be null");

            this.application = application;

            listView.MouseClick += listView_MouseClick;
            
            btnNewTask.Click += (s,e) => NewTask();
            btnEditTask.Click += (s, e) => EditTask();
            btnDeleteTask.Click += (s, e) => DeleteTask();
            mnuPriority.DropDownOpening += mnuPriority_DropDownOpening;
            mnuDeleteTask.Click += (s, e) => DeleteTask();
            mnuEditTask.Click += (s, e) => EditTask();
            mnuNewTask.Click += (s, e) => NewTask();
            mnuPriorityHigh.Click += (s, e) => ChangePriority(TaskPriority.High);
            mnuPriorityMedium.Click += (s, e) => ChangePriority(TaskPriority.Medium);
            mnuPriorityLow.Click += (s, e) => ChangePriority(TaskPriority.Low);
        }

        private void mnuPriority_DropDownOpening(object sender, EventArgs e)
        {
            ITask task = Gui.GroupedListView.SelectedItem<ITask>(listView);
            if (task != null)
                CheckPriorityMenuItem(task.Priority);
        }

        private void listView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                EnableContextMenuItems(listView.FocusedItem.Bounds.Contains(e.Location));
                contextMenu.Show(Cursor.Position);
            }
        }

        private void EnableContextMenuItems(bool singleItemSelected)
        {
            mnuPriority.Enabled = singleItemSelected;
            mnuDeleteTask.Enabled = singleItemSelected;
            mnuEditTask.Enabled = singleItemSelected;
        }

        public void LoadTasks()
        {
            application.LoadTasks();
            LoadTaskList();

            btnNewTask.Enabled = true;
            btnEditTask.Enabled = true;
            btnDeleteTask.Enabled = true;
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
                    DisplayStatusInformation();
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
            DisplayStatusInformation();
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
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, item.DateCreated.ToLongDateString() + ", " + item.DateCreated.ToLongTimeString()));
            listView.Items.Add(listItem);

            return listItem;
        }

        private void NewTask()
        {
            ITask task = application.CreateTask();
            TaskEditDialog dialog = new TaskEditDialog(application, task, application.TaskPriorities);
            DialogResult result = dialog.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                application.AddTask(task);
                application.SaveTasks();
                CreateListviewItem(listView, task, true);
                DisplayStatusInformation();
            }
        }

        private void DeleteTask()
        {
            if (Gui.GroupedListView.ItemsSelected<ITask>(listView))
            {
                IEnumerable<ITask> tasks = Gui.GroupedListView.SelectedItems<ITask>(listView);
                application.DeleteTasks(tasks.ToArray());
                application.SaveTasks();
                LoadTaskList();
                DisplayStatusInformation();
            }
        }

        private void EditTask()
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
                    DisplayStatusInformation();
                }
            }
        }

        private void DisplayStatusInformation()
        {
            taskProgressBar.Value = application.PercentageOfTasksCompleted();
            lblTaskInfo.Text = application.CurrentTaskInformation();
        }

        private void CheckPriorityMenuItem(TaskPriority priority)
        {
            mnuPriorityHigh.Checked = priority == TaskPriority.High;
            mnuPriorityMedium.Checked = priority == TaskPriority.Medium;
            mnuPriorityLow.Checked = priority == TaskPriority.Low;
        }

        private void ChangePriority(TaskPriority priority)
        {
            ITask task = Gui.GroupedListView.SelectedItem<ITask>(listView);
            if (task != null)
            {
                task.Priority = priority;
                application.SaveTasks();
                listView.SelectedItems[0].Text = task.Title;
                listView.SelectedItems[0].Group = listView.Groups[task.Category];
                DisplayStatusInformation();
            }
        }
    }
}
