using System;
using System.Collections.Generic;

namespace MoviesDb.Model.Common.TMBD
{
    public interface ITmbdMoviePopular
    {
        int Id{ get; set; }
        string OriginalTitle { get; set; }
        DateTime? ReleaseDate { get; set; }
        string Title { get; set; }
        IEnumerable<int> GenreIds { get; set; }
    }
}
