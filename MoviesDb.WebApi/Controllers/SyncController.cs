using MoviesDb.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MoviesDb.WebAPI.Controllers
{
    [System.Web.Http.RoutePrefix("api/syncdb")]
    public class SyncDbController : ApiController
    {
        private readonly ISyncDbService SyncSerivice;
        public SyncDbController(ISyncDbService syncService){
            SyncSerivice = syncService;
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("sync")]
        public async Task<IEnumerable<string>> Get()
        {
            await SyncSerivice.SyncDb();
            return default;
        }

        public string Get(int id)
        {
            return "value";
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
