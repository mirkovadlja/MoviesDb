using System.Collections.Generic;

namespace MoviesDb.Model.Common.TMBD
{
    public interface ITmbdGenresResponse
    {
        IEnumerable<ITmbdGenre> Genres { get; set; }
    }
}
