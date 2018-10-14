using Newtonsoft.Json;

namespace FilmViewer.Models.Actors
{
    public class AddVoteRequest
    {
        [JsonProperty("actorId")]
        public int ActorId { get; set; }
        [JsonProperty("stars")]
        public int Stars { get; set; }
    }
}