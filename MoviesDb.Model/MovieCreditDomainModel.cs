using MoviesDb.Model.Common;
using System;

namespace MoviesDb.Model
{
    public class MovieCreditDomainModel: IMovieCreditDomainModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
