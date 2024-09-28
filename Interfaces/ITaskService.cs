using TaskTracker.Models;

namespace TaskTracker.Interfaces
{
    public interface ITaskService
    {
        TaskModel CreateTask(string[] args, string fileName);
        void AddTask(TaskModel task, string fileName);
        void UpdateTask(string[] args, string fileName);
    }
}
