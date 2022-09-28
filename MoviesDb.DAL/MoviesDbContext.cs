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
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Genre>()
                .HasMany(e => e.MovieGenres)
                .WithRequired(e => e.Genre)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Movie>()
                .Property(e => e.OriginalTitle)
                .IsUnicode(false);

            modelBuilder.Entity<Movie>()
                .HasMany(e => e.MovieGenres)
                .WithRequired(e => e.Movie)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Movie>()
                .HasMany(e => e.PersonMovieCredits)
                .WithRequired(e => e.Movie)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MovieCredit>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<MovieCredit>()
                .HasMany(e => e.PersonMovieCredits)
                .WithRequired(e => e.MovieCredit)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.PersonMovieCredits)
                .WithRequired(e => e.Person)
                .WillCascadeOnDelete(false);
        }
    }
}
