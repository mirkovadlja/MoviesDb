using MoviesDb.Model.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace MoviesDb.Model
{
    public class MovieDomainModel: CoreModel, IMovieDomainModel
    {
        public string OriginalTitle { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public IEnumerable<IGenreDomainModel> Genres { get; set; }
        public IEnumerable<IPersonDomainModel> Actors { get; set; }
        public IEnumerable<IPersonDomainModel> Directors { get; set; }
    }
}
