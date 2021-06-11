using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Musicalog.Data.Contexts;
using Musicalog.Domain;

namespace Musicalog.Data.Repositories
{
    public interface IAlbumRepository : IRepositoryBase<Album>
    {
        Task<List<Album>> GetByArtistAndTitleAsync(string artist, string title);
    }
    
    // ReSharper disable once UnusedType.Global
    public class AlbumRepository : RepositoryBase<Album>, IAlbumRepository
    {
        public AlbumRepository(IMusicalogContext context) : base(context)
        {
        }

        public Task<List<Album>> GetByArtistAndTitleAsync(string artist, string title)
        {
            return Context.Albums.Where(a => EF.Functions.Like(a.ArtistName, $"%{artist}%") &&
                                             EF.Functions.Like(a.Title, $"%{title}%"))
                .ToListAsync();
        }
    }
}