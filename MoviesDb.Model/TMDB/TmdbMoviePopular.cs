using MoviesDb.Model.Common;
using MoviesDb.Model.Common.TMBD;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MoviesDb.Model.TMBD
{
    public class TmbdMoviePopular: ITmbdMoviePopular
    {
        [JsonProperty("id")]
        public int Id{ get; set; }
        [JsonProperty("original_title")]
        public string OriginalTitle { get; set; }
        [JsonProperty("release_date")]
        public DateTime? ReleaseDate { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("genre_ids")]
        public IEnumerable<int> GenreIds { get; set; }
    }
}
