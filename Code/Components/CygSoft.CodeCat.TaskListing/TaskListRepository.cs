﻿using CygSoft.CodeCat.TaskListing.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CygSoft.CodeCat.TaskListing
{
    public class TaskListRepository : ITaskListRepository
    {
        private readonly string filePath;
        public string FilePath { get { return this.filePath; } }

        public TaskListRepository(string filePath)
        {
            this.filePath = filePath;
        }

        public void SaveTasks(List<ITask> taskList)
        {
            if (!File.Exists(filePath))
                CreateFile();
            WriteFile(taskList);
        }

        public List<ITask> GetTasks()
        {
            if (!File.Exists(filePath))
                CreateFile();

            XDocument xDocument = XDocument.Load(filePath);
            IEnumerable<XElement> elements = xDocument.Element("TaskList").Elements("Tasks").Elements();

            List<ITask> tasks = ExtractFromXml(elements).OfType<ITask>().ToList();
            return tasks;
        }

        private List<Task> ExtractFromXml(IEnumerable<XElement> elements)
        {
            List<Task> tasks = new List<Task>();

            foreach (XElement element in elements)
            {
                Task item = new Task(
                        (string)element.Attribute("Title"),
                        (string)element?.Attribute("Filter"),
                        TaskList.PriorityFromText((string)element.Attribute("Priority")),
                        bool.Parse((string)element.Attribute("Completed")),
                        DateTime.Parse((string)element.Attribute("DateCreated"))
                    );

                tasks.Add(item);
            }
            return tasks.ToList();
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
                    new XAttribute("Filter", task.Filter ?? ""),
                    new XAttribute("Priority", task.Priority),
                    new XAttribute("Completed", task.Completed.ToString()),
                    new XAttribute("DateCreated", task.DateCreated.ToString())
                ));
            }
        }
    }
}