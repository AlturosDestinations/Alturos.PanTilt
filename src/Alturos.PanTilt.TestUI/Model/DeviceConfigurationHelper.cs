using Newtonsoft.Json;
using System.IO;

namespace Alturos.PanTilt.TestUI.Model
{
    public class DeviceConfigurationHelper
    {
        private string _configDirectory = "config";

        public void SaveConfig(string configName, DeviceConfiguration config)
        {
            if (!Directory.Exists(this._configDirectory))
            {
                Directory.CreateDirectory(this._configDirectory);
            }

            var json = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText($@"{this._configDirectory}\{configName}.config", json);
        }

        public DeviceConfiguration LoadConfig(string configName)
        {
            var filePath = $@"{this._configDirectory}\{configName}.config";

            if (!File.Exists(filePath))
            {
                return null;
            }

            var json = File.ReadAllText(filePath);
            var config = JsonConvert.DeserializeObject<DeviceConfiguration>(json);
            return config;
        }
    }
}
