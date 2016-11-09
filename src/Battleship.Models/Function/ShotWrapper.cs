using Newtonsoft.Json;

namespace Battleship.Models.Function
{
    public class ShotWrapper
    {
        [JsonProperty(PropertyName = "displayBoard")]
        public string[,] DisplayBoard { get; set; }
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set;  }
    }
}
