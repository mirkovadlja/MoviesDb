
using MoviesDb.Model.Common;
using System;
using System.Collections.Generic;

namespace MoviesDb.Repository.Common  
{
    public interface IMovieGenreRepository
    {
        IEnumerable<IMovieGenreDomainModel> GetAll();
        void Insert(IMovieGenreDomainModel movieGenre);
        void InsertRange(IEnumerable<IMovieGenreDomainModel> movieGenres);
        void Save();
    }
}