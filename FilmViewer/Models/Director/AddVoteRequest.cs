using Newtonsoft.Json;

namespace FilmViewer.Models.Director
{
    public class AddVoteRequest
    {
        [JsonProperty("directorId")]
        public int DirectorId { get; set; }
        [JsonProperty("stars")]
        public int Stars { get; set; }
    }
}