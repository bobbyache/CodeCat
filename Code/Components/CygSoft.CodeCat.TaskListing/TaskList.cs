using CygSoft.CodeCat.TaskListing.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace CygSoft.CodeCat.TaskListing
{
    public class TaskList : ITaskListStatus
    {
        private string filePath = null;

        public TaskList(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("Must supply a valid task file path.");
            this.filePath = filePath;
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
            if (!File.Exists(filePath))
                CreateFile();

            XDocument xDocument = XDocument.Load(filePath);
            IEnumerable<XElement> elements = xDocument.Element("TaskList").Elements("Tasks").Elements();

            List<Task> tasks = ExtractFromXml(elements);
            taskList = tasks.OfType<ITask>().ToList();
        }

        public void Save()
        {
            if (!File.Exists(filePath))
                CreateFile();
            WriteFile(taskList);
        }

        private void CreateFile()
        {
            XElement rootElement = new XElement("TaskList", new XElement("Tasks"));
            XDocument xDocument = new XDocument(rootElement);
            xDocument.Save(filePath);
        }

        private void WriteFile(List<ITask> tasks)
        {
            XDocument indexDocument = XDocument.Load(filePath);
            XElement element = indexDocument.Element("TaskList").Element("Tasks");
            element.RemoveNodes();

            AppendToContainerElement(element, tasks);
            indexDocument.Save(filePath);
        }

        private void AppendToContainerElement(XElement containerElement, List<ITask> tasks)
        {
            foreach (ITask task in tasks)
            {

                containerElement.Add(new XElement("Task",
                    new XAttribute("Title", task.Title),
                    new XAttribute("Priority", task.Priority),
                    new XAttribute("Completed", task.Completed.ToString()),
                    new XAttribute("DateCreated", task.DateCreated.ToString())
                ));
            }
        }

        private List<Task> ExtractFromXml(IEnumerable<XElement> elements)
        {
            List<Task> tasks = new List<Task>();

            foreach (XElement element in elements)
            {
                Task item = new Task(
                        (string)element.Attribute("Title"),
                        TaskList.PriorityFromText((string)element.Attribute("Priority")),
                        bool.Parse((string)element.Attribute("Completed")),
                        DateTime.Parse((string)element.Attribute("DateCreated"))
                    );

                tasks.Add(item);
            }
            return tasks.ToList();
        }
    }
}
