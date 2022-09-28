using System;

namespace MoviesDb.Model.Common
{
    public interface ICoreModel
    {
        Guid Id { get; set; }
        int TmdbId { get; set; }
        string Name { get; set; }
        DateTime DateCreated { get; set; }
    }
}
