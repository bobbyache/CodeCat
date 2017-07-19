using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.TaskListing.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.TaskListing
{
    public class Task : ICategorizedItem, ITask
    {
        public string Title { get; set; }
        public DateTime DateCreated { get; private set; }
        public bool Completed { get; set; }
        public TaskPriority Priority { get; set; }

        public Task(string title, TaskPriority priority)
        {
            this.Title = title;
            this.Completed = false;
            this.Priority = priority;
            this.DateCreated = DateTime.Now;
        }

        public Task(string title, TaskPriority priority, bool completed, DateTime dateCreated)
        {
            this.Title = title;
            this.Completed = completed;
            this.Priority = priority;
            this.DateCreated = dateCreated;
        }

        public string Category
        {
            get { return TaskList.PriorityText(this.Priority); }
            set { throw new InvalidOperationException("Category cannot be set for a Task. It is inferred by the current state."); }
        }
    }
}
