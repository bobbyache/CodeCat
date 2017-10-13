using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.TaskListing.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.TaskListing
{
    public class Task : ICategorizedListItem, INotifyPropertyChanged, ITask
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string filter;
        public string Filter
        {
            get { return filter; }
            set
            {
                filter = value ?? string.Empty;
                Notify();
            }
        }


        private string title;
        public string Title
        {
            get { return this.title; }
            set
            {
                title = value;
                Notify();
            }
        }

        private bool completed;

        public bool Completed
        {
            get { return completed; }
            set
            {
                completed = value;
                Notify();
            }
        }

        private TaskPriority priority;

        public TaskPriority Priority
        {
            get { return priority; }
            set
            {
                priority = value;
                Notify();
            }
        }

        public DateTime DateCreated { get; private set; }


        public Task(string title, TaskPriority priority) : this(title, null, priority, false, DateTime.Now)
        {
        }

        public Task(string title, string filter, TaskPriority priority, bool completed, DateTime dateCreated)
        {
            this.title = title;
            this.Filter = filter ?? string.Empty;
            this.Completed = completed;
            this.Priority = priority;
            this.DateCreated = dateCreated;
        }

        public string Category
        {
            get { return TaskList.PriorityText(this.Priority); }
            set { throw new InvalidOperationException("Category cannot be set for a Task. It is inferred by the current state."); }
        }

        private void Notify([CallerMemberName]string caller = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
    }
}
