using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.TaskListing.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CygSoft.CodeCat.TaskListing
{
    public class TaskList : Infrastructure.ITaskList
    {
        public event EventHandler FiltersChanged;

        private TaskCollection<ITask> tasks = new TaskCollection<ITask>();
        private ITaskListRepository repository;

        public TaskList(ITaskListRepository repository)
        {
            if (repository == null)
                throw new ArgumentNullException("Must supply an implementation for ITaskListRepository.");

            this.repository = repository;
        }

        private void Tasks_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public static Task CreateTask()
        {
            return new Task("New Task", TaskPriority.Medium);
        }

        public static string[] Categories { get { return Enum.GetNames(typeof(TaskPriority)); } }

        public string[] Filters
        {
            get
            {
                List<string> filters = new List<string>();
                filters.Add(string.Empty);
                return filters.Union(tasks.Select(t => t.Filter)).Distinct().OrderBy(t => t).ToArray();
            }
        }

        public static TaskPriority PriorityFromText(string text)
        {
            return (TaskPriority)Enum.Parse(typeof(TaskPriority), text);
        }

        public static string PriorityText(TaskPriority priority)
        {
            return priority.ToString();
        }        

        public ITask[] GetTasks(string filter = null)
        {
            if (string.IsNullOrEmpty(filter))
                return tasks.ToArray();
            else
                return tasks.Where(t => t.Filter == filter).ToArray();
        }

        public int NoOfTasks { get { return (tasks != null ?  tasks.Count : 0); } }
        public int NoOfCompletedTasks { get { return (tasks != null ? tasks.Where(r => r.Completed == true).Count() : 0); } }

        public double PercentageOfTasksCompleted
        {
            get
            {
                if (NoOfTasks <= 0)
                    return 0;
                return ((double)NoOfCompletedTasks / (double)NoOfTasks) * 100f;
            }
        }

        public void AddTask(ITask task)
        {
            tasks.Add(task);
            Tasks_FilterChanged(task, new EventArgs());
        }

        public void DeleteTasks(ITask[] tasks)
        {
            foreach (Task task in tasks)
            {
                this.tasks.Remove(task);
            }
        }

        public void Load()
        {
            if (tasks != null)
                tasks.FilterChanged -= Tasks_FilterChanged;
            tasks = new TaskCollection<ITask>(repository.GetTasks());
            tasks.FilterChanged += Tasks_FilterChanged;
        }

        private void Tasks_FilterChanged(object sender, EventArgs e)
        {
            FiltersChanged?.Invoke(sender, new EventArgs());
        }

        public void Save()
        {
            repository.SaveTasks(tasks.ToList());
        }
    }
}
