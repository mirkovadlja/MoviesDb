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
    public class MovieGenreRepository : IMovieGenreRepository
    {
        private readonly IMapper Mapper;
        private readonly MoviesDbContext _context;
        public MovieGenreRepository()
        {
            _context = new MoviesDbContext();
            Mapper = new AutoMapperConfiguration().Configure().CreateMapper();
        }
        public IEnumerable<IMovieGenreDomainModel> GetAll()
        {
            return Mapper.Map<IEnumerable<IMovieGenreDomainModel>>( _context.MovieGenres.ToList());
        }
        public void Insert(IMovieGenreDomainModel movieGenre)
        {
            _context.MovieGenres.Add(Mapper.Map<MovieGenre>(movieGenre));
        }
        public void InsertRange(IEnumerable<IMovieGenreDomainModel> movieGenres)
        {
            _context.MovieGenres.AddRange(Mapper.Map<IEnumerable<MovieGenre>>(movieGenres));
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