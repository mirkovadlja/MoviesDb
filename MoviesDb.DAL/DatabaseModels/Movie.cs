namespace MoviesDb.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Movie")]
    public partial class Movie
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Movie()
        {
            MovieGenres = new HashSet<MovieGenre>();
            PersonMovieCredits = new HashSet<PersonMovieCredit>();
        }

        public Guid Id { get; set; }

        public int TmdbId { get; set; }

        [StringLength(100)]
        public string OriginalTitle { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ReleaseDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateCreated { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MovieGenre> MovieGenres { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonMovieCredit> PersonMovieCredits { get; set; }
    }
}
