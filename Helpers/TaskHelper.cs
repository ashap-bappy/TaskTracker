using TaskTracker.Models;

namespace TaskTracker.Helpers
{
    public class TaskHelper
    {
        public static int GenerateTaskId(List<TaskModel> tasks)
        {
            var taskId = tasks.Any() ? tasks.Max(task => task.Id) : 0;
            return taskId;
        }
    }
}
