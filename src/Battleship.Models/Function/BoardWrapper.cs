using Newtonsoft.Json;

namespace Battleship.Models.Function
{
    public class BoardWrapper
    {
        [JsonProperty(PropertyName = "displayBoard")]
        public string[,] DisplayBoard { get; set; }
    }
}
