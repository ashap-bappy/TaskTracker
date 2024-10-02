using TaskTracker.Commands;
using TaskTracker.Interfaces;

namespace TaskTracker.Helpers
{
    public class CommandResolver
    {
        private readonly Dictionary<string, ITaskCommand> _command;

        public CommandResolver(IConfigurationService config, ITaskService taskService)
        {
            _command = new Dictionary<string, ITaskCommand>
            {
                {"add", new AddCommand(config, taskService) },
                {"update", new UpdateCommand(config, taskService) },
                {"delete", new DeleteCommand(config, taskService) }
            };
        }

        public ITaskCommand ResolveCommand(string commandKey) 
        {
            if (_command.ContainsKey(commandKey.ToLower()))
            {
                return _command[commandKey.ToLower()];
            }
            throw new InvalidOperationException("Command not found");
        }
    }
}
