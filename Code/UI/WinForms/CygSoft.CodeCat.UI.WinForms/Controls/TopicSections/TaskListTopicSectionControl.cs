using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Infrastructure.Graphics;
using CygSoft.CodeCat.Infrastructure.TopicSections;
using CygSoft.CodeCat.TaskListing.Infrastructure;
using CygSoft.CodeCat.UI.WinForms.Dialogs;
using CygSoft.CodeCat.UI.WinForms.TopicSectionBase;
using System;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms.Controls.TopicSections
{
    public partial class TaskListTopicSectionControl : BaseTopicSectionControl
    {
        public TaskListTopicSectionControl()
        {
            InitializeComponent();
        }

        public TaskListTopicSectionControl(IAppFacade application, IImageResources imageResources, ITopicDocument topicDocument, ITasksTopicSection topicSection) 
            : base(application, imageResources, topicDocument, topicSection)
        {
            InitializeComponent();

            taskListControl1.NewTaskImage = imageResources.GetImage(ImageKeys.AddSnippet);
            taskListControl1.EditTaskImage = imageResources.GetImage(ImageKeys.EditSnippet);
            taskListControl1.DeleteTaskImage = imageResources.GetImage(ImageKeys.DeleteSnippet);

            taskListControl1.UpdateStatus = () => TaskTopicSection.TaskListStatus;
            taskListControl1.LoadTaskList(topicSection.Tasks, topicSection.Categories);

            taskListControl1.CompleteTask += TaskListControl1_CompleteTask;
            taskListControl1.DeletingTasks += TaskListControl1_DeleteTasks;
            taskListControl1.EditingTask += TaskListControl1_EditTask;
            taskListControl1.NewTask += TaskListControl1_NewTask;
            taskListControl1.PriorityChanged += TaskListControl1_PriorityChanged;
        }

        private void TaskListControl1_PriorityChanged(object sender, TaskEventArgs e)
        {
            Modify();
        }

        private ITasksTopicSection TaskTopicSection => (ITasksTopicSection)topicSection;

        private void TaskListControl1_NewTask(object sender, EventArgs e)
        {
            ITask task = TaskTopicSection.CreateTask();
            TaskEditDialog dialog = new TaskEditDialog(application, task, TaskTopicSection.Categories);
            DialogResult result = dialog.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                TaskTopicSection.AddTask(task);
                taskListControl1.AddTask(task);
                Modify();
            }
        }

        private void TaskListControl1_EditTask(object sender, TaskEventArgs e)
        {
            ITask task = taskListControl1.SelectedTask;

            if (task != null)
            {
                TaskEditDialog dialog = new TaskEditDialog(application, task, application.TaskPriorities);
                DialogResult result = dialog.ShowDialog(this);

                if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else
                    Modify();

            }
        }

        private void TaskListControl1_DeleteTasks(object sender, TaskListEventArgs e)
        {
            ITask[] tasks = e.TaskList;
            TaskTopicSection.DeleteTasks(tasks);
            Modify();
        }

        private void TaskListControl1_CompleteTask(object sender, TaskEventArgs e)
        {
            Modify();
        }
    }
}
