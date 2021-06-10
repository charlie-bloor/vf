using System.Threading;
using System.Threading.Tasks;

namespace Musicalog.Data.Contexts
{
    public interface IDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        ValueTask<TEntity> FindAsync<TEntity>(params object[] keyValues)
            where TEntity : class;
    }
}