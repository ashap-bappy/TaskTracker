using TaskTracker.Constants;
using TaskTracker.Interfaces;

namespace TaskTracker.Commands
{
    public class AddCommand : ATaskCommand
    {
        private readonly ITaskService _taskService;
        private readonly string _fileName;

        public AddCommand(IConfigurationService config, ITaskService taskService) : base(config)
        {
            _fileName = config.GetConfigValue<string>(TaskData.FileName);
            _taskService = taskService;
        }

        protected override void ExecuteCommand(string[] args)
        {
            if (!IsTaskNameArgumentMissing(args))
            {
                try
                {
                    var task = _taskService.CreateTask(args, _fileName);
                    _taskService.AddTask(task, _fileName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error adding task: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"Invalid command!!! Task name is missing.");
            }
        }

        #region Private
        private static bool IsTaskNameArgumentMissing(string[] args)
        {
            if (args.Length < 2)
            {
                return true;
            }
            return false;
        } 
        #endregion
    }
}
