using TaskTracker.Constants;
using TaskTracker.Helpers;
using TaskTracker.Interfaces;
using TaskTracker.Services;

namespace TaskTracker
{
    public class Program
    {
        const string fileName = "tasks.json";

        static void Main(string[] args)
        {
            IConfigurationService config = SetupConfigurationSettings();

            if (args.Length == 0)
            {
                Console.WriteLine("Invalid command! Please try again.");
                return;
            }

            ITaskService taskService = new TaskService();
            var commandResolver = new CommandResolver(config, taskService);
            var command = commandResolver.ResolveCommand(args[0]);

            if (command == null)
            {
                Console.WriteLine("Invalid command! Please try again.");
            }
            else
            {
                command.Execute(args);
            }
        }

        #region Private
        private static IConfigurationService SetupConfigurationSettings()
        {
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TaskTracker");

            // Ensure the directory exists
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            //Console.WriteLine($"Application data directory: {directoryPath}");

            IConfigurationService config = new ConfigurationService();
            config.SetConfigValue(TaskData.DirectoryPath, directoryPath);
            config.SetConfigValue(TaskData.FileName, fileName);
            config.SetConfigValue(TaskData.FileFullPath, Path.Combine(directoryPath, fileName));
            return config;
        } 
        #endregion
    }
}
