using System.IO;
using MouseSimLib;
using Newtonsoft.Json;

namespace ViaClicker
{
    public static class ClickerConfigService
    {
        private const string FileName = "config.json";

        public static ConfigClicker Load()
        {
            if (!File.Exists(FileName))
                return CreateDefault();

            var json = File.ReadAllText(FileName);
            var config = JsonConvert.DeserializeObject<ConfigClicker>(json);

            return config ?? CreateDefault();
        }

        public static void Save(ConfigClicker config)
        {
            var json = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(FileName, json);
        }

        private static ConfigClicker CreateDefault()
        {
            var cfg = new ConfigClicker
            {
                IntervalMs = 100,
                ClickButton = MouseButton.Left,
                StartKey = ConsoleKey.F6,
                PauseKey = ConsoleKey.F7
            };

            Save(cfg);
            return cfg;
        }
    }
}
