
using System.Text.Json;
using TaskTracker.Enums;
using TaskTracker.Models;

namespace TaskTracker
{
    public class Program
    {
        static string directoryPath = @"D:\Educational\Code\Projects\TaskTracker\Data\";
        static string fileName = "tasks.json";
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Invalid command!!!");
                return;
            }

            var command = args[0];

            switch(command)
            {
                case "add":
                    AddTask(args);
                    break;
            }
        }

        private static void AddTask(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Invalid command!!! Task name is missing.");
                return;
            }
            var task = new TaskModel
            {
                Id = Guid.NewGuid().ToString(),
                Description = args[1],
                TaskStatus = Status.Todo,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            var path = Path.Combine(directoryPath, fileName);
            string jsonString = JsonSerializer.Serialize(task, new JsonSerializerOptions { WriteIndented = true});
            File.WriteAllText(path, jsonString);
        }
    }
}
