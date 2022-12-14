using MoviesDb.Model.Common;
using System;
using System.Collections.Generic;

namespace MoviesDb.Service.Common
{
    public interface IMovieService
    {
        IEnumerable<IMovieDomainModel> GetAll(String SearchString);
    }
}
