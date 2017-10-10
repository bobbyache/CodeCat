using System.Collections.Generic;
using CygSoft.CodeCat.TaskListing.Infrastructure;

namespace CygSoft.CodeCat.TaskListing.Infrastructure
{
    public interface ITaskListRepository
    {
        string FilePath { get; }

        List<ITask> GetTaskList();
        void SaveTaskList(List<ITask> taskList);
    }
}