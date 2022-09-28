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
    public class GenreRepository : IGenreRepository
    {
        private readonly IMapper Mapper;
        private readonly MoviesDbContext _context;
        public GenreRepository()
        {
            _context = new MoviesDbContext();
            Mapper = new AutoMapperConfiguration().Configure().CreateMapper();
        }
        public IEnumerable<IGenreDomainModel> GetAll()
        {
            return Mapper.Map<IEnumerable<IGenreDomainModel>>( _context.Genres.ToList());
        }
        public IGenreDomainModel GetById(Guid id)
        {
            return Mapper.Map<IGenreDomainModel>(_context.Genres.Find(id));
        }
        public void Insert(IGenreDomainModel genre)
        {
            _context.Genres.Add(Mapper.Map<Genre>(genre));
        }
        public void InsertRange(IEnumerable<IGenreDomainModel> genre)
        {
            _context.Genres.AddRange(Mapper.Map<IEnumerable<Genre>>(genre));
        }
        public void Update(IGenreDomainModel genre)
        {
            _context.Entry(Mapper.Map<Movie>(genre)).State = EntityState.Modified;
        }
        public void Delete(Guid id)
        {
            Genre genre = _context.Genres.Find(id);
            _context.Genres.Remove(genre);
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