using System.IO;
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
            return JsonConvert.DeserializeObject<ConfigClicker>(json);
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
