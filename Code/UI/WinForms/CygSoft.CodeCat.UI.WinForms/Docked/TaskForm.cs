using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain;
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
            Gui.GroupedListView.LoadAllItems(listView, taskList.Tasks, new string[] { "High", "Medium", "Low", "Complete" }, CreateListviewItem, true);
            listView.ItemCheck += ListView_ItemCheck;
            //listView.ItemChecked += ListView_ItemChecked;
        }

        private void ListView_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.CurrentValue != e.NewValue)
            {
                ListViewItem originalItem = listView.Items[e.Index];

                if (originalItem != null)
                {
                    Task checkedTask = originalItem.Tag as Task;
                    if (checkedTask != null)
                    {
                        checkedTask.Completed = e.NewValue == CheckState.Checked;
                        listView.ItemCheck -= ListView_ItemCheck;
                        Gui.GroupedListView.LoadAllItems(listView, taskList.Tasks, new string[] { "High", "Medium", "Low", "Complete" }, CreateListviewItem, true);
                        listView.ItemCheck += ListView_ItemCheck;
                    }
                }
                //ListViewItem clonedItem = originalItem.Clone() as ListViewItem;

                
                

                //Task checkedTask = originalItem.Tag as Task;
                // checkedItem.Checked;

                //if (clonedItem != null)
                //{
                //    clonedItem.Checked = e.NewValue == CheckState.Checked;
                //    Task checkedTask = clonedItem.Tag as Task;
                //    //checkedTask.Completed = e.NewValue == CheckState.Checked; zz
                //    checkedTask.Completed = clonedItem.Checked;

                //    //checkedTask.Completed = e.NewValue == CheckState.Checked;
                //    listView.Items.Remove(originalItem);
                //    listView.Items.Add(clonedItem);

                //    Gui.GroupedListView.LoadAllItems(listView, GetTasks(), new string[] { "High", "Medium", "Low", "Complete" }, CreateListviewItem, true);

                //    //Gui.GroupedListView.ReloadGroups(listView, new string[] { "High", "Medium", "Low", "Complete" });
                //}
            }


        }

        private void ListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            ////Gui.GroupedListView.LoadAllItems<Task>(listView, GetTasks(), new string[] { "High", "Medium", "Low", "Complete" }, CreateListviewItem, true);
            //ListViewItem checkedItem = e.Item;
            //Task checkedTask = checkedItem.Tag as Task;
            //checkedTask.Completed = checkedItem.Checked;

            //if (checkedTask != null)
            //{
            //    listView.Items.Remove(checkedItem);
            //    listView.Items.Add(CreateListviewItem(listView, checkedTask, true));
            //}
        }

        private ListViewItem CreateListviewItem(ListView listView, Task item, bool select = false)
        {
            ListViewItem listItem = new ListViewItem();

            //listItem.Name = item.Id;
            //listItem.ImageKey = item.FileExtension;
            listItem.Checked = item.Completed ? true : false;
            listItem.ForeColor = item.Completed ? Color.DarkSlateGray : Color.Black;
            listItem.Font = item.Completed ? new Font(listView.Font, FontStyle.Strikeout) : new Font(listView.Font, FontStyle.Regular);
            listItem.Tag = item;
            listItem.ToolTipText = item.Title;
            listItem.Text = item.Title;
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
            private List<Task> taskList = new List<Task>();

            public Task[] Tasks {  get { return taskList.ToArray(); } }

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
                get
                {
                    if (Completed)
                        return "Complete";

                    if (Priority == TaskPriority.High)
                        return "High";

                    if (Priority == TaskPriority.Medium)
                        return "Medium";

                    if (Priority == TaskPriority.Low)
                        return "Low";

                    return null;
                }

                set
                {
                    throw new InvalidOperationException("Category cannot be set for a Task. It is inferred by the current state.");
                }
            }
        }

        //private Task[] GetTasks()
        //{
        //    List<Task> tasks = new List<Task>()
        //    {
        //        new Task() { Title = "Check with Dylan about the timeout query and get the details.", DateCreated = DateTime.Now, Completed = false, Priority = TaskPriority.Medium },
        //        new Task() { Title = "The view does not appear to exist in P24Master. It only exists in P24MasterTrunk.", DateCreated = DateTime.Now, Completed = false, Priority = TaskPriority.Medium },
        //        new Task() { Title = "This is the task of the Domain layer.", DateCreated = DateTime.Now, Completed = false, Priority = TaskPriority.High },
        //        new Task() { Title = "The mapping is made explicit in a configuration file that is read by the Infrastructure component", DateCreated = DateTime.Now, Completed = true, Priority = TaskPriority.High },
        //        new Task() { Title = "The Separated Interface pattern addresses the problem.", DateCreated = DateTime.Now, Completed = false, Priority = TaskPriority.Low },
        //        new Task() { Title = "Base Repository Framework (Infrastructure)", DateCreated = DateTime.Now, Completed = false, Priority = TaskPriority.High },
        //        new Task() { Title = "Domain Aggregate-specific Repository Interfaces (Domain)", DateCreated = DateTime.Now, Completed = true, Priority = TaskPriority.Low },
        //        new Task() { Title = "You need to spin off the Portal.Tools.InstallImageServerDependencies project executable from your solution so that it will fix this problem:", DateCreated = DateTime.Now, Completed = false, Priority = TaskPriority.High },
        //        new Task() { Title = "Just delete everything under packages/Property24.Sysmon.Telemetry.2.0.82.0", DateCreated = DateTime.Now, Completed = false, Priority = TaskPriority.Medium },
        //        new Task() { Title = "Can you please communicate to your teams the new location if they require it.", DateCreated = DateTime.Now, Completed = false, Priority = TaskPriority.High },
        //        new Task() { Title = "We’ve changed the ARR log location to P24_IISLogs so we can keep 6 months logs- please note there will be no ", DateCreated = DateTime.Now, Completed = false, Priority = TaskPriority.High },
        //        new Task() { Title = "In Domain Driven Design this can be used for the interaction", DateCreated = DateTime.Now, Completed = false, Priority = TaskPriority.Medium },
        //        new Task() { Title = "The mapping is made explicit in a configuration file that is read by the Infrastructure component.", DateCreated = DateTime.Now, Completed = false, Priority = TaskPriority.High },
        //        new Task() { Title = "Responds to user commands. Sometimes instead of being a human the user can be another system.", DateCreated = DateTime.Now, Completed = false, Priority = TaskPriority.Medium },
        //        new Task() { Title = "General plumbing + technical. Persistence of objects to database, sending messages.", DateCreated = DateTime.Now, Completed = false, Priority = TaskPriority.High },
        //        new Task() { Title = "Each aggregate should have its own repository.", DateCreated = DateTime.Now, Completed = true, Priority = TaskPriority.High },
        //        new Task() { Title = "Follow the idea that each aggregate root should have its own repository!", DateCreated = DateTime.Now, Completed = false, Priority = TaskPriority.High },
        //        new Task() { Title = "Creating new entity objects using the layered supertype.", DateCreated = DateTime.Now, Completed = false, Priority = TaskPriority.High }
        //    };

        //    return tasks.ToArray();
        //}
    }
}
