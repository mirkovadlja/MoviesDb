using System;

namespace MoviesDb.Model.Common
{
    public interface IMovieGenreDomainModel
    {
        Guid MovieId { get; set; }
        Guid GenreId { get; set; }
        DateTime DateCreated { get; set; }
    }
}
