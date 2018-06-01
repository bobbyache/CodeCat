using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Infrastructure.TopicSections;
using CygSoft.CodeCat.UI.WinForms.UiHelpers;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public partial class TaskListControl : UserControl
    {
        public event EventHandler NewTask;
        public event EventHandler<TaskEventArgs> EditingTask;
        public event EventHandler<TaskListEventArgs> DeletingTasks;
        public event EventHandler<TaskEventArgs> PriorityChanged;
        public event EventHandler<TaskEventArgs> CompleteTask;

        public Func<ITaskListStatus> UpdateStatus { private get; set; }
        
        public Image NewTaskImage { set { btnNewTask.Image = value; } }
        public Image EditTaskImage { set { btnEditTask.Image = value; } }
        public Image DeleteTaskImage { set { btnDeleteTask.Image = value; } }

        public TaskListControl()
        {
            InitializeComponent();

            taskProgressBar.Maximum = 100;
            btnNewTask.Enabled = false;
            btnEditTask.Enabled = false;
            btnDeleteTask.Enabled = false;

            listView.MouseClick += listView_MouseClick;
            listView.SelectedIndexChanged += ListView_SelectedIndexChanged;
            mnuPriority.DropDownOpening += mnuPriority_DropDownOpening;

            btnNewTask.Click += (s, e) => NewTask?.Invoke(this, new EventArgs());
            btnEditTask.Click += EditTask_Click;
            btnDeleteTask.Click += DeleteTask_Click;

            mnuDeleteTask.Click += DeleteTask_Click;
            mnuEditTask.Click += EditTask_Click;
            mnuNewTask.Click += (s, e) => NewTask?.Invoke(this, new EventArgs());

            mnuPriorityHigh.Click += (s, e) => ChangePriority(TaskPriority.High);
            mnuPriorityMedium.Click += (s, e) => ChangePriority(TaskPriority.Medium);
            mnuPriorityLow.Click += (s, e) => ChangePriority(TaskPriority.Low);
        }

        private void ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshInputStatus(Gui.GroupedListView.SingleItemSelected<ITask>(listView));
        }

        private void DeleteTask_Click(object sender, EventArgs e)
        {
            var tasks = SelectedTasks;
            var items = listView.SelectedItems;
            var args = new TaskListEventArgs(tasks);

            DeletingTasks?.Invoke(this, args);

            if (!args.Cancel)
            {
                foreach (var item in items)
                    listView.Items.Remove((ListViewItem)item);

                RefreshInputStatus(Gui.GroupedListView.SingleItemSelected<ITask>(listView));
                UpdateStatusBar(UpdateStatus());
            }
        }

        private void EditTask_Click(object sender, EventArgs e)
        {
            ITask task = ActivatedTask();

            if (task != null)
            {
                TaskEventArgs args = new TaskEventArgs(task);
                EditingTask?.Invoke(this, args);

                if (!args.Cancel)
                {
                    listView.SelectedItems[0].Text = task.Title;
                    listView.SelectedItems[0].Group = listView.Groups[task.Category];
                    UpdateStatusBar(UpdateStatus());
                }
            }
        }

        public ITask SelectedTask => ActivatedTask();
        public ITask[] SelectedTasks => Gui.GroupedListView.SelectedItems<ITask>(listView).ToArray();


        public void AddTask(ITask task)
        {
            CreateListviewItem(listView, task, true);
            UpdateStatusBar(UpdateStatus());
            RefreshInputStatus(Gui.GroupedListView.SingleItemSelected<ITask>(listView));
        }

        private void UpdateStatusBar(ITaskListStatus status)
        {
            taskProgressBar.Value = (int)Math.Round(status.PercentageOfTasksCompleted);
            lblTaskInfo.Text = $"{status.NoOfCompletedTasks}/{status.NoOfTasks} ({Math.Round(status.PercentageOfTasksCompleted, 1)}%) Tasks Completed";  
        }

        private void ChangePriority(TaskPriority priority)
        {
            ITask task = Gui.GroupedListView.SelectedItem<ITask>(listView);
            if (task != null)
            {
                task.Priority = priority;
                listView.SelectedItems[0].Text = task.Title;
                listView.SelectedItems[0].Group = listView.Groups[task.Category];

                PriorityChanged?.Invoke(this, new TaskEventArgs(task));
            }
        }

        private ITask ActivatedTask()
        {
            return Gui.GroupedListView.SelectedItem<ITask>(listView);
        }

        private void mnuPriority_DropDownOpening(object sender, EventArgs e)
        {
            ITask task = Gui.GroupedListView.SelectedItem<ITask>(listView);
            if (task != null)
                CheckPriorityMenuItem(task.Priority);
        }

        private void CheckPriorityMenuItem(TaskPriority priority)
        {
            mnuPriorityHigh.Checked = priority == TaskPriority.High;
            mnuPriorityMedium.Checked = priority == TaskPriority.Medium;
            mnuPriorityLow.Checked = priority == TaskPriority.Low;
        }

        private void listView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenu.Show(Cursor.Position);
        }

        private ListViewItem CreateListviewItem(ListView listView, ITask item, bool select = false)
        {
            ListViewItem listItem = new ListViewItem();

            listItem.Checked = item.Completed ? true : false;
            FormatTaskItem(listItem);
            listItem.Tag = item;
            listItem.ToolTipText = item.Title;
            listItem.Text = item.Title;
            listItem.Group = listView.Groups[item.Category];
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, item.DateCreated.ToLongDateString() + ", " + 
                item.DateCreated.ToLongTimeString()));
            listView.Items.Add(listItem);

            return listItem;
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

        public void LoadTaskList(ITask[] tasks, string[] categories)
        {
            listView.ItemCheck -= ListView_ItemCheck;

            Gui.GroupedListView.LoadAllItems(listView, tasks, categories, CreateListviewItem, true);

            if (listView.Items.Count > 0)
            {
                listView.Items[0].Selected = true;
                listView.Items[0].EnsureVisible();
            }

            RefreshInputStatus(Gui.GroupedListView.SingleItemSelected<ITask>(listView));
            UpdateStatusBar(UpdateStatus());

            listView.ItemCheck += ListView_ItemCheck;
        }

        private void RefreshInputStatus(bool singleItemSelected)
        {
            btnNewTask.Enabled = true;
            btnEditTask.Enabled = singleItemSelected;
            btnDeleteTask.Enabled = singleItemSelected;
            mnuPriority.Enabled = singleItemSelected;
            mnuDeleteTask.Enabled = singleItemSelected;
            mnuEditTask.Enabled = singleItemSelected;
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
                    if (checkedTask.Completed != itemChecked)
                    {
                        checkedTask.Completed = itemChecked;
                        FormatTaskItem(item, itemChecked);
                        UpdateStatusBar(UpdateStatus());
                        CompleteTask?.Invoke(this, new TaskEventArgs(checkedTask));
                    }
                }
            }
        }
    }

    public class CancelableEventArgs : EventArgs
    {
        public bool Cancel { get; set; }
    }

    public class TaskListEventArgs : CancelableEventArgs
    {
        private readonly ITask[] taskList;
        public ITask[] TaskList { get { return taskList; } }

        public TaskListEventArgs(ITask[] taskList)
        {
            this.taskList = taskList;
        }
    }

    public class TaskEventArgs : CancelableEventArgs
    {
        private readonly ITask task;
        public ITask Task { get { return task; } }

        public TaskEventArgs(ITask task)
        {
            this.task = task;
        }
    }
}
