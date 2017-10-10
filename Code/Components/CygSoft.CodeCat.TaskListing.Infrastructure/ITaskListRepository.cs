using System.Collections.Generic;
using CygSoft.CodeCat.TaskListing.Infrastructure;

namespace CygSoft.CodeCat.TaskListing.Infrastructure
{
    public interface ITaskListRepository
    {
        string FilePath { get; }

        List<ITask> GetTasks();
        void SaveTasks(List<ITask> taskList);
    }
}