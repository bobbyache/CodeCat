//using CygSoft.CodeCat.TaskListing.Infrastructure;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Xml.Linq;

//namespace CygSoft.CodeCat.TaskListing
//{
//    public class TaskListManagerRepository
//    {
//        private readonly string filePath;
//        public string FilePath { get { return this.filePath; } }

//        public TaskListManagerRepository(string filePath)
//        {
//            this.filePath = filePath;
//        }

//        public void SaveTaskLists(List<ITaskList> taskLists)
//        {
//            if (!File.Exists(filePath))
//                CreateFile();
//            WriteFile(taskLists);
//        }

//        public List<ITaskList> GetTaskLists()
//        {
//            if (!File.Exists(filePath))
//                CreateFile();

//            XDocument xDocument = XDocument.Load(filePath);
//            IEnumerable<XElement> elements = xDocument.Element("TaskManagement").Elements("TaskLists").Elements();

//            List<ITaskList> taskLists = ExtractFromXml(elements).OfType<ITaskList>().ToList();
//            return taskLists;
//        }

//        private List<ITaskList> ExtractFromXml(IEnumerable<XElement> elements)
//        {
//            List<ITaskList> taskLists = new List<ITaskList>();

//            foreach (XElement element in elements)
//            {
//                ITaskList item = new TaskList(
//                        (string)element.Attribute("Title"),
//                        DateTime.Parse((string)element.Attribute("DateCreated"))
//                    );

//                taskLists.Add(item);
//            }
//            return taskLists.ToList();
//        }

//        private void CreateFile()
//        {
//            XElement rootElement = new XElement("TaskManagement", new XElement("TaskLists"));
//            XDocument xDocument = new XDocument(rootElement);
//            xDocument.Save(filePath);
//        }

//        private void WriteFile(List<ITaskList> taskLists)
//        {
//            XDocument indexDocument = XDocument.Load(filePath);
//            XElement element = indexDocument.Element("TaskManagement").Element("TaskLists");
//            element.RemoveNodes();

//            AppendToContainerElement(element, taskLists);
//            indexDocument.Save(filePath);
//        }

//        private void AppendToContainerElement(XElement containerElement, List<ITaskList> taskLists)
//        {
//            foreach (Task taskList in taskLists)
//            {

//                containerElement.Add(new XElement("TaskList",
//                    new XAttribute("Title", taskList.Title),
//                    new XAttribute("DateCreated", taskList.DateCreated.ToString())
//                ));
//            }
//        }
//    }
//}
