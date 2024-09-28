using TaskTracker.Constants;
using TaskTracker.Interfaces;

namespace TaskTracker.Commands
{
    public class UpdateCommand : ATaskCommand
    {
        private readonly ITaskService _taskService;
        private readonly string _fileName;

        public UpdateCommand(IConfigurationService config, ITaskService taskService) : base(config)
        {
            _fileName = config.GetConfigValue<string>(TaskData.FileName);
            _taskService = taskService;
        }

        protected override void ExecuteCommand(string[] args)
        {
            if (!IsUpdateArgumentsMissing(args))
            {
                try
                {
                    _taskService.UpdateTask(args, _fileName);
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
            string missingArgument = args.Length == 1 ? "Task ID" : "Updated task name";
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
