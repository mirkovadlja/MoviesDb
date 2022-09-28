using MoviesDb.Model.Common.TMBD;
using Newtonsoft.Json;

namespace MoviesDb.Model.TMBD
{
    public class TmbdCrew: ITmbdCrew
    {
        [JsonProperty("id")]
        public int Id{ get; set; }

        [JsonProperty("job")]
        public string Job { get; set; }
        
        [JsonProperty("original_name")]
        public string Name { get; set; }

    }
}
