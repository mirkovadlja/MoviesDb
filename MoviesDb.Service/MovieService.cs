using MoviesDb.Model.Common;
using MoviesDb.Repository.Common;
using MoviesDb.Service.Common;
using System;
using System.Collections.Generic;

namespace MoviesDb.Service
{
    public class MovieService: IMovieService
    {
        private IMovieRepository MovieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            MovieRepository = movieRepository;
        }

        public IEnumerable<IMovieDomainModel> GetAll(String searchString)
        {
            return MovieRepository.GetAll(searchString);
        }
    }
}
