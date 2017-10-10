using CygSoft.CodeCat.TaskListing.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CygSoft.CodeCat.TaskListing
{
    public class TaskList
    {
        private ITaskListRepository repository;

        public TaskList(ITaskListRepository repository)
        {
            if (repository == null)
                throw new ArgumentNullException("Must supply an implementation for ITaskListRepository.");

            this.repository = repository;
        }

        public static Task CreateTask()
        {
            return new Task("New Task", TaskPriority.Medium);
        }

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

        private List<ITask> taskList = new List<ITask>();
        public ITask[] Tasks { get { return taskList.ToArray(); } }

        public int NoOfTasks { get { return (taskList != null ?  taskList.Count : 0); } }
        public int NoOfCompletedTasks { get { return (taskList != null ? taskList.Where(r => r.Completed == true).Count() : 0); } }

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
            taskList.Add(task);
        }

        public void DeleteTasks(ITask[] tasks)
        {
            foreach (Task task in tasks)
            {
                taskList.Remove(task);
            }
        }

        public void Load()
        {
            taskList = repository.GetTaskList();
        }

        public void Save()
        {
            repository.SaveTaskList(taskList);
        }
    }
}
