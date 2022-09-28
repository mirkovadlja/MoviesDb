
using MoviesDb.Model.Common;
using System;
using System.Collections.Generic;

namespace MoviesDb.Repository.Common  
{
    public interface IPersonMovieCreditRepository
    {
        IEnumerable<IPersonMovieCreditDomainModel> GetAll();
        IPersonMovieCreditDomainModel GetById(Guid id);
        void Insert(IPersonMovieCreditDomainModel personMovieCredit);
        void InsertRange(IEnumerable<IPersonMovieCreditDomainModel> personMovieCredits);
        void Update(IPersonMovieCreditDomainModel personMovieCredit);
        void Delete(Guid id);
        void Save();
    }
}