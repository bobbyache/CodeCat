using CygSoft.CodeCat.Infrastructure;
using System;
using System.ComponentModel;

namespace CygSoft.CodeCat.TaskListing.Infrastructure
{
    public enum TaskPriority
    {
        Today,
        High,
        Medium,
        Low
    }

    public interface ITask : ICategorizedListItem, INotifyPropertyChanged
    {
        bool Completed { get; set; }
        DateTime DateCreated { get; }
        TaskPriority Priority { get; set; }
        string Title { get; set; }
        string Filter { get; set; }
    }
}