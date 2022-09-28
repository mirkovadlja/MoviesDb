using System;
using System.Collections.Generic;

namespace MoviesDb.Model.Common.TMBD
{
    public interface ITmbdMovieCredit
    {
        Guid Id{ get; set; }
        int TmbdId { get; set; }
        IEnumerable<ITmbdCast> Cast { get; set; }
        IEnumerable<ITmbdCrew> Crew { get; set; }
    }
}
