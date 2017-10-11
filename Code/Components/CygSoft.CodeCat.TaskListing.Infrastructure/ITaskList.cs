using CygSoft.CodeCat.TaskListing.Infrastructure;

namespace CygSoft.CodeCat.TaskListing.Infrastructure
{
    public interface ITaskList
    {
        int NoOfCompletedTasks { get; }
        int NoOfTasks { get; }
        double PercentageOfTasksCompleted { get; }
        ITask[] Tasks { get; }

        void AddTask(ITask task);
        void DeleteTasks(ITask[] tasks);
        void Load();
        void Save();
    }
}