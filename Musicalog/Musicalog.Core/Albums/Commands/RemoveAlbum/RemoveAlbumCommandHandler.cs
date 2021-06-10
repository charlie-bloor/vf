using System.Threading.Tasks;
using Musicalog.Core.Services;
using Musicalog.Data.Repositories;

namespace Musicalog.Core.Albums.Commands.RemoveAlbum
{
    // ReSharper disable once UnusedType.Global
    public class RemoveAlbumCommandHandler : IRequestHandler<RemoveAlbumCommand>
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IAlbumService _albumService;

        public RemoveAlbumCommandHandler(IAlbumRepository albumRepository,
                                         IAlbumService albumService)
        {
            _albumRepository = albumRepository;
            _albumService = albumService;
        }
        public async Task HandleAsync(RemoveAlbumCommand request)
        {
            var album = await _albumRepository.SingleAsync(request.AlbumId);
            await _albumService.RemoveAsync(album);
        }
    }
}