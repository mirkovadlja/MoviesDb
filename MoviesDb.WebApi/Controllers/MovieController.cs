using MoviesDb.Model.Common;
using MoviesDb.Service;
using MoviesDb.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MoviesDb.WebAPI.Controllers
{
    [System.Web.Http.RoutePrefix("api/movies")]
    public class MovieController : ApiController
    {
        private readonly IMovieService MovieService;
        public MovieController(IMovieService movieService){
            MovieService = movieService;
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("")]
        public HttpResponseMessage GetAll(String searchString = null)
        {
            IEnumerable<IMovieDomainModel> movies = MovieService.GetAll(searchString);
            return Request.CreateResponse(HttpStatusCode.OK, movies);
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
