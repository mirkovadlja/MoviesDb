using MoviesDb.Model.Common.TMBD;
using Newtonsoft.Json;

namespace MoviesDb.Model.TMBD
{
    public class TmbdCast: ITmbdCast
    {
        [JsonProperty("id")]
        public int Id{ get; set; }

        [JsonProperty("original_name")]
        public string Name { get; set; }
    }
}
