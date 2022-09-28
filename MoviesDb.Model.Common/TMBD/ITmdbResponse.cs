using MoviesDb.Model.Common;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MoviesDb.Model.Common.TMBD
{
    public interface ITmbdResponse
    {
        int Page { get; set; }
        IEnumerable<ITmbdMoviePopular> Results { get; set; }
        int TotalPages { get; set; }
        int TotalResults { get; set; }
    }
}
