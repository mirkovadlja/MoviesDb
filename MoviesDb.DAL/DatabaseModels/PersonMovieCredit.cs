namespace MoviesDb.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PersonMovieCredit")]
    public partial class PersonMovieCredit
    {
        [Key]
        [Column(Order = 0)]
        public Guid MovieId { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid PersonId { get; set; }

        [Key]
        [Column(Order = 2)]
        public Guid MovieCreditId { get; set; }

        public virtual Movie Movie { get; set; }

        public virtual MovieCredit MovieCredit { get; set; }

        public virtual Person Person { get; set; }
    }
}
