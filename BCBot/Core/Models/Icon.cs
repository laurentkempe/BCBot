using Newtonsoft.Json;

namespace BCBot.Core.Models
{
    public class Icon
    {
        public string url { get; set; }

        [JsonProperty("url@2x")]
        public string url2 { get; set; }
    }
}