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
            IConfigurationService config = new ConfigurationService();
            config.SetConfigValue(TaskData.FileName, fileName);

            ITaskService taskService = new TaskService();

            if (args.Length == 0)
            {
                Console.WriteLine("Invalid command! Please try again.");
                return;
            }

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
    }
}
