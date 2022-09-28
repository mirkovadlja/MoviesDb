[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(MoviesDb.WebAPI.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(MoviesDb.WebAPI.App_Start.NinjectWebCommon), "Stop")]

namespace MoviesDb.WebAPI.App_Start
{
    using System;
    using System.Web;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using MoviesDb.DAL;
    using MoviesDb.DAL.Common;
    using MoviesDb.Repository;
    using MoviesDb.Repository.Common;
    using MoviesDb.Service;
    using MoviesDb.Service.Common;
    using MoviesDb.WebAPI.Controllers;
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application.
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
           
            kernel.Bind<ISyncDbService>().To<SyncDbService>().WhenInjectedInto(typeof(SyncDbController));
            kernel.Bind<IMoviesDbContext>().To<MoviesDbContext>();
            kernel.Bind<IMovieRepository>().To<MovieRepository>();
            kernel.Bind<IGenreRepository>().To<GenreRepository>();
            kernel.Bind<IMovieCreditRepository>().To<MovieCreditRepository>();
            kernel.Bind<IPersonMovieCreditRepository>().To<PersonMovieCreditRepository>();
            kernel.Bind<IMovieGenreRepository>().To<MovieGenreRepository>();
            kernel.Bind<IPersonRepository>().To<PersonRepositroy>();
            kernel.Bind<IMovieService>().To<MovieService>();
        }
    }
}
