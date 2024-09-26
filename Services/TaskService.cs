using System.Text.Json;
using TaskTracker.Constants;
using TaskTracker.Enums;
using TaskTracker.Models;

namespace TaskTracker.Services
{
    public class TaskService
    {
        public static void AddTask(TaskModel task, string directoryPath, string fileName, string message)
        {
            try
            {
                List<TaskModel> tasks = GetAllTasks(directoryPath, fileName);
                tasks.Add(task);
                var path = Path.Combine(directoryPath, fileName);
                var jsonString = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(path, jsonString);

                Console.WriteLine($"Task {message} successfully.");
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"Error writing to the file: {ioEx.Message}");
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"Error serializing the tasks: {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public static void UpdateTask(string[] args, string directoryPath, string fileName)
        {
            try
            {
                int.TryParse(args[1], out int taskId);
                var taskName = args[2];
                List<TaskModel> tasks = GetAllTasks(directoryPath, fileName);
                var task = tasks.FirstOrDefault(task => task.Id == taskId);

                if (task == null)
                {
                    throw new Exception($"No task found with Id: {taskId}");
                }

                task.Description = taskName;
                task.UpdatedAt = DateTime.Now;
                AddTask(task, directoryPath, fileName, TaskMessage.Updated);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static TaskModel CreateTask(string[] args, string directoryPath, string fileName)
        {
            int taskId = GenerateTaskId(directoryPath, fileName);
            var task = new TaskModel
            {
                Id = ++taskId,
                Description = args[1],
                TaskStatus = Status.Todo,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            return task;
        }

        #region Private
        private static int GenerateTaskId(string directoryPath, string fileName)
        {
            var tasks = GetAllTasks(directoryPath, fileName);
            var taskId = tasks.Any() ? tasks.Max(task => task.Id) : 0;
            return taskId;
        }
        private static List<TaskModel> GetAllTasks(string directoryPath, string fileName)
        {
            List<TaskModel> tasks = new List<TaskModel>();

            try
            {
                string path = Path.Combine(directoryPath, fileName);

                if (!File.Exists(path))
                {
                    return tasks;
                }

                string jsonString = File.ReadAllText(path);
                tasks = JsonSerializer.Deserialize<List<TaskModel>>(jsonString) ?? new List<TaskModel>();
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"Error reading the file: {ioEx.Message}");
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"Error parsing the JSON: {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return tasks;
        }
        #endregion
    }
}
