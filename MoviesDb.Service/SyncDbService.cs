using AutoMapper;
using AutoMapper.Configuration.Conventions;
using MoviesDb.Model;
using MoviesDb.Model.Common;
using MoviesDb.Model.Common.TMBD;
using MoviesDb.Model.TMBD;
using MoviesDb.Repository.Common;
using MoviesDb.Service.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Channels;
using System.Threading.Tasks;

namespace MoviesDb.Service
{
    public class SyncDbService: ISyncDbService
    {
        const string baseUrl = "https://api.themoviedb.org/3/";
        const string APIKey = "2b21594fa1aa19a22b277fc3f992fe24";
        protected HttpClient httpClient;
        protected IEnumerable<IGenreDomainModel> genreList;
        private IMovieRepository MovieRepository;
        private IGenreRepository GenreReposiory;
        private IMovieGenreRepository MovieGenreRepository;
        private IPersonRepository PersonRepository;
        private IMovieCreditRepository MovieCreditRepository;
        private IPersonMovieCreditRepository PersonMovieCreditRepository;

        public SyncDbService(IMovieRepository movieRepository, IGenreRepository genreRepository, IMovieGenreRepository movieGenreRepository, IPersonRepository personRepository, IMovieCreditRepository movieCreditRepository, IPersonMovieCreditRepository personMovieCreditRepository)
        {
            httpClient = new HttpClient();
            MovieRepository = movieRepository;
            GenreReposiory = genreRepository;
            MovieGenreRepository = movieGenreRepository;
            PersonRepository = personRepository;
            MovieCreditRepository = movieCreditRepository;
            PersonMovieCreditRepository = personMovieCreditRepository; 
        }

        public async Task<bool> SyncDb()
        {
            //Set moviecredit values from DB
            IEnumerable<IMovieCreditDomainModel> movieCreditsLookup = MovieCreditRepository.GetAll();
            //Get genres
            IEnumerable<ITmbdGenre> tmbdGenreLIst = await GetGenresAsync();
            genreList = MapGenreList(tmbdGenreLIst);
             
            //Insert genres
            GenreReposiory.InsertRange(genreList);
            GenreReposiory.Save();

            //Get popular movies
            IEnumerable<ITmbdMoviePopular> tmbdMoviesList = await GetPopularMoviesAsync();
            IEnumerable<IMovieDomainModel> movieList = MapMovieList(tmbdMoviesList);
            
            //Insert movies
            MovieRepository.InsertRange(movieList);
            MovieRepository.Save();

            //Insert movie genres
            IEnumerable<IMovieGenreDomainModel> movieGenreList = MapMovieGenreList(movieList);
            MovieGenreRepository.InsertRange(movieGenreList);
            MovieGenreRepository.Save();


            // Get movie credits
            List<IPersonDomainModel> persons = new List<IPersonDomainModel>();
            List<IPersonMovieCreditDomainModel> movieCredits = new List<IPersonMovieCreditDomainModel>();

            foreach (IMovieDomainModel movie in movieList)
            {
                await GetMovieAsync(movie, persons, movieCredits, movieCreditsLookup);
            };

            //Insert persons
            PersonRepository.InsertRange(persons);
            PersonRepository.Save();

            // Insert movie credits
            PersonMovieCreditRepository.InsertRange(movieCredits.GroupBy(a => new { a.MovieId, a.MovieCreditId, a.PersonId }).Select(g => g.First()).ToList()); 
            PersonMovieCreditRepository.Save();
            
            return true;
        }

        private async Task<IEnumerable<ITmbdMoviePopular>> GetPopularMoviesAsync() 
        {
            int currentPage = 1;
            int totalPages = 0;
            List<ITmbdMoviePopular> moviesList = new List<ITmbdMoviePopular>();
            do
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"{baseUrl}trending/movie/day?api_key={APIKey}&page={currentPage}"))
                {
                    HttpResponseMessage response = await httpClient.SendAsync(request);
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<TmbdResponse>(jsonResponse);
                    moviesList.AddRange(result.Results);
                    currentPage++;
                    totalPages = result.TotalPages;
                }
            } while (currentPage <= 3); // for testing and development used only first 3 pages but this can be changed to curentPage <= totalPages to get all
            return moviesList.GroupBy(t => t.Id).Select(g => g.First()).ToList();
        }

        private async Task<IEnumerable<ITmbdGenre>> GetGenresAsync()
        {
            List<ITmbdGenre> genreList = new List<ITmbdGenre>();

            using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"{baseUrl}genre/movie/list?api_key={APIKey}"))
            {
                HttpResponseMessage response = await httpClient.SendAsync(request);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TmbdGenresResponse>(jsonResponse);
                genreList.AddRange(result.Genres);     
            }
            return genreList;
        }

        private async Task<bool> GetMovieAsync(IMovieDomainModel movie, List<IPersonDomainModel> persons, List<IPersonMovieCreditDomainModel> movieCredits, IEnumerable<IMovieCreditDomainModel> movieCreditsLookup)
        {
            using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"{baseUrl}movie/{movie.TmdbId}/credits?api_key={APIKey}"))
            {
                HttpResponseMessage response = await httpClient.SendAsync(request);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TmbdMovieCredit>(jsonResponse);
                TmbdMovieCredit tmbdMovieCredit = new TmbdMovieCredit()
                {
                    Id = movie.Id,
                    TmbdId = result.TmbdId,
                    Crew = result.Crew.Where(crew => crew.Job == "Director").ToList(),
                    Cast = result.Cast
                };
                //Map actors
                List<IPersonDomainModel> actors = new List<IPersonDomainModel>();
                foreach (ITmbdCast cast in tmbdMovieCredit.Cast) {
                    IPersonDomainModel existingPerson = persons.Where(p => p.TmdbId == cast.Id).FirstOrDefault();
                    IPersonDomainModel person = new PersonDomainModel
                    {
                        Id = existingPerson != null ? existingPerson.Id : Guid.NewGuid(),
                        TmdbId = cast.Id,
                        Name = cast.Name
                    };
                    actors.Add(person);

                    if(existingPerson == null)
                    {
                        persons.Add(person);
                    }
                    IPersonMovieCreditDomainModel movieCredit = new PersonMovieCreditDomainModel
                    {
                        MovieCreditId = movieCreditsLookup.Where(credit => credit.Name == "Actor").FirstOrDefault().Id,
                        MovieId = movie.Id,
                        PersonId = person.Id
                    };
                    movieCredits.Add(movieCredit);
                    
                }
                movie.Actors = actors;
                //Map director/s
                List<IPersonDomainModel> directors = new List<IPersonDomainModel>();
                foreach (ITmbdCrew crew in tmbdMovieCredit.Crew)
                {
                    IPersonDomainModel existingPerson = persons.Where(p => p.TmdbId == crew.Id).FirstOrDefault();
                    IPersonDomainModel person = new PersonDomainModel
                    {
                        Id = existingPerson != null ? existingPerson.Id : Guid.NewGuid(),
                        TmdbId = crew.Id,
                        Name = crew.Name,
                    };
                    directors.Add(person);
                    if (existingPerson == null)
                    {
                        persons.Add(person);
                    }
                    IPersonMovieCreditDomainModel movieCredit = new PersonMovieCreditDomainModel
                    {
                        MovieCreditId = movieCreditsLookup.Where(credit => credit.Name == "Director").FirstOrDefault().Id,
                        MovieId = movie.Id,
                        PersonId = person.Id
                    };
                    movieCredits.Add(movieCredit);
                }
                movie.Directors = directors;
            }
            return true;
        }
       
        private IEnumerable<IGenreDomainModel> MapGenreList(IEnumerable<ITmbdGenre> tmbdGenreList) 
        {
            IEnumerable<IGenreDomainModel> genres = tmbdGenreList.Select(tmbdGenre =>
            {
                IGenreDomainModel genre = new GenreDomainModel
                {
                    Id = Guid.NewGuid(),
                    TmdbId = tmbdGenre.Id,
                    Name = tmbdGenre.Name,
                    DateCreated = DateTime.Now
                };
                return genre;
            }).ToList();
            return genres;
        }
        private IEnumerable<IMovieDomainModel> MapMovieList(IEnumerable<ITmbdMoviePopular> tmbdMovieList)
        {
            return tmbdMovieList.Select(tmbdMovie =>
            {
                IMovieDomainModel movie = new MovieDomainModel
                {
                    Id = Guid.NewGuid(),
                    TmdbId = tmbdMovie.Id,
                    OriginalTitle = tmbdMovie.OriginalTitle,
                    ReleaseDate = tmbdMovie.ReleaseDate,
                    DateCreated = DateTime.Now
                };
                if (tmbdMovie.GenreIds.Any()) {
                    List<IGenreDomainModel> movieGenres = new List<IGenreDomainModel>();
                    foreach (int tmbdGenreId in tmbdMovie.GenreIds)
                    {
                        movieGenres.Add(genreList.Where(genre => genre.TmdbId == tmbdGenreId).FirstOrDefault());
                    }
                    movie.Genres = movieGenres;
                }
                return movie;
            }).ToList();
        }
        private IEnumerable<IMovieGenreDomainModel> MapMovieGenreList(IEnumerable<IMovieDomainModel> movieList)
        {
            List<IMovieGenreDomainModel> movieGenres = new List<IMovieGenreDomainModel>();
            foreach(IMovieDomainModel movie in movieList)
            {
                foreach(IGenreDomainModel genre in movie.Genres) {
                    IMovieGenreDomainModel movieGenre = new MovieGenreDomainModel
                    {
                        MovieId = movie.Id,
                        GenreId = genre.Id,
                        DateCreated = DateTime.Now
                    };
                    movieGenres.Add(movieGenre);
                }
            }
            return movieGenres;
        }
    }
}
