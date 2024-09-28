using TaskTracker.Interfaces;

namespace TaskTracker.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private Dictionary<string, object> _settings = new Dictionary<string, object>();

        public void SetConfigValue(string key, object value)
        {
            _settings[key] = value;
        }

        public T GetConfigValue<T>(string key)
        {
            if (_settings.ContainsKey(key))
            {
                return (T)_settings[key];
            }
            throw new KeyNotFoundException($"Key '{key}' not found in configuration.");
        }
    }
}
