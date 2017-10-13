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
        private ITaskListRepository repository;
        public event EventHandler Modified;

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
        //public static string[] Categories { get { return new string[] { "Today", "High", "Medium", "Low" }; } }

        public string[] Filters { get { return tasks.Select(t => t.Filter).Distinct().OrderBy(t => t).ToArray(); } }

        public static TaskPriority PriorityFromText(string text)
        {
            return (TaskPriority)Enum.Parse(typeof(TaskPriority), text);
            //switch (text)
            //{
            //    case "Today":
            //        return TaskPriority.Today;
            //    case "High":
            //        return TaskPriority.High;
            //    case "Medium":
            //        return TaskPriority.Medium;
            //    case "Low":
            //        return TaskPriority.Low;
            //    default:
            //        return TaskPriority.Medium;
            //}
        }

        public static string PriorityText(TaskPriority priority)
        {
            return priority.ToString();

            //if (priority == TaskPriority.Today)
            //    return "Today";

            //if (priority == TaskPriority.High)
            //    return "High";

            //if (priority == TaskPriority.Medium)
            //    return "Medium";

            //if (priority == TaskPriority.Low)
            //    return "Low";

            //return null;
        }

        private TrulyObservableCollection<ITask> tasks = new TrulyObservableCollection<ITask>();

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
            tasks = new TrulyObservableCollection<ITask>(repository.GetTasks());
            tasks.CollectionChanged += (s, e) => Modified?.Invoke(this, new EventArgs());
        }

        public void Save()
        {
            repository.SaveTasks(tasks.ToList());
        }
    }
}
