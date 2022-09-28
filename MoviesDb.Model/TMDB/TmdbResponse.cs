using MoviesDb.Model.Common.TMBD;
using Newtonsoft.Json;
using System.Collections.Generic;
using MoviesDb.Common;

namespace MoviesDb.Model.TMBD
{
    public class TmbdResponse: ITmbdResponse
    {
        [JsonProperty("page")]
        public int Page { get; set; }
        [JsonProperty("results")]
        [JsonConverter(typeof(CustomJsonConverter<IEnumerable<TmbdMoviePopular>>))]
        public IEnumerable<ITmbdMoviePopular> Results { get; set; }
        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }
        [JsonProperty("total_results")]
        public int TotalResults { get; set; }
    }
}
