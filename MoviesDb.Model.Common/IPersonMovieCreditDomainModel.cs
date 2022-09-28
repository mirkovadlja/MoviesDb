using System;

namespace MoviesDb.Model.Common
{
    public interface IPersonMovieCreditDomainModel
    {
        Guid MovieId { get; set; }
        Guid PersonId { get; set; }
        Guid MovieCreditId { get; set; }
    }
}
