using TaskTracker.Constants;
using TaskTracker.Helpers;
using TaskTracker.Interfaces;

namespace TaskTracker.Commands
{
    public abstract class ATaskCommand : ITaskCommand
    {
        private readonly string _fileName;

        protected ATaskCommand(IConfigurationService config)
        {
            _fileName = config.GetConfigValue<string>(TaskData.FileName);
        }

        public void Execute(string[] args)
        {
            ExecuteCommand(args);
            MoveDataFile();
        }

        protected abstract void ExecuteCommand(string[] args);

        #region Private
        private void MoveDataFile()
        {
            TaskHelper.MoveDataFileToSolutionDirectory(_fileName);
        } 
        #endregion
    }
}
