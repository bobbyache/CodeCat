namespace CygSoft.CodeCat.Infrastructure.TopicSections
{
    public interface ITaskListStatus
    {
        int NoOfCompletedTasks { get; }
        int NoOfTasks { get; }
        double PercentageOfTasksCompleted { get; }
    }
}