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
    public interface IRepositoryBase<TEntity> : IRepositoryBase where TEntity : class
    {
        Task AddAsync(TEntity entity);

        Task<TEntity> SingleAsync(params object[] ids);

        Task<TEntity> SingleOrDefaultAsync(params object[] ids);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task RemoveAsync(TEntity entity);
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
    }
}