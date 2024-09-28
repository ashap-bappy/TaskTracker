namespace TaskTracker.Interfaces
{
    public interface ITaskCommand
    {
        void Execute(string[] args);
    }
}
