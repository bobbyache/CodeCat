using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain;
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
        private TaskList taskList = new TaskList();
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
            taskList.Load();
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
                    Task checkedTask = item.Tag as Task;
                    checkedTask.Completed = itemChecked;
                    FormatTaskItem(item, itemChecked);
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
            Gui.GroupedListView.LoadAllItems(listView, taskList.Tasks, TaskList.Categories, CreateListviewItem, true);
            listView.ItemCheck += ListView_ItemCheck;
        }

        private ListViewItem CreateListviewItem(ListView listView, Task item, bool select = false)
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

        public enum TaskPriority
        {
            High,
            Medium,
            Low
        }

        public class TaskList
        {
            public static string[] Categories { get { return new string[] { "High", "Medium", "Low" }; } }

            public static TaskPriority PriorityFromText(string text)
            {
                switch (text)
                {
                    case "High":
                        return TaskPriority.High;
                    case "Medium":
                        return TaskPriority.Medium;
                    case "Low":
                        return TaskPriority.Low;
                    default:
                        return TaskPriority.Medium;
                }
            }

            public static string PriorityText(TaskPriority priority)
            {
                if (priority == TaskPriority.High)
                    return "High";

                if (priority == TaskPriority.Medium)
                    return "Medium";

                if (priority == TaskPriority.Low)
                    return "Low";

                return null;
            }

            private List<Task> taskList = new List<Task>();
            public Task[] Tasks {  get { return taskList.ToArray(); } }

            public Task AddTask(string title, TaskPriority priority)
            {
                Task task = new Task() { Title = title, Priority = priority, DateCreated = DateTime.Now, Completed = false };
                taskList.Add(task);
                return task;
            }

            public void DeleteTasks(Task[] tasks)
            {
                foreach (Task task in  tasks)
                {
                    taskList.Remove(task);
                }
            }

            public void Load()
            {
                taskList = new List<Task>()
                {
                    new Task() { Title = "Check with Dylan about the timeout query and get the details.", DateCreated = DateTime.Now, Completed = false, Priority = TaskPriority.Medium },
                    new Task() { Title = "The view does not appear to exist in P24Master. It only exists in P24MasterTrunk.", DateCreated = DateTime.Now, Completed = false, Priority = TaskPriority.Medium },
                    new Task() { Title = "This is the task of the Domain layer.", DateCreated = DateTime.Now, Completed = false, Priority = TaskPriority.High },
                    new Task() { Title = "The mapping is made explicit in a configuration file that is read by the Infrastructure component", DateCreated = DateTime.Now, Completed = true, Priority = TaskPriority.High },
                    new Task() { Title = "The Separated Interface pattern addresses the problem.", DateCreated = DateTime.Now, Completed = false, Priority = TaskPriority.Low },
                    new Task() { Title = "Base Repository Framework (Infrastructure)", DateCreated = DateTime.Now, Completed = false, Priority = TaskPriority.High },
                    new Task() { Title = "Domain Aggregate-specific Repository Interfaces (Domain)", DateCreated = DateTime.Now, Completed = true, Priority = TaskPriority.Low },
                    new Task() { Title = "You need to spin off the Portal.Tools.InstallImageServerDependencies project executable from your solution so that it will fix this problem:", DateCreated = DateTime.Now, Completed = false, Priority = TaskPriority.High },
                    new Task() { Title = "Just delete everything under packages/Property24.Sysmon.Telemetry.2.0.82.0", DateCreated = DateTime.Now, Completed = false, Priority = TaskPriority.Medium },
                    new Task() { Title = "Can you please communicate to your teams the new location if they require it.", DateCreated = DateTime.Now, Completed = false, Priority = TaskPriority.High },
                    new Task() { Title = "We’ve changed the ARR log location to P24_IISLogs so we can keep 6 months logs- please note there will be no ", DateCreated = DateTime.Now, Completed = false, Priority = TaskPriority.High },
                    new Task() { Title = "In Domain Driven Design this can be used for the interaction", DateCreated = DateTime.Now, Completed = false, Priority = TaskPriority.Medium },
                    new Task() { Title = "The mapping is made explicit in a configuration file that is read by the Infrastructure component.", DateCreated = DateTime.Now, Completed = false, Priority = TaskPriority.High },
                    new Task() { Title = "Responds to user commands. Sometimes instead of being a human the user can be another system.", DateCreated = DateTime.Now, Completed = false, Priority = TaskPriority.Medium },
                    new Task() { Title = "General plumbing + technical. Persistence of objects to database, sending messages.", DateCreated = DateTime.Now, Completed = false, Priority = TaskPriority.High },
                    new Task() { Title = "Each aggregate should have its own repository.", DateCreated = DateTime.Now, Completed = true, Priority = TaskPriority.High },
                    new Task() { Title = "Follow the idea that each aggregate root should have its own repository!", DateCreated = DateTime.Now, Completed = false, Priority = TaskPriority.High },
                    new Task() { Title = "Creating new entity objects using the layered supertype.", DateCreated = DateTime.Now, Completed = false, Priority = TaskPriority.High }
                };
            }
         }

        public class Task : ICategorizedItem
        {
            public string Title { get; set; }
            public DateTime DateCreated { get; set; }
            public bool Completed { get; set; }
            public TaskPriority Priority { get; set; }

            public string Category
            {
                get { return TaskList.PriorityText(this.Priority); }
                set { throw new InvalidOperationException("Category cannot be set for a Task. It is inferred by the current state."); }
            }
        }

        private void btnNewTask_Click(object sender, EventArgs e)
        {
            Task task = new Task() { DateCreated = DateTime.Now, Priority = TaskPriority.Medium, Completed = false, Title = "New Task" };
            TaskEditDialog dialog = new TaskEditDialog(task, TaskList.Categories);
            dialog.ShowDialog(this);

            //Task task = taskList.AddTask("Test Task", TaskPriority.High);
            //ListViewItem item = CreateListviewItem(listView, task, true);
        }

        private void btnDeleteTask_Click(object sender, EventArgs e)
        {
            IEnumerable<Task> tasks = Gui.GroupedListView.SelectedItems<Task>(listView);
            taskList.DeleteTasks(tasks.ToArray());
            LoadTaskList();
        }
    }
}
