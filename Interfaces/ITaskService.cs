using TaskTracker.Models;

namespace TaskTracker.Interfaces
{
    public interface ITaskService
    {
        TaskModel CreateTask(string[] args, string fileFullPath);
        void AddTask(TaskModel task, string fileFullPath);
        void UpdateTask(string[] args, string fileFullPath);
        void DeleteTask(string[] args, string fileFullPath);
    }
}
