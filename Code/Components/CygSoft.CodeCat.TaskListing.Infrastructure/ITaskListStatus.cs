namespace CygSoft.CodeCat.TaskListing.Infrastructure
{
    public interface ITaskListStatus
    {
        int NoOfCompletedTasks { get; }
        int NoOfTasks { get; }
        double PercentageOfTasksCompleted { get; }
    }
}