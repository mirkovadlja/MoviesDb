namespace MoviesDb.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MovieGenre")]
    public partial class MovieGenre
    {
        [Key]
        [Column(Order = 0)]
        public Guid MovieId { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid GenreId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateCreated { get; set; }

        public virtual Genre Genre { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
