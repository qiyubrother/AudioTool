using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Media;

namespace AudioTool
{
    public class AppConfig
    {
        private static readonly string ConfigDirectory = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "AudioTool");

        private static readonly string ConfigFilePath = Path.Combine(ConfigDirectory, "config.json");

        public double? WindowLeft { get; set; }
        public double? WindowTop { get; set; }
        public double Opacity { get; set; } = 1.0;
        public bool TopMost { get; set; } = true;
        
        [JsonIgnore]
        public Color? MicrophoneColor { get; set; }
        
        [JsonIgnore]
        public Color? SpeakerColor { get; set; }
        
        public string? MicrophoneColorString
        {
            get => MicrophoneColor?.ToString();
            set
            {
                if (value != null)
                {
                    try
                    {
                        MicrophoneColor = (Color)ColorConverter.ConvertFromString(value);
                    }
                    catch { }
                }
                else
                {
                    MicrophoneColor = null;
                }
            }
        }
        
        public string? SpeakerColorString
        {
            get => SpeakerColor?.ToString();
            set
            {
                if (value != null)
                {
                    try
                    {
                        SpeakerColor = (Color)ColorConverter.ConvertFromString(value);
                    }
                    catch { }
                }
                else
                {
                    SpeakerColor = null;
                }
            }
        }

        public static AppConfig Load()
        {
            try
            {
                if (File.Exists(ConfigFilePath))
                {
                    var json = File.ReadAllText(ConfigFilePath);
                    var config = JsonSerializer.Deserialize<AppConfig>(json);
                    return config ?? new AppConfig();
                }
            }
            catch { }

            return new AppConfig();
        }

        public void Save()
        {
            try
            {
                if (!Directory.Exists(ConfigDirectory))
                {
                    Directory.CreateDirectory(ConfigDirectory);
                }

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                var json = JsonSerializer.Serialize(this, options);
                File.WriteAllText(ConfigFilePath, json);
            }
            catch { }
        }
    }
}

