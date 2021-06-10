using System.Threading.Tasks;
using Musicalog.Data.Repositories;
using Musicalog.Domain;

namespace Musicalog.Core.Services
{
    /// <summary>
    /// Wrapper service
    /// E.g. to ensure that SignalR callbacks are raised when we make changes
    /// (Not implemented here)
    /// </summary>
    public interface IAlbumService
    {
        Task AddAsync(Album entity);

        Task RemoveAsync(Album entity);
        
        Task UpdateAsync(Album entity);
    }
    
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;

        public AlbumService(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }
        
        public async Task AddAsync(Album entity)
        {
            await _albumRepository.AddAsync(entity);
            // TODO: e.g. issue AlbumAdded SignalR callback
        }

        public async Task RemoveAsync(Album entity)
        {
            await _albumRepository.RemoveAsync(entity);
            // TODO: e.g. issue AlbumRemoved SignalR callback
        }

        public Task UpdateAsync(Album entity)
        {
            throw new System.NotImplementedException();
            // TODO: e.g. issue AlbumUpdated SignalR callback
        }
    }
}