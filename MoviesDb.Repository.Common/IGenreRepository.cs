
using MoviesDb.Model.Common;
using System;
using System.Collections.Generic;

namespace MoviesDb.Repository.Common  
{
    public interface IGenreRepository
    {
        IEnumerable<IGenreDomainModel> GetAll();
        IGenreDomainModel GetById(Guid id);
        void Insert(IGenreDomainModel genre);
        void InsertRange(IEnumerable<IGenreDomainModel> genres);
        void Update(IGenreDomainModel genre);
        void Delete(Guid id);
        void Save();
    }
}