using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDb.Service.Common
{
    public interface ISyncDbService
    {
        Task<bool> SyncDb();
    }
}
