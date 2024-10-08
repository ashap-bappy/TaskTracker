﻿using TaskTracker.Constants;
using TaskTracker.Interfaces;

namespace TaskTracker.Commands
{
    public class AddCommand : ITaskCommand
    {
        private readonly ITaskService _taskService;
        private readonly string _fileFullPath;

        public AddCommand(IConfigurationService config, ITaskService taskService)
        {
            _fileFullPath = config.GetConfigValue<string>(TaskData.FileFullPath);
            _taskService = taskService;
        }

        public void Execute(string[] args)
        {
            if (!IsTaskNameArgumentMissing(args))
            {
                try
                {
                    var task = _taskService.CreateTask(args, _fileFullPath);
                    _taskService.AddTask(task, _fileFullPath);
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
