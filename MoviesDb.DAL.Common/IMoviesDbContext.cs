using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace MoviesDb.DAL.Common
{
    public interface IMoviesDbContext : IDisposable
    {
        DbEntityEntry Entry(object entity);
        Task<int> SaveChangesAsync();
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbSet Set(Type entityType);
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
