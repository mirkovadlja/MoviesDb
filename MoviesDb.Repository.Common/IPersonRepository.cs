
using MoviesDb.Model.Common;
using System;
using System.Collections.Generic;

namespace MoviesDb.Repository.Common  
{
    public interface IPersonRepository
    {
        IEnumerable<IPersonDomainModel> GetAll();
        IPersonDomainModel GetById(Guid id);
        void Insert(IPersonDomainModel person);
        void InsertRange(IEnumerable<IPersonDomainModel> persons);
        void Update(IPersonDomainModel person);
        void Delete(Guid id);
        void Save();
    }
}