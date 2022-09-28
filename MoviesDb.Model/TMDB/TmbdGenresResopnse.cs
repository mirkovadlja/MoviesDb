using MoviesDb.Model.Common.TMBD;
using Newtonsoft.Json;
using System.Collections.Generic;
using MoviesDb.Common;

namespace MoviesDb.Model.TMBD
{
    public class TmbdGenresResponse: ITmbdGenresResponse
    {     
        [JsonProperty("genres")]
        [JsonConverter(typeof(CustomJsonConverter<IEnumerable<TmbdGenre>>))]
        public IEnumerable<ITmbdGenre> Genres { get; set; }
    }
}
