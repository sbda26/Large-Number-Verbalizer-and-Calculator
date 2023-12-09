using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ApplicationJsonSettings.Library
{
    public enum SettingsJsonNotFound
    {
        Ignore,
        Error,
        Create
    }

    public class ApplicationJsonSettingsClass<T>
    {
        private readonly string _directory = Directory.GetCurrentDirectory();
        private readonly FileInfo _jsonFile;

        public ApplicationJsonSettingsClass()
        {
            _jsonFile = new FileInfo($"{_directory}\\appsettings.json");
        }

        public T? GetSettings
            (SettingsJsonNotFound actionIfSettingsJsonNotFound, bool reloadOnChange)
        {
            bool optional = GetOptionalStatus(actionIfSettingsJsonNotFound);

            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(_directory)
                .AddJsonFile(_jsonFile.Name, optional: optional, reloadOnChange: reloadOnChange)
                .AddEnvironmentVariables()
                .Build();

            T? settings = config.GetRequiredSection("Settings").Get<T>();

            return settings;
        }

        public void SaveSettings(T? settings)
        {
            EnclosingSettingsClass<T?> enclosingSettings = new() { Settings = settings };
            string json = JsonConvert.SerializeObject(enclosingSettings, Formatting.Indented);

            File.WriteAllText(_jsonFile.FullName, json);
        }

        private bool GetOptionalStatus(SettingsJsonNotFound actionIfSettingsJsonNotFound)
        {
            switch(actionIfSettingsJsonNotFound)
            {
                case SettingsJsonNotFound.Ignore:
                    return true;
                case SettingsJsonNotFound.Error:
                    if (_jsonFile.Exists == false)
                        throw new FileNotFoundException($"Settings file not found: {_jsonFile.FullName}");
                    else
                        return false;
                case SettingsJsonNotFound.Create:
                    {
                        File.Create(_jsonFile.FullName);
                        return false;
                    }
                default:
                    throw new NotImplementedException($"{actionIfSettingsJsonNotFound}");
            }
        }
    }
}
