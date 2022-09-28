using MoviesDb.Model.Common;
using System;

namespace MoviesDb.Model
{
    public class PersonMovieCreditDomainModel: IPersonMovieCreditDomainModel
    {
        public Guid MovieId { get; set; }
        public Guid PersonId { get; set; }
        public Guid MovieCreditId { get; set; }

    }
}
