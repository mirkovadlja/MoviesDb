using MoviesDb.DAL.Common;
using System.Data.Entity;

namespace MoviesDb.DAL
{
    public partial class MoviesDbContext : DbContext, IMoviesDbContext
    {
        public MoviesDbContext()
            : base("name=MovieDB")
        {
        }

        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<MovieCredit> MovieCredits { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<MovieGenre> MovieGenres { get; set; }
        public virtual DbSet<PersonMovieCredit> PersonMovieCredits { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>()
                .HasMany(e => e.MovieGenres)
                .WithRequired(e => e.Genre)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Movie>()
                .HasMany(e => e.MovieGenres)
                .WithRequired(e => e.Movie)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Movie>()
                .HasMany(e => e.PersonMovieCredits)
                .WithRequired(e => e.Movie)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MovieCredit>()
                .HasMany(e => e.PersonMovieCredits)
                .WithRequired(e => e.MovieCredit)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.PersonMovieCredits)
                .WithRequired(e => e.Person)
                .WillCascadeOnDelete(false);
        }
    }
}
