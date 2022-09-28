using AutoMapper;
using MoviesDb.DAL;
using MoviesDb.DAL.Common;
using MoviesDb.Model;
using MoviesDb.Model.Common;

namespace MoviesDb.Repository.Mapping
{ 
    class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<IMovieDomainModel, Movie>().ReverseMap();
            CreateMap<IGenreDomainModel, Genre>().ReverseMap();
            CreateMap<IMovieGenreDomainModel, MovieGenre>().ReverseMap();
            CreateMap<IPersonDomainModel, Person>().ReverseMap();
            CreateMap<IMovieCreditDomainModel, MovieCredit>().ReverseMap();
            CreateMap<IPersonMovieCreditDomainModel, PersonMovieCredit>().ReverseMap();

        }
    }
}