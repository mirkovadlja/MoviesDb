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
    public class PersonMovieCreditRepository : IPersonMovieCreditRepository
    {
        private readonly IMapper Mapper;
        private readonly MoviesDbContext _context;
        public PersonMovieCreditRepository()
        {
            _context = new MoviesDbContext();
            Mapper = new AutoMapperConfiguration().Configure().CreateMapper();
        }
        public IEnumerable<IPersonMovieCreditDomainModel> GetAll()
        {
            return Mapper.Map<IEnumerable<IPersonMovieCreditDomainModel>>( _context.PersonMovieCredits.ToList());
        }
        public IPersonMovieCreditDomainModel GetById(Guid id)
        {
            return Mapper.Map<IPersonMovieCreditDomainModel>(_context.PersonMovieCredits.Find(id));
        }
        public void Insert(IPersonMovieCreditDomainModel personMovieCredit)
        {
            _context.PersonMovieCredits.Add(Mapper.Map<PersonMovieCredit>(personMovieCredit));
        }
        public void InsertRange(IEnumerable<IPersonMovieCreditDomainModel> personMovieCredit)
        {
            _context.PersonMovieCredits.AddRange(Mapper.Map<IEnumerable<PersonMovieCredit>>(personMovieCredit));
        }
        public void Update(IPersonMovieCreditDomainModel personMovieCredit)
        {
            _context.Entry(Mapper.Map<PersonMovieCredit>(personMovieCredit)).State = EntityState.Modified;
        }
        public void Delete(Guid id)
        {
            PersonMovieCredit personMovieCredit = _context.PersonMovieCredits.Find(id);
            _context.PersonMovieCredits.Remove(personMovieCredit);
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