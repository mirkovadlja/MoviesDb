using AutoMapper;

namespace MoviesDb.Repository.Mapping
{
    public class AutoMapperConfiguration
    {
        public MapperConfiguration Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfiles());
            });
            return config;
        
        }
    }
}