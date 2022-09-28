using MoviesDb.Model.Common;
using System;

namespace MoviesDb.Model
{
    public class MovieGenreDomainModel : IMovieGenreDomainModel
    {
        public Guid MovieId{ get; set; }

        public Guid GenreId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
