using TaskTracker.Constants;
using TaskTracker.Interfaces;

namespace TaskTracker.Commands
{
    public class UpdateCommand : ITaskCommand
    {
        private readonly ITaskService _taskService;
        private readonly string _fileFullPath;

        public UpdateCommand(IConfigurationService config, ITaskService taskService)
        {
            _fileFullPath = config.GetConfigValue<string>(TaskData.FileFullPath);
            _taskService = taskService;
        }

        public void Execute(string[] args)
        {
            if (!IsUpdateArgumentsMissing(args))
            {
                try
                {
                    _taskService.UpdateTask(args, _fileFullPath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating task: {ex.Message}");
                }
            }
            else
            {
                ShowMissingArgumentsMessage(args);
            }
        }

        #region Private
        private static void ShowMissingArgumentsMessage(string[] args)
        {
            string missingArgument = args.Length == 2 ? "Task ID" : "Updated task name";
            Console.WriteLine($"Invalid command!!! {missingArgument} is missing.");
        }

        private static bool IsUpdateArgumentsMissing(string[] args)
        {
            if (args.Length < 3)
            {
                return true;
            }
            return false;
        } 
        #endregion
    }
}
