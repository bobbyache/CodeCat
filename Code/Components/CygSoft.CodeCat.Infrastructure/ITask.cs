using CygSoft.CodeCat.Infrastructure;
using System;

namespace CygSoft.CodeCat.TaskListing.Infrastructure
{
    public enum TaskPriority
    {
        High,
        Medium,
        Low
    }

    public interface ITask : ICategorizedListItem
    {
        bool Completed { get; set; }
        DateTime DateCreated { get; }
        TaskPriority Priority { get; set; }
        string Title { get; set; }
    }
}