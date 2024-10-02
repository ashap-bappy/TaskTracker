using TaskTracker.Constants;
using TaskTracker.Interfaces;

namespace TaskTracker.Commands
{
    public class DeleteCommand : ITaskCommand
    {
        private readonly ITaskService _taskService;
        private readonly string _fileFullPath;
        public DeleteCommand(IConfigurationService config, ITaskService taskService)
        {
            _taskService = taskService;
            _fileFullPath = config.GetConfigValue<string>(TaskData.FileFullPath);
        }
        public void Execute(string[] args)
        {
            if (!IsTaskIdMissing(args))
            {
                try
                {
                    _taskService.DeleteTask(args, _fileFullPath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting task: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"Invalid command!!! Task ID is missing");
            }
        }

        private bool IsTaskIdMissing(string[] args)
        {
            if (args.Length < 2)
            {
                return true;
            }
            return false;
        }
    }
}
