using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Musicalog.Data.Contexts;
using Musicalog.Data.Exceptions;

namespace Musicalog.Data.Repositories
{
    public interface IRepositoryBase
    {
    }

    /// <summary>
    /// Repository base class containing overridable common functionality.
    /// </summary>
    /// <remarks>
    /// Saving in the repository breaks EF's implementation of Unit of Work
    /// when we compose multiple repository calls, e.g. from a service, because
    /// we should only be saving once per request.
    /// This could be worked around with custom Unit of Work functionality
    /// but that's another story!
    /// </remarks>
    public interface IRepositoryBase<TEntity> : IRepositoryBase where TEntity : class
    {
        Task AddAsync(TEntity entity);

        Task<TEntity> SingleAsync(params object[] ids);

        Task<TEntity> SingleOrDefaultAsync(params object[] ids);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task RemoveAsync(TEntity entity);
        
        Task UpdateAsync(TEntity entity);
    }
    
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected RepositoryBase(IMusicalogContext context)
        {
            Context = context;
        }

        protected IMusicalogContext Context { get; }

        public async Task AddAsync(TEntity entity)
        {
            await ((DbContext)Context).Set<TEntity>().AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public async Task<TEntity> SingleAsync(params object[] ids)
        {
            return await SingleOrDefaultAsync(ids) ?? throw new EntityNotFoundException(typeof(TEntity), ids);
        }

        public virtual async Task<TEntity> SingleOrDefaultAsync(params object[] id)
        {
            return await Context.FindAsync<TEntity>(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await ((DbContext)Context).Set<TEntity>().ToListAsync();
        }

        public virtual async Task RemoveAsync(TEntity entity)
        {
            ((DbContext)Context).Set<TEntity>().Remove(entity);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            // EF tracks which columns have changed and will update only those columns.
            // We don't need to call Update.
            await Context.SaveChangesAsync();
        }
    }
}