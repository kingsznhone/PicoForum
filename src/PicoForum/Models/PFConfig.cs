using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace PicoForum.Models
{
    [Serializable]
    public class PFConfig
    {
        public string ForumName { get; set; } = "Pico Forum";
        public bool AllowGuest { get; set; } = true;
        public bool AllowRegister { get; set; } = true;
        public int QueryLimitPost { get; set; } = 10;
        public int QueryLimitReply { get; set; } = 10;
        public int QueryLimitSearch { get; set; } = 10;

        //default 1m image
        public int MaxAvatarSize { get; set; } = 1024 * 1024;

        public int JpegCompressQuality { get; set; } = 80;

        private string configFilePath;

        public PFConfig(string path)
        {
            configFilePath = path;
            if (File.Exists(configFilePath))
            {
                string jsonstring = File.ReadAllText(configFilePath);
                PFConfig instance = JsonSerializer.Deserialize<PFConfig>(jsonstring);

                ForumName = instance.ForumName;
                AllowGuest = instance.AllowGuest;
                AllowRegister = instance.AllowRegister;
                QueryLimitPost = instance.QueryLimitPost;
                QueryLimitReply = instance.QueryLimitReply;
                QueryLimitSearch = instance.QueryLimitSearch;
                MaxAvatarSize = instance.MaxAvatarSize;
                JpegCompressQuality = instance.JpegCompressQuality;
            }
            SaveChange();
        }

        public PFConfig()
        { }

        public bool SaveChange()
        {
            //try
            {
                string jsonString = JsonSerializer.Serialize(this, new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                    WriteIndented = true
                });
                File.WriteAllText(configFilePath, jsonString);
                return true;
            }
            //catch (Exception ex)
            {
                // Console.WriteLine($"Error saving changes: {ex.Message}");
                //return false;
            }
        }
    }
}