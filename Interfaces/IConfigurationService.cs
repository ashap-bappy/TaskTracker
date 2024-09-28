namespace TaskTracker.Interfaces
{
    public interface IConfigurationService
    {
        void SetConfigValue(string key, object value);
        T GetConfigValue<T>(string key);
    }
}
