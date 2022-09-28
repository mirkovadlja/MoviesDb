using System;
using System.Collections.Generic;

namespace MoviesDb.Model.Common
{
    public interface IMovieDomainModel : ICoreModel
    {
        string OriginalTitle { get; set; }
        DateTime? ReleaseDate { get; set; }
        IEnumerable<IGenreDomainModel> Genres { get; set; }
        IEnumerable<IPersonDomainModel> Actors { get; set; }
        IEnumerable<IPersonDomainModel> Directors { get; set; }
    }
}
