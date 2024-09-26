using TaskTracker.Constants;
using TaskTracker.Services;

namespace TaskTracker
{
    public class Program
    {
        static string directoryPath = @"D:\Educational\Code\Projects\TaskTracker\Data\";
        static string fileName = "tasks.json";

        static void Main(string[] args)
        {
            CheckCommandValidity(args);

            var command = args[0];

            switch (command)
            {
                case "add":
                    var task = TaskService.CreateTask(args, directoryPath, fileName);
                    TaskService.AddTask(task, directoryPath, fileName, TaskMessage.Added);
                    break;
                case "update":
                    TaskService.UpdateTask(args, directoryPath, fileName);
                    break;
            }
        }

        private static void CheckCommandValidity(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Invalid command!!!");
                return;
            }

            var command = args[0];

            if (command == "add" && args.Length < 2)
            {
                Console.WriteLine("Invalid command! Task name is missing.");
                return;
            }

            if (command == "update")
            {
                if (args.Length < 2)
                {
                    Console.WriteLine("Invalid command!!! Task ID is missing.");
                    return;
                }
                if (args.Length < 3)
                {
                    Console.WriteLine("Invalid command!!! Updated task name is missing.");
                    return;
                }
            }
        }
    }
}
