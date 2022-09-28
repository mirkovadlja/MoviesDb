
using MoviesDb.DAL.Common;
using MoviesDb.Model.Common;
using System;
using System.Collections.Generic;

namespace MoviesDb.Repository.Common  
{
    public interface IMovieRepository
    {
        IEnumerable<IMovieDomainModel> GetAll();
        IMovieDomainModel GetById(Guid id);
        void Insert(IMovieDomainModel movie);
        void InsertRange(IEnumerable<IMovieDomainModel> movies);
        void Update(IMovieDomainModel movie);
        void Delete(Guid id);
        void Save();
    }
}