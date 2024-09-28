using TaskTracker.Models;

namespace TaskTracker.Helpers
{
    public class TaskHelper
    {
        public static void MoveDataFileToSolutionDirectory(string fileName)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string sourceDataFilePath = Path.Combine(currentDirectory, fileName);
            string destinationDataFilePath = Path.Combine(GetSolutionDirectory(), "Data", fileName);

            try
            {
                // Move the file to the solution directory
                if (!File.Exists(sourceDataFilePath))
                {
                    throw new Exception($"File doesn't exist in the specified {sourceDataFilePath}");
                }
                File.Copy(sourceDataFilePath, destinationDataFilePath, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error moving file: {ex.Message}");
            }
        }
        public static int GenerateTaskId(List<TaskModel> tasks)
        {
            var taskId = tasks.Any() ? tasks.Max(task => task.Id) : 0;
            return taskId;
        }

        #region Private
        private static string GetSolutionDirectory()
        {
            // Assuming the executable is running in the bin folder
            var directoryInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            while (directoryInfo.Parent != null && !directoryInfo.GetFiles("*.sln").Any())
            {
                directoryInfo = directoryInfo.Parent;
            }
            return directoryInfo.FullName;
        } 
        #endregion
    }
}
