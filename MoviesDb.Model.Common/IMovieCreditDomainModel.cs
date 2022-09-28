using System;

namespace MoviesDb.Model.Common
{
    public interface IMovieCreditDomainModel
    {
        Guid Id { get; set; }
        string Name { get; set; }
    }
}
