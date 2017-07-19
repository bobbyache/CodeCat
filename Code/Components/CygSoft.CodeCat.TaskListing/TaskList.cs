using CygSoft.CodeCat.TaskListing.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.TaskListing
{
    public class TaskList
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
            return new Task() { Title = "New Task", Priority = TaskPriority.Medium, DateCreated = DateTime.Now, Completed = false };
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
            taskList = new List<ITask>()
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
}
