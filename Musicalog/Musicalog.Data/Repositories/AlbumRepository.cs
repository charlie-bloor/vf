using Musicalog.Data.Contexts;
using Musicalog.Domain;

namespace Musicalog.Data.Repositories
{
    public interface IAlbumRepository : IRepositoryBase<Album>
    {
    }
    
    public class AlbumRepository : RepositoryBase<Album>, IAlbumRepository
    {
        public AlbumRepository(IMusicalogContext context) : base(context)
        {
        }
    }
}