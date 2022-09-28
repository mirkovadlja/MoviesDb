using AutoMapper;
using MoviesDb.DAL;
using MoviesDb.DAL.Common;
using MoviesDb.Model.Common;
using MoviesDb.Repository.Common;
using MoviesDb.Repository.Mapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
namespace MoviesDb.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly IMapper Mapper;
        private readonly MoviesDbContext _context;
        public MovieRepository()
        {
            _context = new MoviesDbContext();
            Mapper = new AutoMapperConfiguration().Configure().CreateMapper();
        }
        public IEnumerable<IMovieDomainModel> GetAll()
        {
            var moviesList = _context.Movies
                .Include(movie => movie.MovieGenres.Select(genre => genre.Genre))
                .Include(movie => movie.PersonMovieCredits.Select(pmc => pmc.Person))
                .Include(movie => movie.PersonMovieCredits.Select(pmc => pmc.MovieCredit))
                .ToList();
            List<IMovieDomainModel> resultList = new List<IMovieDomainModel>();
            foreach (Movie movie in moviesList) { 
                //Mapping genres
                IMovieDomainModel mappedMovie = Mapper.Map<IMovieDomainModel>(movie);
                List<IGenreDomainModel> resultGenres = new List<IGenreDomainModel>();
                foreach (var genre in movie.MovieGenres) {
                    IGenreDomainModel mappedGenre = Mapper.Map<IGenreDomainModel>(genre.Genre);
                    resultGenres.Add(mappedGenre);
                }
                mappedMovie.Genres = resultGenres;
                //Mapping persons
                List<IPersonDomainModel> actors = new List<IPersonDomainModel>();
                List<IPersonDomainModel> directors = new List<IPersonDomainModel>();
                foreach (var pmc in movie.PersonMovieCredits) 
                {
                    if (pmc.MovieCredit.Name == "Actor")
                    {
                        IPersonDomainModel mappedActor = Mapper.Map<IPersonDomainModel>(pmc.Person);
                        actors.Add(mappedActor);
                    }
                    else if (pmc.MovieCredit.Name == "Director") {
                        IPersonDomainModel mappedDirector = Mapper.Map<IPersonDomainModel>(pmc.Person);
                        directors.Add(mappedDirector);
                    }
                }
                mappedMovie.Actors = actors;
                mappedMovie.Directors = directors;
                resultList.Add(mappedMovie);
            }
            return resultList;
        }
        public IMovieDomainModel GetById(Guid id)
        {
            return Mapper.Map<IMovieDomainModel>(_context.Movies.Find(id));
        }
        public void Insert(IMovieDomainModel movie)
        {
            _context.Movies.Add(Mapper.Map<Movie>(movie));
        }
        public void InsertRange(IEnumerable<IMovieDomainModel> movies)
        {
            _context.Movies.AddRange(Mapper.Map<IEnumerable<Movie>>(movies));
        }
        public void Update(IMovieDomainModel movie)
        {
            _context.Entry(Mapper.Map<Movie>(movie)).State = EntityState.Modified;
        }
        public void Delete(Guid id)
        {
            Movie movie = _context.Movies.Find(id);
            _context.Movies.Remove(movie);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}