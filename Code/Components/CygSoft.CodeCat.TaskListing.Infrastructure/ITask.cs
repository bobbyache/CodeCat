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

    public interface ITask : ICategorizedItem
    {
        bool Completed { get; set; }
        DateTime DateCreated { get; set; }
        TaskPriority Priority { get; set; }
        string Title { get; set; }
    }
}