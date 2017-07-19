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
        public DateTime DateCreated { get; set; }
        public bool Completed { get; set; }
        public TaskPriority Priority { get; set; }

        public string Category
        {
            get { return TaskList.PriorityText(this.Priority); }
            set { throw new InvalidOperationException("Category cannot be set for a Task. It is inferred by the current state."); }
        }
    }
}
