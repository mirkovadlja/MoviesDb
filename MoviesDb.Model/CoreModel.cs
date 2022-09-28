using MoviesDb.Model.Common;
using System;

namespace MoviesDb.Model
{
    public class CoreModel: ICoreModel
    {
        public Guid Id { get; set; }
        public int TmdbId { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
