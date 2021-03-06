﻿using CygSoft.CodeCat.TaskListing.Infrastructure;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface ITasksTopicSection : ITopicSection
    {
        ITaskListStatus TaskListStatus { get; }
        ITask[] Tasks { get; }
        string[] Categories { get; }

        ITask CreateTask();
        void AddTask(ITask task);
        void DeleteTasks(ITask[] tasks);
    }
}