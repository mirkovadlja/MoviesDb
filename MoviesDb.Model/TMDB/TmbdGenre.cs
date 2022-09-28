using MoviesDb.Model.Common.TMBD;
using Newtonsoft.Json;

namespace MoviesDb.Model.TMBD
{
    public class TmbdGenre: ITmbdGenre
    {
        [JsonProperty("id")]
        public int Id{ get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
