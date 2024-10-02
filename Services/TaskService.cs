using System.Text.Json;
using System.Text.Json.Serialization;
using TaskTracker.Enums;
using TaskTracker.Helpers;
using TaskTracker.Interfaces;
using TaskTracker.Models;

namespace TaskTracker.Services
{
    public class TaskService : ITaskService
    {
        public TaskModel CreateTask(string[] args, string fileFullPath)
        {
            var tasks = GetAllTasks(fileFullPath);
            int taskId = TaskHelper.GenerateTaskId(tasks);
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
        public void AddTask(TaskModel task, string fileFullPath)
        {
            try
            {
                var tasks = GetAllTasks(fileFullPath);
                tasks.Add(task);
                SaveTasksToFile(tasks, fileFullPath);
                Console.WriteLine($"Task added successfully with ID: {task.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding task: {ex.Message}");
            }
        }

        public void UpdateTask(string[] args, string fileFullPath)
        {
            try
            {
                int.TryParse(args[1], out int taskId);
                var tasks = GetAllTasks(fileFullPath);
                var task = GetTaskById(taskId, tasks);

                if (task == null)
                {
                    Console.WriteLine($"No task found with ID: {taskId}");
                    return;
                }

                UpdateTaskProperties(args, task);
                SaveTasksToFile(tasks, fileFullPath);
                Console.WriteLine($"Task updated successfully with ID: {taskId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating task: {ex.Message}");
            }
        }

        public void DeleteTask(string[] args, string fileFullPath)
        {
            try
            {
                int.TryParse(args[1], out int taskId);
                var tasks = GetAllTasks(fileFullPath);

                if (!tasks.Exists(t => t.Id == taskId))
                {
                    Console.WriteLine($"No task found with ID: {taskId}");
                    return;
                }
                else
                {
                    tasks.RemoveAll(t => t.Id == taskId);
                }

                SaveTasksToFile(tasks, fileFullPath);
                Console.WriteLine($"Task deleted successfully with ID: {taskId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting task: {ex.Message}");
            }
        }

        #region Private Methods
        private static void UpdateTaskProperties(string[] args, TaskModel task)
        {
            var taskName = args[2];
            task.Description = taskName;
            task.UpdatedAt = DateTime.Now;
        }

        private TaskModel GetTaskById(int taskId, List<TaskModel> tasks)
        {
            var task = tasks.FirstOrDefault(t => t.Id == taskId);
            return task;
        }

        private static List<TaskModel> GetAllTasks(string fileFullPath)
        {
            List<TaskModel> tasks = new List<TaskModel>();

            try
            {
                if (!File.Exists(fileFullPath))
                {
                    return tasks;
                }

                string jsonString = File.ReadAllText(fileFullPath);
                tasks = JsonSerializer.Deserialize<List<TaskModel>>(jsonString, new JsonSerializerOptions
                {
                    Converters = { new JsonStringEnumConverter() }
                }) ?? new List<TaskModel>();
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

        private static void SaveTasksToFile(List<TaskModel> tasks, string fileFullPath)
        {
            try
            {
                var jsonString = JsonSerializer.Serialize(tasks, new JsonSerializerOptions 
                {
                    WriteIndented = true,
                    Converters = { new JsonStringEnumConverter() }
                });
                //Console.WriteLine($"BaseDirectory: {AppContext.BaseDirectory}");
                //Console.WriteLine($"Saving file path: {fileFullPath}");
                File.WriteAllText(fileFullPath, jsonString);
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

        #endregion
    }
}
