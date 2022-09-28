using MoviesDb.Common;
using MoviesDb.Model.Common;
using MoviesDb.Model.Common.TMBD;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MoviesDb.Model.TMBD
{
    public class TmbdMovieCredit: ITmbdMovieCredit
    {
        public Guid Id { get; set; }
        [JsonProperty("id")]
        public int TmbdId{ get; set; }
        [JsonProperty("cast")]
        [JsonConverter(typeof(CustomJsonConverter<IEnumerable<TmbdCast>>))]
        public IEnumerable<ITmbdCast> Cast { get; set; }

        [JsonProperty("crew")]
        [JsonConverter(typeof(CustomJsonConverter<IEnumerable<TmbdCrew>>))]
        public IEnumerable<ITmbdCrew> Crew { get; set; }
    }
}
