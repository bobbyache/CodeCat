using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using CygSoft.CodeCat.DocumentManager.TopicSections;
using CygSoft.CodeCat.Infrastructure.TopicSections;
using CygSoft.CodeCat.TaskListing;
using CygSoft.CodeCat.TaskListing.Infrastructure;

namespace CygSoft.CodeCat.Domain.TopicSections.Tasks
{
    public class TaskListTopicSection : TopicSection, ITasksTopicSection
    {
        private TaskList taskList;

        public TaskListTopicSection(string folder, string title) 
            : base(new DocumentPathGenerator(folder, "tasks"), title, null)
        {
            this.DocumentType = SectionTypes.GetDocumentType(TopicSectionType.TaskList);
            this.taskList = new TaskList(this.FilePath);
        }

        public TaskListTopicSection(string folder, string id, string title, int ordinal) 
            : base(new DocumentPathGenerator(folder, "tasks", id), title, null, ordinal)
        {
            this.DocumentType = SectionTypes.GetDocumentType(TopicSectionType.TaskList);
            this.taskList = new TaskList(this.FilePath);
        }

        protected override void SaveFile()
        {
            base.SaveFile();
            taskList.Save();
        }

        protected override void OnOpen()
        {
            taskList.Load();
        }

        public ITaskListStatus TaskListStatus { get { return this.taskList as ITaskListStatus; } }

        public ITask[] Tasks => taskList.Tasks;

        public string[] Categories => TaskList.Categories;

        public void DeleteTasks(ITask[] tasks)
        {
            taskList.DeleteTasks(tasks);
        }

        public ITask CreateTask()
        {
            return TaskList.CreateTask();
        }

        public void AddTask(ITask task)
        {
            taskList.AddTask(task);
        }
    }
}
