
using MoviesDb.Model.Common;
using System;
using System.Collections.Generic;

namespace MoviesDb.Repository.Common  
{
    public interface IMovieCreditRepository
    {
        IEnumerable<IMovieCreditDomainModel> GetAll();
        void Save();
    }
}