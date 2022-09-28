using AutoMapper;
using MoviesDb.DAL;
using MoviesDb.Model.Common;
using MoviesDb.Repository.Common;
using MoviesDb.Repository.Mapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
namespace MoviesDb.Repository
{
    public class PersonRepositroy : IPersonRepository
    {
        private readonly IMapper Mapper;
        private readonly MoviesDbContext _context;
        public PersonRepositroy()
        {
            _context = new MoviesDbContext();
            Mapper = new AutoMapperConfiguration().Configure().CreateMapper();
        }
        public IEnumerable<IPersonDomainModel> GetAll()
        {
            return Mapper.Map<IEnumerable<IPersonDomainModel>>( _context.People.ToList());
        }
        public IPersonDomainModel GetById(Guid id)
        {
            return Mapper.Map<IPersonDomainModel>(_context.People.Find(id));
        }
        public void Insert(IPersonDomainModel person)
        {
            _context.People.Add(Mapper.Map<Person>(person));
        }
        public void InsertRange(IEnumerable<IPersonDomainModel> person)
        {
            _context.People.AddRange(Mapper.Map<IEnumerable<Person>>(person));
        }
        public void Update(IPersonDomainModel person)
        {
            _context.Entry(Mapper.Map<Movie>(person)).State = EntityState.Modified;
        }
        public void Delete(Guid id)
        {
            Person person = _context.People.Find(id);
            _context.People.Remove(person);
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